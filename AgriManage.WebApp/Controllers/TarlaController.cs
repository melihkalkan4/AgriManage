using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AgriManage.BusinessLogic.Services;
using AgriManage.DataAccess.Models;
using System.Security.Claims;
using System;
using System.Linq;

namespace AgriManage.WebApp.Controllers
{
    public class TarlaController : Controller
    {
        private readonly ITarlaService _tarlaService;

        public TarlaController(ITarlaService tarlaService)
        {
            _tarlaService = tarlaService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tüm tarlaları (Seninkiler + Sahipsizler) getir
            var tarlalar = _tarlaService.GetAllTarlalar(userId);
            var aktifEkimler = _tarlaService.GetAktifEkimler(userId);
            var gecmisEkimler = _tarlaService.GetGecmisEkimler(userId);

            var model = new AnalizViewModel
            {
                Tarlalar = tarlalar,
                AktifEkimler = aktifEkimler,
                GecmisEkimler = gecmisEkimler
            };

            if (model.AktifEkimler != null && model.AktifEkimler.Any())
            {
                model.ToplamEkiliAlan = (int)model.AktifEkimler.Sum(e => e.Tarla?.AlanDonum ?? 0);
            }

            return View(model);
        }

        // ==========================================
        // EKİM İŞLEMİ (401 HATASI ÇÖZÜLDÜ)
        // ==========================================
        public IActionResult EkimYap(int id)
        {
            var tarla = _tarlaService.GetTarlaById(id);
            if (tarla == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // DÜZELTME: Tarlanın sahibi sensen VEYA tarlanın sahibi yoksa (NULL) izin ver
            if (tarla.ApplicationUserId != null && tarla.ApplicationUserId != userId)
                return Unauthorized();

            var model = new EkimPlani
            {
                TarlaId = id,
                EkimTarihi = DateTime.Now
            };

            ViewBag.TarlaAd = tarla.Ad;
            ViewData["UrunId"] = new SelectList(_tarlaService.GetAllUrunler(), "Id", "Ad");
            ViewData["SezonId"] = new SelectList(_tarlaService.GetAktifSezonlar(), "Id", "Ad");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EkimYap(EkimPlani ekimPlani)
        {
            ModelState.Remove("Tarla");
            ModelState.Remove("Urun");
            ModelState.Remove("Sezon");

            if (ModelState.IsValid)
            {
                ekimPlani.Id = 0; // Identity Insert Hatası Önlemi
                _tarlaService.CreateEkimPlani(ekimPlani);
                TempData["basarili"] = "Ekim işlemi başarıyla kaydedildi.";
                return RedirectToAction(nameof(Index));
            }

            var tarla = _tarlaService.GetTarlaById(ekimPlani.TarlaId);
            ViewBag.TarlaAd = tarla?.Ad;
            ViewData["UrunId"] = new SelectList(_tarlaService.GetAllUrunler(), "Id", "Ad", ekimPlani.UrunId);
            ViewData["SezonId"] = new SelectList(_tarlaService.GetAktifSezonlar(), "Id", "Ad", ekimPlani.SezonId);

            return View(ekimPlani);
        }

        // ==========================================
        // CREATE (VALIDASYON HATASI ÇÖZÜLDÜ)
        // ==========================================
        public IActionResult Create()
        {
            LoadLokasyonList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tarla tarla)
        {
            // Formdan gelmeyen tüm alanları validasyondan temizliyoruz
            ModelState.Remove("Lokasyon");
            ModelState.Remove("ApplicationUser");
            ModelState.Remove("ApplicationUserId");

            // Listeler
            ModelState.Remove("EkimPlanlari");
            ModelState.Remove("TarlaAraziTipleri");
            ModelState.Remove("Gorevler");
            ModelState.Remove("Urunler");

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                tarla.ApplicationUserId = userId; // Yeni tarlayı sana zimmetliyoruz

                _tarlaService.CreateTarla(tarla);

                TempData["basarili"] = "Yeni tarla başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }

            // Hata varsa listeyi tekrar doldur
            LoadLokasyonList();
            return View(tarla);
        }

        // ==========================================
        // EDIT (401 HATASI ÇÖZÜLDÜ)
        // ==========================================
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var tarla = _tarlaService.GetTarlaById(id.Value);
            if (tarla == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // DÜZELTME: Sahipsiz tarlaları düzenlemeye izin ver
            if (tarla.ApplicationUserId != null && tarla.ApplicationUserId != userId)
                return Unauthorized();

            LoadLokasyonList();
            return View(tarla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tarla tarla)
        {
            ModelState.Remove("Lokasyon");
            ModelState.Remove("ApplicationUser");
            ModelState.Remove("EkimPlanlari");
            ModelState.Remove("TarlaAraziTipleri");
            ModelState.Remove("Gorevler");
            ModelState.Remove("Urunler");

            if (ModelState.IsValid)
            {
                // Mevcut sahiplik bilgisini korumak gerekebilir ama şimdilik sana atıyoruz
                // Veya hidden field ile taşıyabiliriz. Basitlik adına:
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (tarla.ApplicationUserId == null) tarla.ApplicationUserId = userId;

                _tarlaService.UpdateTarla(tarla);
                TempData["basarili"] = "Tarla güncellendi.";
                return RedirectToAction("Index");
            }

            LoadLokasyonList();
            return View(tarla);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var tarla = _tarlaService.GetTarlaById(id.Value);
            if (tarla == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (tarla.ApplicationUserId != null && tarla.ApplicationUserId != userId) return Unauthorized();

            LoadLokasyonList();
            return View(tarla);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            _tarlaService.DeleteTarla(id);
            TempData["basarili"] = "Tarla silindi.";
            return RedirectToAction("Index");
        }

        public IActionResult HasatYap(int id)
        {
            var sonEkim = _tarlaService.GetSonEkimByTarlaId(id);
            if (sonEkim == null || sonEkim.HasatTarihi != null)
            {
                TempData["hata"] = "Hasat edilecek ürün yok!";
                return RedirectToAction("Index");
            }
            return View(sonEkim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HasatYap(int id, decimal GerceklesenVerimKg, DateTime HasatTarihi)
        {
            _tarlaService.HasatYap(id, GerceklesenVerimKg, HasatTarihi);
            TempData["basarili"] = "Hasat kaydedildi.";
            return RedirectToAction("Index");
        }

        private void LoadLokasyonList()
        {
            ViewBag.LokasyonListesi = _tarlaService.GetAllLokasyonlar();
        }
    }
}