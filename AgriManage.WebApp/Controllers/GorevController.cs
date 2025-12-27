using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;

namespace AgriManage.Controllers
{
    public class GorevController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GorevController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. LİSTELEME
        // ==========================================
        public async Task<IActionResult> Index()
        {
            var gorevler = await _context.Gorevler
                .Include(g => g.Personel)
                .Include(g => g.Tarla)
                .Include(g => g.Ekipman)
                .Include(g => g.StokItem)
                .Include(g => g.GorevDurumu)
                .OrderByDescending(g => g.PlanlananBaslangic)
                .ToListAsync();
            return View(gorevler);
        }

        // ==========================================
        // 2. YENİ GÖREV OLUŞTURMA (GET)
        // ==========================================
        public IActionResult Create()
        {
            // --- CRITICAL FIX: DROPDOWNLARI DOLDURMA ---
            // Bu kısım boş olduğu için sayfa açılırken NullReference hatası alıyordun.

            // Personel Listesi: Sicil No ile gösteriyoruz
            var personeller = _context.Personeller.Select(p => new {
                Id = p.Id,
                Gorunum = p.SicilNo // Eğer modelinde Ad varsa: p.SicilNo + " - " + p.Ad 
            }).ToList();

            ViewData["PersonelId"] = new SelectList(personeller, "Id", "Gorunum");
            ViewData["TarlaId"] = new SelectList(_context.Tarlalar, "Id", "Ad");
            ViewData["EkipmanId"] = new SelectList(_context.Ekipmanlar, "Id", "Ad");
            ViewData["StokItemId"] = new SelectList(_context.StokItemleri, "Id", "Ad");

            return View();
        }

        // ==========================================
        // 3. YENİ GÖREV KAYDETME (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gorev gorev)
        {
            // Otomatik Tarih ve Durum Ataması
            gorev.OlusturmaTarihi = DateTime.Now;

            // Eğer durum seçilmediyse varsayılan "1: Atandı" yap
            if (gorev.GorevDurumuId == 0) gorev.GorevDurumuId = 1;

            // Formdan gelmeyen navigation property'leri validasyondan çıkar
            ModelState.Remove("Personel");
            ModelState.Remove("Tarla");
            ModelState.Remove("Ekipman");
            ModelState.Remove("StokItem");
            ModelState.Remove("GorevDurumu");

            if (ModelState.IsValid)
            {
                _context.Add(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Hata olursa (Validasyon) dropdownları tekrar doldur ki sayfa patlamasın
            var personeller = _context.Personeller.Select(p => new { Id = p.Id, Gorunum = p.SicilNo }).ToList();
            ViewData["PersonelId"] = new SelectList(personeller, "Id", "Gorunum", gorev.PersonelId);
            ViewData["TarlaId"] = new SelectList(_context.Tarlalar, "Id", "Ad", gorev.TarlaId);
            ViewData["EkipmanId"] = new SelectList(_context.Ekipmanlar, "Id", "Ad", gorev.EkipmanId);
            ViewData["StokItemId"] = new SelectList(_context.StokItemleri, "Id", "Ad", gorev.StokItemId);

            return View(gorev);
        }

        // ==========================================
        // 4. GÖREVİ TAMAMLA (OPERASYONEL ZEKA & STOK DÜŞÜMÜ)
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> GoreviTamamla(int id)
        {
            var gorev = await _context.Gorevler
                .Include(g => g.StokItem) // Stok detayına erişmek için şart
                .Include(g => g.Ekipman)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gorev == null) return NotFound();

            // A. Görev Durumunu Güncelle
            gorev.GorevDurumuId = 3; // 3: Tamamlandı varsayıyoruz
            gorev.TamamlanmaTarihi = DateTime.Now;

            // B. Stoktan Düş ve Hareket Kaydı Gir
            if (gorev.StokItemId != null && gorev.PlanlananStokMiktari > 0)
            {
                // StokItem null değilse işlem yap
                if (gorev.StokItem != null)
                {
                    // 1. Ana stok miktarını düşür
                    gorev.StokItem.Miktar -= gorev.PlanlananStokMiktari;

                    // 2. Stok Hareket Kaydı Oluştur (Senin Modeline Uygun)
                    var hareket = new StokHareket
                    {
                        StokItemId = gorev.StokItemId.Value,
                        Tarih = DateTime.Now,
                        Miktar = gorev.PlanlananStokMiktari,

                        // DÜZELTME 1: Senin modelinde 'IslemTipi' (string) var
                        IslemTipi = "Cikis",

                        // DÜZELTME 2: 'DepoId' zorunlu alan. Bunu StokItem'dan alıyoruz.
                        DepoId = gorev.StokItem.DepoId,

                        // Ekstra bilgiler
                        Aciklama = $"Görev #{gorev.Id} kapsamında kullanım",
                        BirimFiyat = 0 // Şimdilik 0 veya stok maliyetinden çekilebilir
                    };

                    _context.Add(hareket);
                }
            }

            // C. Ekipman Saatini Güncelle
            if (gorev.EkipmanId != null && gorev.TahminiSureSaat > 0)
            {
                if (gorev.Ekipman != null)
                {
                    gorev.Ekipman.MevcutCalismaSaati += gorev.TahminiSureSaat;
                    _context.Update(gorev.Ekipman);
                }
            }

            _context.Update(gorev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}