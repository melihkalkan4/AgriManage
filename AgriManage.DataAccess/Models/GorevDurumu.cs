using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class GorevDurumu
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ad { get; set; } // "Atandı", "Devam Ediyor", "Tamamlandı", "İptal Edildi"

        public virtual ICollection<Gorev> Gorevler { get; set; }
    }
}