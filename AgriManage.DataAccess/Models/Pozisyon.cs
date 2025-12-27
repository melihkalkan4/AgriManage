using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Pozisyon
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // Örn: Ziraat Mühendisi, Operatör

        public int DepartmanId { get; set; }
        [ForeignKey("DepartmanId")]
        public virtual Departman Departman { get; set; }

        public virtual ICollection<Personel> Personeller { get; set; }
        public virtual ICollection<PozisyonYetki> PozisyonYetkileri { get; set; }
    }
}