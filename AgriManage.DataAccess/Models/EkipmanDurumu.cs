using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class EkipmanDurumu
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ad { get; set; } // "Aktif", "Bakımda", "Arızalı", "Depoda"

        public virtual ICollection<Ekipman> Ekipmanlar { get; set; }
    }
}