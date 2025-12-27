using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AgriManage.WebApp.DTOs;
using AgriManage.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace AgriManage.WebApp.Controllers
{
    // Sadece giriş yapmış kullanıcılar girebilir
    [Authorize]
    public class EnvanterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;

        // Constructor (Dependency Injection)
        public EnvanterController(IHttpClientFactory httpClientFactory, TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        // ---------------------------------------------------------
        // 1. LİSTELEME SAYFASI
        // ---------------------------------------------------------
        public async Task<IActionResult> Index(int? tarlaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name;
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? "User";

            // Token Oluştur
            var token = _tokenService.TokenOlustur(userName, userRole, userId);

            // İstemci Oluştur
            var client = _httpClientFactory.CreateClient("InventoryClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // API'ye İstek At (TarlaId varsa ekle)
            string url = "/api/products";
            if (tarlaId.HasValue)
            {
                url += $"?tarlaId={tarlaId.Value}";
            }

            var response = await client.GetAsync(url);

            // View'a Tarla Bilgisini Taşı (Buton için gerekli)
            ViewBag.SeciliTarlaId = tarlaId;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var urunListesi = JsonSerializer.Deserialize<List<UrunDto>>(jsonString, options);
                return View(urunListesi);
            }

            // Hata varsa
            ViewBag.Hata = $"Veri çekilemedi. Kod: {response.StatusCode}";
            return View(new List<UrunDto>());
        }

        // ---------------------------------------------------------
        // 2. EKLEME SAYFASINI AÇ (GET)
        // ---------------------------------------------------------
        [HttpGet]
        public IActionResult Create(int tarlaId)
        {
            ViewBag.TarlaId = tarlaId;
            return View();
        }

        // ---------------------------------------------------------
        // 3. KAYDET VE API'YE GÖNDER (POST)
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(UrunDto model)
        {
            // Frontend validasyonu (boş alan kontrolü vb.)
            if (!ModelState.IsValid)
            {
                // Hata varsa sayfayı tekrar göster
                ViewBag.TarlaId = model.TarlaId; // ID kaybolmasın
                return View(model);
            }

            // Token Hazırla
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name;
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? "User";
            var token = _tokenService.TokenOlustur(userName, userRole, userId);

            // İstemci Hazırla
            var client = _httpClientFactory.CreateClient("InventoryClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // API'ye Gönder 🚀
            var response = await client.PostAsJsonAsync("/api/products", model);

            if (response.IsSuccessStatusCode)
            {
                // Başarılı! Listeye geri dön.
                return RedirectToAction("Index", new { tarlaId = model.TarlaId });
            }

            // API Hata Döndüyse
            ViewBag.Hata = "Kayıt API tarafından reddedildi.";
            ViewBag.TarlaId = model.TarlaId;
            return View(model);
        }
        // ---------------------------------------------------------
        // 4. AJAX İÇİN: DEPODAKİ ÜRÜNLERİ GETİR (JSON)
        // ---------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetDepoUrunleri()
        {
            // Token Hazırla
            var token = _tokenService.TokenOlustur(User.Identity.Name, User.FindFirstValue(ClaimTypes.Role) ?? "User", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var client = _httpClientFactory.CreateClient("InventoryClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // API'den Depo (TarlaId=0) ürünlerini çek
            var response = await client.GetAsync("/api/products?tarlaId=0");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                // JSON'u olduğu gibi Frontend'e pasla
                return Content(json, "application/json");
            }
            return Json(new List<object>());
        }

        // ---------------------------------------------------------
        // 5. AJAX İÇİN: TRANSFER İŞLEMİNİ YAP (POST)
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> UrunTransferEt(int urunId, int hedefTarlaId)
        {
            var token = _tokenService.TokenOlustur(User.Identity.Name, User.FindFirstValue(ClaimTypes.Role) ?? "User", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var client = _httpClientFactory.CreateClient("InventoryClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // API'deki "Assign" metodunu çağır
            // API Rotası: PUT /api/products/assign?productId=...&targetTarlaId=...
            var response = await client.PutAsync($"/api/products/assign?productId={urunId}&targetTarlaId={hedefTarlaId}", null);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}