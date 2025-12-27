using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AgriManage.BusinessLogic.Services;
using AgriManage.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.WebApp.Controllers
{
    [Authorize]
    public class VardiyaController : Controller
    {
        private readonly IVardiyaService _vardiyaService;

        public VardiyaController(IVardiyaService vardiyaService)
        {
            _vardiyaService = vardiyaService;
        }

        public IActionResult Program() => View(_vardiyaService.TumProgramiGetir());

        public IActionResult Index() => View(_vardiyaService.GetVardiyaTanimlari());

        [Authorize(Roles = "Admin")]
        public IActionResult Ata()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Ata(int PersonelId, int VardiyaId, DayOfWeek Gun)
        {
            _vardiyaService.VardiyaAta(PersonelId, VardiyaId, Gun);
            TempData["basarili"] = "Vardiya ataması başarıyla tamamlandı.";
            return RedirectToAction("Program");
        }

        private void LoadDropdowns()
        {
            ViewBag.Vardiyalar = new SelectList(_vardiyaService.GetVardiyaTanimlari(), "Id", "Ad");

            var personeller = _vardiyaService.GetAllPersoneller()
                .Select(p => new {
                    Id = p.Id,
                    Gorunum = $"{p.SicilNo} - {p.ApplicationUser?.TamAd ?? "İsimsiz"}"
                }).ToList();

            ViewBag.Personeller = new SelectList(personeller, "Id", "Gorunum");

            var gunler = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = GetTurkishDay(g)
                }).ToList();
            ViewBag.Gunler = new SelectList(gunler, "Value", "Text");
        }

        private string GetTurkishDay(DayOfWeek day) => day switch
        {
            DayOfWeek.Monday => "Pazartesi",
            DayOfWeek.Tuesday => "Salı",
            DayOfWeek.Wednesday => "Çarşamba",
            DayOfWeek.Thursday => "Perşembe",
            DayOfWeek.Friday => "Cuma",
            DayOfWeek.Saturday => "Cumartesi",
            _ => "Pazar"
        };
    }
}