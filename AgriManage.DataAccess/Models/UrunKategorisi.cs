using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class UrunKategorisi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // Örn: "Tahıllar", "Yağlı Tohumlar", "Baklagiller"

        public virtual ICollection<Urun> Urunler { get; set; }
    }
}