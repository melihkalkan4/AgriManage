using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriManage.WebApp.Models.ViewModels
{
    public class PersonelCreateViewModel
    {
        // --- 1. KULLANICI HESABI BİLGİLERİ (Identity) ---
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        public string TamAd { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Admin, personele geçici şifre verecek

        // --- 2. PERSONEL ÖZLÜK BİLGİLERİ ---
        [Required(ErrorMessage = "Sicil No zorunludur.")]
        public string SicilNo { get; set; }

        [Required]
        public DateTime IseBaslamaTarihi { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Pozisyon seçiniz.")]
        public int PozisyonId { get; set; }

        // Dropdown Listesi (Sadece Pozisyonlar için lazım, User listesine gerek kalmadı)
        public IEnumerable<SelectListItem>? PozisyonListesi { get; set; }
    }
}