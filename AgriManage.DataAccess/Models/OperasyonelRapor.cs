using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class OperasyonelRapor
    {
        public int Id { get; set; }
        public DateTime RaporTarihi { get; set; }

        [Required]
        public string RaporOzeti { get; set; } // "Sulama tamamlandı, 2 ton su kullanıldı."

        public int GorevId { get; set; }
        [ForeignKey("GorevId")]
        public virtual Gorev Gorev { get; set; }

        // Bu operasyonda kullanılan malzemeler (çıkış fişleri)
        public virtual ICollection<StokHareket> KullanilanMalzemeler { get; set; }
    }
}