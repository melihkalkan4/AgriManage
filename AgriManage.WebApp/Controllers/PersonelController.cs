using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;

namespace AgriManage.Controllers
{
    public class PersonelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. PERSONEL LİSTELEME
        public async Task<IActionResult> Index()
        {
            var personeller = await _context.Personeller
                .Include(p => p.Pozisyon)
                .Include(p => p.ApplicationUser) // Kullanıcı bilgisini çekiyoruz
                .Include(p => p.AtananGorevler)
                .ToListAsync();
            return View(personeller);
        }

        // 2. YENİ PERSONEL (GET)
        public IActionResult Create()
        {
            ViewData["PozisyonId"] = new SelectList(_context.Pozisyonlar, "Id", "Ad");
            // Kullanıcıları (ApplicationUser) listelemek istersen buraya ekleyebilirsin
            return View();
        }

        // 3. YENİ PERSONEL (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Personel personel)
        {
            // Sicil No otomatik atanmıyorsa validasyon için gereklidir
            if (ModelState.IsValid)
            {
                _context.Add(personel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PozisyonId"] = new SelectList(_context.Pozisyonlar, "Id", "Ad", personel.PozisyonId);
            return View(personel);
        }

        // ==========================================
        // 4. HIZLI GÖREV ATAMA (DÜZELTİLEN KISIM)
        // ==========================================
        public IActionResult GorevAta()
        {
            // DÜZELTME: Senin 'Personel' modelinde 'Ad' yok. 'SicilNo' var.
            // Listede şöyle görünecek: "SICIL123 - (Mühendis)"
            var personelListesi = _context.Personeller
                .Include(p => p.Pozisyon)
                .Select(p => new {
                    Id = p.Id,
                    Gorunum = p.SicilNo + " - (" + (p.Pozisyon != null ? p.Pozisyon.Ad : "Pozisyon Yok") + ")"
                }).ToList();

            var tarlaListesi = _context.Tarlalar.Select(t => new {
                Id = t.Id,
                Gorunum = t.Ad + " (" + t.AlanDonum + " dk)"
            }).ToList();

            ViewData["PersonelId"] = new SelectList(personelListesi, "Id", "Gorunum");
            ViewData["TarlaId"] = new SelectList(tarlaListesi, "Id", "Gorunum");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GorevAta(Gorev gorev)
        {
            // Görev modeline uygun tarih atamaları
            gorev.OlusturmaTarihi = DateTime.Now;

            if (gorev.PlanlananBaslangic == DateTime.MinValue)
                gorev.PlanlananBaslangic = DateTime.Now;

            // Varsayılan Durum: 1 (Bekliyor)
            if (gorev.GorevDurumuId == 0) gorev.GorevDurumuId = 1;

            // Formda olmayan alanlar hata vermesin diye temizliyoruz
            ModelState.Remove("Personel");
            ModelState.Remove("Tarla");
            ModelState.Remove("GorevDurumu");
            ModelState.Remove("Ekipman");
            ModelState.Remove("StokItem");

            if (ModelState.IsValid)
            {
                _context.Add(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Hata durumunda listeyi tekrar doldur (Sicil No ile)
            var personelListesi = _context.Personeller
                .Include(p => p.Pozisyon)
                .Select(p => new { Id = p.Id, Gorunum = p.SicilNo + " - " + (p.Pozisyon != null ? p.Pozisyon.Ad : "") })
                .ToList();

            ViewData["PersonelId"] = new SelectList(personelListesi, "Id", "Gorunum", gorev.PersonelId);
            ViewData["TarlaId"] = new SelectList(_context.Tarlalar, "Id", "Ad", gorev.TarlaId);

            return View(gorev);
        }
    }
}