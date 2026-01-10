using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgriManage.WebApp.Controllers
{
    [Authorize(Roles = "Admin,ZiraatMuhendisi")]
    public class VardiyaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VardiyaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. VARDİYA LİSTESİ
        public async Task<IActionResult> Program()
        {
            var vardiyalar = await _context.PersonelVardiyalari
                .Include(pv => pv.Personel).ThenInclude(p => p.ApplicationUser)
                .Include(pv => pv.Vardiya)
                .OrderByDescending(pv => pv.Tarih)
                .ToListAsync();

            return View(vardiyalar);
        }

        // 2. VARDİYA ATA (GET) - EKRANIN AÇILDIĞI YER
        public async Task<IActionResult> Ata()
        {
            // Eğer hiç vardiya tanımı yoksa otomatik oluştur
            if (!await _context.Vardiyalar.AnyAsync())
            {
                var varsayilanVardiyalar = new List<Vardiya>
                {
                    new Vardiya { Ad = "Gündüz Vardiyası", BaslangicSaati = new TimeSpan(08, 0, 0), BitisSaati = new TimeSpan(16, 0, 0) },
                    new Vardiya { Ad = "Akşam Vardiyası", BaslangicSaati = new TimeSpan(16, 0, 0), BitisSaati = new TimeSpan(00, 0, 0) },
                    new Vardiya { Ad = "Gece Vardiyası", BaslangicSaati = new TimeSpan(00, 0, 0), BitisSaati = new TimeSpan(08, 0, 0) }
                };

                _context.Vardiyalar.AddRange(varsayilanVardiyalar);
                await _context.SaveChangesAsync();
            }

            // Personel Listesini Doldur
            ViewData["PersonelId"] = new SelectList(_context.Personeller.Include(p => p.ApplicationUser)
                .Select(p => new {
                    Id = p.Id,
                    Ad = p.SicilNo + " - " + (p.ApplicationUser != null ? p.ApplicationUser.TamAd : "İsimsiz")
                }), "Id", "Ad");

            // Vardiya Listesini Doldur
            var vardiyaListesi = await _context.Vardiyalar.ToListAsync();
            ViewData["VardiyaId"] = new SelectList(vardiyaListesi.Select(v => new {
                Id = v.Id,
                Gorunum = v.Gorunum
            }), "Id", "Gorunum");

            return View();
        }

        // 3. VARDİYA ATA (POST) - KAYDETME İŞLEMİ (DÜZELTİLDİ ✅)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ata(PersonelVardiya personelVardiya)
        {
            // 🔥 KRİTİK DÜZELTME BURASI 🔥
            // Formdan sadece ID'ler gelir, nesneler (Personel, Vardiya) boş gelir.
            // Bu yüzden Validasyon hatası almamak için bunları görmezden geliyoruz.
            ModelState.Remove("Personel");
            ModelState.Remove("Vardiya");

            if (ModelState.IsValid)
            {
                // Çakışma Kontrolü: Aynı personele aynı gün başka vardiya var mı?
                bool cakisma = await _context.PersonelVardiyalari
                    .AnyAsync(x => x.PersonelId == personelVardiya.PersonelId && x.Tarih.Date == personelVardiya.Tarih.Date);

                if (cakisma)
                {
                    ModelState.AddModelError("", "Bu personelin seçilen tarihte zaten bir vardiyası mevcut.");
                }
                else
                {
                    _context.Add(personelVardiya);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Program));
                }
            }

            // Hata varsa listeleri tekrar doldur (Sayfa patlamasın diye)
            ViewData["PersonelId"] = new SelectList(_context.Personeller.Include(p => p.ApplicationUser)
                .Select(p => new { Id = p.Id, Ad = p.SicilNo + " - " + (p.ApplicationUser != null ? p.ApplicationUser.TamAd : "") }), "Id", "Ad", personelVardiya.PersonelId);

            var vardiyaListesi = await _context.Vardiyalar.ToListAsync();
            ViewData["VardiyaId"] = new SelectList(vardiyaListesi.Select(v => new { Id = v.Id, Gorunum = v.Gorunum }), "Id", "Gorunum", personelVardiya.VardiyaId);

            return View(personelVardiya);
        }
    }
}