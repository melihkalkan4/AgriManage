using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Sera
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; }
        public decimal AlanMetrekare { get; set; }

        public int LokasyonId { get; set; }
        [ForeignKey("LokasyonId")]
        public virtual Lokasyon Lokasyon { get; set; }
    }
}