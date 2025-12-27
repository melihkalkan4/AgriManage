using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class PersonelVardiya
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        [ForeignKey("PersonelId")]
        public virtual Personel Personel { get; set; }

        public int VardiyaId { get; set; }
        [ForeignKey("VardiyaId")]
        public virtual Vardiya Vardiya { get; set; }

        public DayOfWeek Gun { get; set; } // Hangi gün bu vardiyada
    }
}