using AgriManage.BusinessLogic.Dtos;
using AgriManage.BusinessLogic.Services; // Data deðil, Service namespace'i
using Microsoft.AspNetCore.Mvc;

namespace AgriManage.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalizService _analizService;

        // Servisi enjekte ediyoruz
        public HomeController(IAnalizService analizService)
        {
            _analizService = analizService;
        }

        public IActionResult Index()
        {
            // Kullanýcý giriþ yapmamýþsa direkt View döndür (Login butonlarýný görsün)
            if (User?.Identity?.IsAuthenticated != true)
            {
                return View();
            }

            // Kullanýcý giriþ yapmýþsa verileri Servis'ten çek
            // AnalizController'da kullanýlan metodun aynýsýný burada da kullanýyoruz!
            var model = _analizService.GetDetayliAnaliz();

            return View(model);
        }
    }
}