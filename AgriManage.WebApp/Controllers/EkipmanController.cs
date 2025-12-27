using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;

namespace AgriManage.Controllers
{
    public class EkipmanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EkipmanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME
        public async Task<IActionResult> Index()
        {
            // Ekipmanları kategorisi ve durumuyla (Join) beraber çekiyoruz
            var ekipmanlar = await _context.Ekipmanlar
                .Include(e => e.EkipmanKategorisi)
                .Include(e => e.EkipmanDurumu)
                .ToListAsync();
            return View(ekipmanlar);
        }

        // 2. YENİ EKLEME SAYFASI (GET)
        public IActionResult Create()
        {
            // Dropdown (Açılır Kutu) için verileri hazırlıyoruz
            ViewData["EkipmanKategorisiId"] = new SelectList(_context.EkipmanKategorileri, "Id", "Ad");
            ViewData["EkipmanDurumuId"] = new SelectList(_context.EkipmanDurumlari, "Id", "Ad");
            return View();
        }

        // 3. YENİ EKLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ekipman ekipman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekipman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Hata varsa listeleri tekrar yükle
            ViewData["EkipmanKategorisiId"] = new SelectList(_context.EkipmanKategorileri, "Id", "Ad", ekipman.EkipmanKategorisiId);
            ViewData["EkipmanDurumuId"] = new SelectList(_context.EkipmanDurumlari, "Id", "Ad", ekipman.EkipmanDurumuId);
            return View(ekipman);
        }

        // 4. DÜZENLEME SAYFASI (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ekipman = await _context.Ekipmanlar.FindAsync(id);
            if (ekipman == null) return NotFound();

            ViewData["EkipmanKategorisiId"] = new SelectList(_context.EkipmanKategorileri, "Id", "Ad", ekipman.EkipmanKategorisiId);
            ViewData["EkipmanDurumuId"] = new SelectList(_context.EkipmanDurumlari, "Id", "Ad", ekipman.EkipmanDurumuId);
            return View(ekipman);
        }

        // 5. DÜZENLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ekipman ekipman)
        {
            if (id != ekipman.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(ekipman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EkipmanKategorisiId"] = new SelectList(_context.EkipmanKategorileri, "Id", "Ad", ekipman.EkipmanKategorisiId);
            ViewData["EkipmanDurumuId"] = new SelectList(_context.EkipmanDurumlari, "Id", "Ad", ekipman.EkipmanDurumuId);
            return View(ekipman);
        }

        // 6. SİLME ONAY SAYFASI (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ekipman = await _context.Ekipmanlar
                .Include(e => e.EkipmanKategorisi)
                .Include(e => e.EkipmanDurumu)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ekipman == null) return NotFound();

            return View(ekipman);
        }

        // 7. SİLME İŞLEMİ (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekipman = await _context.Ekipmanlar.FindAsync(id);
            if (ekipman != null)
            {
                _context.Ekipmanlar.Remove(ekipman);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}