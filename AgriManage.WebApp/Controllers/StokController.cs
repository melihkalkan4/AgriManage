using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AgriManage.WebApp.Controllers
{
    public class StokController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StokController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // 1. ÖZEL STOK VE FİNANSAL PANEL (Dashboard)
        // =========================================================
        public async Task<IActionResult> Dashboard()
        {
            var stoklar = await _context.StokItemleri
                .Include(s => s.StokKategorisi) // Kategori ilişkisi eklendi
                .Include(s => s.Depo)
                .ToListAsync();

            // Finansal analiz verileri
            // Not: Birim fiyat StokHareket'te olduğu için burada ortalama maliyet hesabı karmaşık olabilir.
            // Şimdilik sadece miktarı topluyoruz.
            ViewBag.ToplamEnvanterDegeri = stoklar.Sum(s => s.Miktar);

            // DÜZELTME 1: Modeldeki 'KritikStokSeviyesi' ismini kullandık
            ViewBag.KritikStokSayisi = stoklar.Count(s => s.Miktar < s.KritikStokSeviyesi);
            ViewBag.ToplamKalemSayisi = stoklar.Count;

            // Son 10 stok hareketini analiz için gönderiyoruz
            ViewBag.SonHareketler = await _context.StokHareketleri
                .Include(sh => sh.StokItem)
                .Include(sh => sh.Tedarikci) // Modelde var, dahil ettik
                .OrderByDescending(sh => sh.Tarih)
                .Take(10).ToListAsync();

            return View(stoklar);
        }

        // =========================================================
        // 2. STOK LİSTESİ (Index)
        // =========================================================
        public async Task<IActionResult> Index()
        {
            var envanter = await _context.StokItemleri
                .Include(s => s.StokKategorisi)
                .Include(s => s.Depo)
                .ToListAsync();
            return View(envanter);
        }

        public IActionResult Create()
        {
            // Dropdownlar Modeldeki FK isimlerine göre ayarlandı
            ViewData["StokKategorisiId"] = new SelectList(_context.StokKategorileri, "Id", "Ad");
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StokItem stokItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stokItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Hata olursa dropdownları tekrar doldur
            ViewData["StokKategorisiId"] = new SelectList(_context.StokKategorileri, "Id", "Ad", stokItem.StokKategorisiId);
            ViewData["DepoId"] = new SelectList(_context.Depolar, "Id", "Ad", stokItem.DepoId);
            return View(stokItem);
        }

        // =========================================================
        // 3. STOK HAREKETİ EKLEME (Manuel Giriş/Çıkış)
        // =========================================================
        public async Task<IActionResult> HareketEkle(int? id)
        {
            if (id == null) return NotFound();

            var stokItem = await _context.StokItemleri.FindAsync(id);
            if (stokItem == null) return NotFound();

            ViewBag.StokAd = stokItem.Ad;
            ViewBag.MevcutMiktar = stokItem.Miktar + " " + stokItem.Birim;

            // İlişkisel veriler
            ViewBag.TedarikciId = new SelectList(_context.Tedarikciler, "Id", "Ad");
            ViewBag.DepoId = new SelectList(_context.Depolar, "Id", "Ad", stokItem.DepoId);

            // Yeni hareket nesnesi oluşturuyoruz
            var hareket = new StokHareket
            {
                StokItemId = stokItem.Id,
                Tarih = DateTime.Now,
                DepoId = stokItem.DepoId // Varsayılan olarak ürünün deposu seçili gelsin
            };
            return View(hareket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HareketEkle(StokHareket hareket)
        {
            // Validasyon: Depo ve Tedarikci gibi zorunlu alanlar modelde varsa kontrol edilir.
            // Modelstate.IsValid kontrolünü geçici olarak esnetebiliriz veya dropdownları doldurmalıyız.

            // DÜZELTME 2: IslemTipi (Giris/Cikis) kontrolü
            if (hareket.IslemTipi != null)
            {
                _context.Add(hareket);

                var anaStok = await _context.StokItemleri.FindAsync(hareket.StokItemId);
                if (anaStok != null)
                {
                    // DÜZELTME: Modelde 'IslemTipi' string olarak tanımlı.
                    // Ekrandan "Giris" veya "Cikis" value'su gelmeli.
                    if (hareket.IslemTipi == "Giris")
                    {
                        anaStok.Miktar += hareket.Miktar;
                    }
                    else if (hareket.IslemTipi == "Cikis")
                    {
                        anaStok.Miktar -= hareket.Miktar;
                    }

                    _context.Update(anaStok);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            // Hata durumunda dropdownları tekrar doldurmak gerekir
            ViewBag.TedarikciId = new SelectList(_context.Tedarikciler, "Id", "Ad", hareket.TedarikciId);
            ViewBag.DepoId = new SelectList(_context.Depolar, "Id", "Ad", hareket.DepoId);

            return View(hareket);
        }

        // =========================================================
        // 4. HIZLI KULLANIM (TARLAYA DÜŞÜŞ)
        // =========================================================

        // GET: Hızlı Tüketim Sayfası
        public IActionResult HizliKullanim()
        {
            ViewBag.Tarlalar = new SelectList(_context.Tarlalar.ToList(), "Id", "Ad");

            // Stok listesini miktarıyla beraber gösterelim
            var stoklar = _context.StokItemleri
                .Select(s => new { Id = s.Id, Ad = $"{s.Ad} (Mevcut: {s.Miktar} {s.Birim})" })
                .ToList();
            ViewBag.Stoklar = new SelectList(stoklar, "Id", "Ad");

            return View();
        }

        // POST: Stoktan Düş ve Kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HizliKullanim(int TarlaId, int StokItemId, decimal Miktar, string Aciklama)
        {
            var stok = _context.StokItemleri.Find(StokItemId);
            if (stok != null)
            {
                if (stok.Miktar < Miktar)
                {
                    TempData["hata"] = $"Yetersiz Stok! Depoda sadece {stok.Miktar} {stok.Birim} {stok.Ad} var.";

                    // Listeleri tekrar yükle
                    ViewBag.Tarlalar = new SelectList(_context.Tarlalar.ToList(), "Id", "Ad");
                    var stokList = _context.StokItemleri
                         .Select(s => new { Id = s.Id, Ad = $"{s.Ad} (Mevcut: {s.Miktar} {s.Birim})" })
                         .ToList();
                    ViewBag.Stoklar = new SelectList(stokList, "Id", "Ad");

                    return View();
                }

                // 1. Stoktan Düş
                stok.Miktar -= Miktar;

                // 2. Hareket Kaydı Oluştur
                // DÜZELTME 3: StokHareket modeline uygun alan isimleri kullanıldı
                var hareket = new StokHareket
                {
                    StokItemId = StokItemId,
                    IslemTipi = "Cikis", // Modeldeki isim: IslemTipi
                    Miktar = Miktar,
                    Tarih = DateTime.Now,
                    DepoId = stok.DepoId, // Zorunlu alan: Ürünün bulunduğu depodan düşüyoruz
                    BirimFiyat = 0, // Zorunlu alan değil ama decimal olduğu için 0 atıyoruz
                    Aciklama = $"Tarla ID: {TarlaId} kullanımı. Not: {Aciklama}"
                };

                _context.StokHareketleri.Add(hareket);

                _context.SaveChanges();
                TempData["basarili"] = $"{Miktar} {stok.Birim} {stok.Ad} stoktan düşüldü.";

                return RedirectToAction("Index", "Tarla");
            }

            return View();
        }
    }
}