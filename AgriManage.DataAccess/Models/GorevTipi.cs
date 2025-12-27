using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class GorevTipi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // "Sulama", "Gübreleme", "İlaçlama", "Hasat"

        public virtual ICollection<Gorev> Gorevler { get; set; }
    }
}