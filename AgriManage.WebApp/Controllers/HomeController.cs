using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AgriManage.BusinessLogic.Services; // Servisleri kullanýyoruz

namespace AgriManage.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAnalizService _analizService; // Daha önce eklemiþtik

        // Constructor Injection ile servisleri alýyoruz
        public HomeController(IDashboardService dashboardService, IAnalizService analizService)
        {
            _dashboardService = dashboardService;
            _analizService = analizService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isAdmin = User.IsInRole("Admin");

            // TÜM ÝÞ MANTIÐI ARTIK SERVÝSTE
            var model = await _dashboardService.GetDashboardDataAsync(userId, isAdmin);

            return View(model);
        }

        public IActionResult Analiz()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _analizService.GetGenelAnaliz(userId);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}