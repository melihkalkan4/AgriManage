using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class EkipmanKategorisi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // Örn: "Traktörler", "Sulama Sistemleri", "El Aletleri"

        public virtual ICollection<Ekipman> Ekipmanlar { get; set; }
    }
}