using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgriManage.Controllers
{
    [Authorize]
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
            // 1. Gelecek/Mevcut Planları Çek
            var bakimPlanlari = await _context.BakimPlanlari
                .Include(b => b.Ekipman)
                .Include(b => b.BakimTipi)
                .OrderBy(b => b.PlanlananTarih) // Tarihe göre sırala
                .ToListAsync();

            // 2. Yapılmış Geçmiş Kayıtları Çek
            // BURASI ÇOK ÖNEMLİ: Personel adını görebilmek için ApplicationUser'ı da çekiyoruz.
            var gecmisIslemler = await _context.BakimKayitlari
                .Include(k => k.Personel)
                    .ThenInclude(p => p.ApplicationUser) // <--- İşte TamAd'ı getiren kilit nokta burası
                .Include(k => k.BakimPlani).ThenInclude(bp => bp.Ekipman)
                .Include(k => k.BakimPlani).ThenInclude(bp => bp.BakimTipi)
                .OrderByDescending(k => k.Tarih) // En son yapılan en üstte
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
            // ID gönderiyoruz, nesne null geliyor. Validasyonu temizliyoruz.
            ModelState.Remove("Ekipman");
            ModelState.Remove("BakimTipi");
            ModelState.Remove("BakimKayitlari");

            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

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

            ViewBag.PlanBilgisi = $"{plan.Ekipman.Ad} - {plan.BakimTipi.Ad}";

            var kayit = new BakimKaydi
            {
                BakimPlaniId = planId,
                Tarih = DateTime.Now
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

            // Kullanıcıyı Bul
            var userMail = User.Identity.Name;
            var personel = _context.Personeller
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.ApplicationUser.Email == userMail);

            // Personel Atama Mantığı
            if (personel != null)
            {
                kayit.PersonelId = personel.Id;
            }
            else if (User.IsInRole("Admin"))
            {
                // Admin ise ama Personel kaydı yoksa, dummy personel ata ve not düş
                var dummyPersonel = _context.Personeller.FirstOrDefault();
                if (dummyPersonel != null)
                {
                    kayit.PersonelId = dummyPersonel.Id;
                    kayit.Aciklama = $"(ADMIN: {userMail}) - {kayit.Aciklama}";
                }
            }
            else
            {
                ModelState.AddModelError("", "Yetkisiz işlem. Personel veya Admin olmalısınız.");
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
                    // Plan tarihini 1 ay ileri atıyoruz (Periyodik bakım mantığı)
                    plan.PlanlananTarih = DateTime.Now.AddMonths(1);
                    _context.Update(plan);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return ViewKayiplariDoldur(kayit);
        }

        // Hata durumunda View verilerini tekrar dolduran yardımcı metot
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