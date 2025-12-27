using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Depo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // "Ana Depo", "İlaç Deposu"

        public int LokasyonId { get; set; }
        [ForeignKey("LokasyonId")]
        public virtual Lokasyon Lokasyon { get; set; }

        public virtual ICollection<StokHareket> StokHareketleri { get; set; }
        public virtual ICollection<StokItem> StokItemleri { get; set; }
    }
}