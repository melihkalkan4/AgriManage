using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Vardiya
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ad { get; set; } // Örn: "08:00 - 16:00"
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public virtual ICollection<PersonelVardiya> PersonelVardiyalari { get; set; }
    }
}