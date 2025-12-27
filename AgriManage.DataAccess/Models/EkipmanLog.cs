using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class EkipmanLog
    {
        public int Id { get; set; }

        public int EkipmanId { get; set; }
        [ForeignKey("EkipmanId")]
        public virtual Ekipman Ekipman { get; set; }

        [Required]
        [StringLength(500)]
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}