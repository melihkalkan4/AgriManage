using Microsoft.AspNetCore.Identity; // <-- EKLENMESİ GEREKEN SATIR
using System.ComponentModel.DataAnnotations;
using AgriManage.DataAccess.Models; // <-- BUNU DA EKLEYİN (Gorev ilişkisi için)

namespace AgriManage.DataAccess.Models
{
    // Varsayılan kullanıcı tablosuna (AspNetUsers) ek alanlar ekliyoruz
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string TamAd { get; set; }

        // İlişki: Bu kullanıcı bir personele bağlı olabilir
        public virtual Personel Personel { get; set; }
    }
}