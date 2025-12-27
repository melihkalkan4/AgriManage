using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Bolge
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // Örn: "Trakya", "İç Anadolu"

        public virtual ICollection<Lokasyon> Lokasyonlar { get; set; }
    }
}