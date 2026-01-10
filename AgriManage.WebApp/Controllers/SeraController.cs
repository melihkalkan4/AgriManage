using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AgriManage.WebApp.Services; // SeraService burada
using AgriManage.WebApp.DTOs;     // DTO'lar burada

namespace AgriManage.WebApp.Controllers
{
    [Authorize] // Sadece giriş yapmış kullanıcılar girebilsin
    public class SeraController : Controller
    {
        // 👇 DİKKAT: Başındaki "I" harfini kaldırdık. Doğrudan sınıfı kullanıyoruz.
        private readonly SeraService _seraService;
        private readonly TokenService _tokenService;

        // Constructor'da da "SeraService" istiyoruz (Interface değil)
        public SeraController(SeraService seraService, TokenService tokenService)
        {
            _seraService = seraService;
            _tokenService = tokenService;
        }

        // =======================================================
        // 1. LİSTELEME (INDEX)
        // =======================================================
        public async Task<IActionResult> Index()
        {
            var token = GetUserToken();
            var seralar = await _seraService.GetSeralarAsync(token);
            return View(seralar);
        }

        // =======================================================
        // 2. DETAY SAYFASI
        // =======================================================
        public async Task<IActionResult> Details(int id)
        {
            var token = GetUserToken();

            var sera = await _seraService.GetSeraByIdAsync(id, token);
            if (sera == null) return RedirectToAction("Index");

            // Hasatları ve Ekimleri getir
            var hasatlar = await _seraService.GetHasatlarAsync(id, token);
            var ekimler = await _seraService.GetEkimlerAsync(id, token);

            ViewBag.Hasatlar = hasatlar;
            ViewBag.Ekimler = ekimler;

            return View(sera);
        }

        // =======================================================
        // 3. YENİ SERA EKLEME
        // =======================================================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeraDto sera)
        {
            var token = GetUserToken();

            if (!string.IsNullOrEmpty(sera.Ad))
            {
                var sonuc = await _seraService.AddSeraAsync(sera, token);
                if (sonuc) return RedirectToAction(nameof(Index));
            }
            return View(sera);
        }

        // =======================================================
        // 4. HASAT EKLEME
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHasat(int seraId, string urunAdi, double miktar, decimal gelir)
        {
            var token = GetUserToken();

            var hasatDto = new HasatDto
            {
                SeraId = seraId,
                UrunAdi = urunAdi ?? "Bilinmiyor",
                MiktarKg = miktar,
                Gelir = gelir,
                Tarih = DateTime.Now
            };

            await _seraService.AddHasatAsync(hasatDto, token);
            return RedirectToAction("Details", new { id = seraId });
        }

        // =======================================================
        // 5. EKİM (YENİ ÜRÜN EKME)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEkim(int seraId, string urunAdi, int adet)
        {
            var token = GetUserToken();

            var ekimDto = new EkimDto
            {
                SeraId = seraId,
                UrunAdi = urunAdi,
                AdetVeyaM2 = adet,
                AktifMi = true,
                EkimTarihi = DateTime.Now
            };

            await _seraService.AddEkimAsync(ekimDto, token);
            return RedirectToAction("Details", new { id = seraId });
        }

        // =======================================================
        // YARDIMCI: Token Al
        // =======================================================
        private string GetUserToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";
            var userName = User.Identity?.Name ?? "User";
            var role = User.FindFirstValue(ClaimTypes.Role) ?? "User";

            return _tokenService.TokenOlustur(userName, role, userId);
        }
        // =======================================================
        // 6. DÜZENLEME (EDIT) İŞLEMLERİ
        // =======================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = GetUserToken();
            var sera = await _seraService.GetSeraByIdAsync(id, token);

            if (sera == null) return RedirectToAction("Index");

            return View(sera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SeraDto sera)
        {
            var token = GetUserToken();

            // API güncelleme isteği gönder
            var sonuc = await _seraService.UpdateSeraAsync(sera, token);

            if (sonuc)
            {
                return RedirectToAction("Details", new { id = sera.Id });
            }

            // Hata olursa sayfayı tekrar göster
            ModelState.AddModelError("", "Güncelleme başarısız oldu.");
            return View(sera);
        }
    }
}