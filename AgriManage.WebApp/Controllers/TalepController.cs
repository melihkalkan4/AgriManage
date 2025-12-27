using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgriManage.Controllers
{
    [Authorize]
    public class TalepController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TalepController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME
        public async Task<IActionResult> Index()
        {
            var talepler = _context.Talepler
                .Include(t => t.Personel) // SicilNo'ya erişmek için Include şart
                .AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var userMail = User.Identity.Name;
                var personel = _context.Personeller
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefault(p => p.ApplicationUser.Email == userMail);

                if (personel != null)
                {
                    talepler = talepler.Where(t => t.PersonelId == personel.Id);
                }
                else
                {
                    // Personel kaydı yoksa boş liste
                    talepler = talepler.Where(t => t.Id == -1);
                }
            }

            return View(await talepler.OrderByDescending(t => t.Tarih).ToListAsync());
        }

        // 2. YENİ TALEP (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. YENİ TALEP (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Talep talep)
        {
            var userMail = User.Identity.Name;

            // Personeli bulurken Ad'a ihtiyacımız yok, Email ve ID yeterli
            var personel = _context.Personeller
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.ApplicationUser.Email == userMail);

            if (personel != null)
            {
                talep.PersonelId = personel.Id;
                talep.DurumId = 0;
                talep.Tarih = DateTime.Now;

                _context.Add(talep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Personel kaydı bulunamadı.");
            return View(talep);
        }

        // 4. DURUM DEĞİŞTİR (ADMIN)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DurumDegistir(int id, int durumId)
        {
            var talep = await _context.Talepler.FindAsync(id);
            if (talep != null)
            {
                talep.DurumId = durumId;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}