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
        // 1. BAKIM LİSTESİ VE ALARMLAR (HERKES GÖRÜR)
        // ==========================================
        public async Task<IActionResult> Index()
        {
            // Planları ve geçmiş kayıtları çek
            var bakimPlanlari = await _context.BakimPlanlari
                .Include(b => b.Ekipman)
                .Include(b => b.BakimTipi)
                .ToListAsync();

            // KONTROL MEKANİZMASI: Hangi araçların bakımı gelmiş?
            // Bu bilgiyi View'da renklendirmek için kullanacağız.
            return View(bakimPlanlari);
        }

        // ==========================================
        // 2. YENİ BAKIM PLANI OLUŞTUR (SADECE ADMIN)
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
        // 3. BAKIM YAPILDI GİRİŞİ (ÇALIŞAN/TEKNİKER)
        // ==========================================
        public IActionResult BakimYap(int planId)
        {
            var plan = _context.BakimPlanlari
                .Include(p => p.Ekipman)
                .Include(p => p.BakimTipi)
                .FirstOrDefault(p => p.Id == planId);

            if (plan == null) return NotFound();

            // Yeni kayıt ekranına plan bilgilerini taşıyoruz
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
            // Personel ID'yi giriş yapan kullanıcıdan bul
            var userMail = User.Identity.Name;
            var personel = _context.Personeller
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.ApplicationUser.Email == userMail);

            if (personel != null)
            {
                kayit.PersonelId = personel.Id; // Bakımı yapan kişi

                // Bakım yapıldığı için Ekipman'ın son bakım saatini güncellemek gerekebilir
                // (Gelişmiş versiyonda buraya eklenebilir)

                _context.Add(kayit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Personel bulunamadı.");
            return View(kayit);
        }
    }
}