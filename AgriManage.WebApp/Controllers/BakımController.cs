using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AgriManage.Controllers
{
    // =========================================================================
    // YETKİLENDİRME:
    // Bu Controller'a sadece Adminler ve Bakım ekibi (Teknisyen/Görevli) girebilir.
    // Ziraat Mühendisleri veya Çiftçiler burayı göremez.
    // =========================================================================
    [Authorize(Roles = "Admin, BAKIMTEKNISYENI, BAKIMGOREVLISI")]
    public class BakimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BakimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. BAKIM LİSTESİ (PLANLAR + GEÇMİŞ)
        // ==========================================
        public async Task<IActionResult> Index()
        {
            // 1. Gelecek/Mevcut Planları Çek (Henüz yapılmamış olanlar)
            var bakimPlanlari = await _context.BakimPlanlari
                .Include(b => b.Ekipman)
                .Include(b => b.BakimTipi)
                .OrderBy(b => b.PlanlananTarih) // Tarihe göre sırala (En yakın tarih en üstte)
                .ToListAsync();

            // 2. Yapılmış Geçmiş Kayıtları Çek
            // Personel adını görebilmek için ApplicationUser bağlantısını kuruyoruz.
            var gecmisIslemler = await _context.BakimKayitlari
                .Include(k => k.Personel)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(k => k.BakimPlani)
                    .ThenInclude(bp => bp.Ekipman)
                .Include(k => k.BakimPlani)
                    .ThenInclude(bp => bp.BakimTipi)
                .OrderByDescending(k => k.Tarih) // En son yapılan işlem en üstte
                .ToListAsync();

            ViewBag.GecmisIslemler = gecmisIslemler;

            return View(bakimPlanlari);
        }

        // ==========================================
        // 2. PLAN OLUŞTURMA (SADECE ADMIN)
        // ==========================================
        [Authorize(Roles = "Admin")]
        public IActionResult PlanOlustur()
        {
            ViewData["EkipmanId"] = new SelectList(_context.Ekipmanlar, "Id", "Ad");
            ViewData["BakimTipiId"] = new SelectList(_context.BakimTipleri, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlanOlustur(BakimPlani plan)
        {
            // İlişkisel nesneler formdan null gelebilir, ID yeterlidir. Validasyonu temizliyoruz.
            ModelState.Remove("Ekipman");
            ModelState.Remove("BakimTipi");
            ModelState.Remove("BakimKayitlari");

            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa dropdownları tekrar doldur
            ViewData["EkipmanId"] = new SelectList(_context.Ekipmanlar, "Id", "Ad", plan.EkipmanId);
            ViewData["BakimTipiId"] = new SelectList(_context.BakimTipleri, "Id", "Ad", plan.BakimTipiId);
            return View(plan);
        }

        // ==========================================
        // 3. BAKIM YAP (İŞLEME ve GÜNCELLEME)
        // ==========================================
        public IActionResult BakimYap(int planId)
        {
            var plan = _context.BakimPlanlari
                .Include(p => p.Ekipman)
                .Include(p => p.BakimTipi)
                .FirstOrDefault(p => p.Id == planId);

            if (plan == null) return NotFound();

            // View'da hangi ekipmana işlem yapıldığını göstermek için
            ViewBag.PlanBilgisi = $"{plan.Ekipman.Ad} - {plan.BakimTipi.Ad}";

            var kayit = new BakimKaydi
            {
                BakimPlaniId = planId,
                Tarih = DateTime.Now // Varsayılan olarak şu anın saati
            };
            return View(kayit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BakimYap(BakimKaydi kayit)
        {
            // Validasyon temizliği
            ModelState.Remove("Personel");
            ModelState.Remove("BakimPlani");

            // --- PERSONEL BULMA MANTIĞI ---
            // Sisteme giriş yapmış kullanıcının email adresini al
            var userMail = User.Identity.Name;

            // Bu email adresine sahip Personel kartını bul
            var personel = _context.Personeller
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.ApplicationUser.Email == userMail);

            if (personel != null)
            {
                // Eğer giriş yapan kişi bir Personel ise (Teknisyen vb.)
                kayit.PersonelId = personel.Id;
            }
            else if (User.IsInRole("Admin"))
            {
                // Eğer giriş yapan Admin ise ama bir personel kartı yoksa:
                // Veritabanındaki herhangi bir personeli (örneğin ilkini) 'dummy' olarak ata
                // ve not düş. Bu, veritabanı bütünlüğünü korumak içindir.
                var dummyPersonel = _context.Personeller.FirstOrDefault();

                if (dummyPersonel != null)
                {
                    kayit.PersonelId = dummyPersonel.Id;
                    kayit.Aciklama = $"(ADMIN İŞLEMİ: {userMail}) - {kayit.Aciklama}";
                }
                else
                {
                    // Hiç personel yoksa işlem yapılamaz
                    ModelState.AddModelError("", "Sistemde kayıtlı hiç personel yok. Önce personel ekleyiniz.");
                    return ViewKayiplariDoldur(kayit);
                }
            }
            else
            {
                // Ne personel ne admin
                ModelState.AddModelError("", "Bu işlemi yapmak için yetkili bir personel kaydınız bulunamadı.");
                return ViewKayiplariDoldur(kayit);
            }

            if (ModelState.IsValid)
            {
                // 1. KAYDI EKLE (Geçmişe at)
                _context.Add(kayit);

                // 2. PLANI GÜNCELLE (Takvimi İleri At)
                var plan = await _context.BakimPlanlari.FindAsync(kayit.BakimPlaniId);
                if (plan != null)
                {
                    // Periyodik bakım: Bir sonraki bakım tarihini 1 ay sonraya atıyoruz.
                    // (Gerçek projede bu 'Periyot' alanından dinamik gelebilir)
                    plan.PlanlananTarih = DateTime.Now.AddMonths(1);
                    _context.Update(plan);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return ViewKayiplariDoldur(kayit);
        }

        // Hata durumunda View verilerini (ViewBag vb.) tekrar dolduran yardımcı metot
        private IActionResult ViewKayiplariDoldur(BakimKaydi kayit)
        {
            var plan = _context.BakimPlanlari
                .Include(p => p.Ekipman)
                .Include(p => p.BakimTipi)
                .FirstOrDefault(p => p.Id == kayit.BakimPlaniId);

            if (plan != null)
            {
                ViewBag.PlanBilgisi = $"{plan.Ekipman.Ad} - {plan.BakimTipi.Ad}";
            }
            return View(kayit);
        }
    }
}