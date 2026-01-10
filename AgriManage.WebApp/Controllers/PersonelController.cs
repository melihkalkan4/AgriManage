using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using AgriManage.WebApp.Models.ViewModels;

namespace AgriManage.WebApp.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin erişebilir
    public class PersonelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonelController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==========================================
        // 1. PERSONEL LİSTELEME
        // ==========================================
        public async Task<IActionResult> Index()
        {
            var personeller = await _context.Personeller
                .Include(p => p.Pozisyon)
                .Include(p => p.ApplicationUser)
                .Include(p => p.AtananGorevler)
                .ToListAsync();
            return View(personeller);
        }

        // ==========================================
        // 2. YENİ PERSONEL & KULLANICI (GET)
        // ==========================================
        public IActionResult Create()
        {
            var pozisyonlar = _context.Pozisyonlar
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Ad })
                .ToList();

            var model = new PersonelCreateViewModel
            {
                PozisyonListesi = pozisyonlar,
                IseBaslamaTarihi = DateTime.Now
            };

            return View(model);
        }

        // ==========================================
        // 3. YENİ PERSONEL & KULLANICI (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    TamAd = model.TamAd,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var pozisyon = await _context.Pozisyonlar.FindAsync(model.PozisyonId);
                    if (pozisyon != null)
                    {
                        string atanacakRol = "Ciftci";
                        var pAd = pozisyon.Ad.ToLower();

                        if (pAd.Contains("mühendis") || pAd.Contains("muhendis")) atanacakRol = "ZiraatMuhendisi";
                        else if (pAd.Contains("bakım") || pAd.Contains("teknik")) atanacakRol = "BakimGorevlisi";
                        else if (pAd.Contains("yönetici") || pAd.Contains("müdür")) atanacakRol = "Admin";

                        await _userManager.AddToRoleAsync(user, atanacakRol);
                    }

                    var personel = new Personel
                    {
                        SicilNo = model.SicilNo,
                        IseBaslamaTarihi = model.IseBaslamaTarihi,
                        PozisyonId = model.PozisyonId,
                        ApplicationUserId = user.Id
                    };

                    _context.Add(personel);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            model.PozisyonListesi = _context.Pozisyonlar
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Ad })
                .ToList();
            return View(model);
        }

        // ==========================================
        // 4. PERSONEL DÜZENLEME (EDIT - GET)
        // ==========================================
        public async Task<IActionResult> Edit(int id)
        {
            var personel = await _context.Personeller
                .Include(p => p.ApplicationUser)
                .Include(p => p.Pozisyon)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (personel == null) return NotFound();

            ViewData["PozisyonId"] = new SelectList(_context.Pozisyonlar, "Id", "Ad", personel.PozisyonId);
            return View(personel);
        }

        // ==========================================
        // 5. PERSONEL DÜZENLEME (EDIT - POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Personel personel)
        {
            if (id != personel.Id) return NotFound();

            // Navigasyon property'lerini validasyondan muaf tut
            ModelState.Remove("ApplicationUser");
            ModelState.Remove("Pozisyon");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Personeller.Any(e => e.Id == personel.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PozisyonId"] = new SelectList(_context.Pozisyonlar, "Id", "Ad", personel.PozisyonId);
            return View(personel);
        }

        // ==========================================
        // 6. PERSONEL SİLME (DELETE)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel != null)
            {
                _context.Personeller.Remove(personel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // 7. HIZLI GÖREV ATAMA
        // ==========================================
        public IActionResult GorevAta()
        {
            var personelListesi = _context.Personeller
                .Include(p => p.ApplicationUser)
                .Select(p => new {
                    Id = p.Id,
                    Gorunum = p.SicilNo + " - " + (p.ApplicationUser != null ? p.ApplicationUser.TamAd : "İsimsiz")
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
            gorev.OlusturmaTarihi = DateTime.Now;
            if (gorev.PlanlananBaslangic == DateTime.MinValue) gorev.PlanlananBaslangic = DateTime.Now;
            if (gorev.GorevDurumuId == 0) gorev.GorevDurumuId = 1;

            ModelState.Remove("Personel");
            ModelState.Remove("Tarla");
            ModelState.Remove("GorevDurumu");

            if (ModelState.IsValid)
            {
                _context.Add(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(GorevAta));
        }
    }
}