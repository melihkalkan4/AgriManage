using Microsoft.AspNetCore.Mvc;
using AgriManage.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;

namespace AgriManage.WebApp.Controllers
{
    [Authorize(Roles = "Admin,ZiraatMuhendisi")]
    public class AnalizController : Controller
    {
        private readonly IAnalizService _analizService;

        public AnalizController(IAnalizService analizService)
        {
            _analizService = analizService;
        }

        public IActionResult Index()
        {
            // Servisten DTO geliyor, View'a iletiyoruz.
            var model = _analizService.GetDetayliAnaliz();
            return View(model);
        }
    }
}