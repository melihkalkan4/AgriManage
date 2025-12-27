using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AgriManage.WebApp.DTOs;
using AgriManage.WebApp.Services; // TokenService için
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims; // Kullanıcı bilgilerini okumak için
using System.Net.Http.Headers; // Header ayarı için

namespace AgriManage.WebApp.Controllers
{
    // Sadece giriş yapmış kullanıcılar bu sayfayı görebilsin
    [Authorize]
    public class EnvanterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;

        public EnvanterController(IHttpClientFactory httpClientFactory, TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Giriş yapmış kullanıcının bilgilerini alıyoruz
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name;

            // Kullanıcının rolünü buluyoruz (Yoksa varsayılan 'User' olsun)
            var userRole = User.FindFirstValue(ClaimTypes.Role) ?? "User";

            // 2. TokenService'i kullanarak taze bir PASAPORT (Token) basıyoruz
            var token = _tokenService.TokenOlustur(userName, userRole, userId);

            // 3. API ile konuşacak İstemciyi hazırlıyoruz
            var client = _httpClientFactory.CreateClient("InventoryClient");

            // 4. KRİTİK NOKTA: Pasaportu isteğin başlığına zımbalıyoruz 📎
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 5. Artık kapıdan geçebiliriz! API'ye istek atıyoruz
            var response = await client.GetAsync("/api/products");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var urunListesi = JsonSerializer.Deserialize<List<UrunDto>>(jsonString, options);

                return View(urunListesi);
            }
            else
            {
                // Eğer hala 401 hatası alıyorsak veya başka bir sorun varsa
                ViewBag.Hata = $"API Erişimi Reddedildi! Durum Kodu: {response.StatusCode}";
                return View(new List<UrunDto>());
            }
        }
    }
}