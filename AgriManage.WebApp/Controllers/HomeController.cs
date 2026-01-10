using AgriManage.BusinessLogic.Services; // Servisi kullanmak için
using AgriManage.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgriManage.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnalizService _analizService; // Servis Tanýmý

        // Dependency Injection ile servisi içeri alýyoruz
        public HomeController(ILogger<HomeController> logger, IAnalizService analizService)
        {
            _logger = logger;
            _analizService = analizService;
        }

        public IActionResult Index()
        {
            // Eðer kullanýcý giriþ yapmamýþsa, verileri çekmeye çalýþma, sadece sayfayý göster
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            // Giriþ yapmýþsa servisten DTO verisini çek
            var model = _analizService.GetDetayliAnaliz();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}