using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class AraziTipi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // Örn: "Killi Toprak", "Kumlu Toprak", "Taşlı"

        public virtual ICollection<TarlaAraziTipi> TarlaAraziTipleri { get; set; }
    }
}