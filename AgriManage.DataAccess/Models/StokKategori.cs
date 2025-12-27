using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class StokKategorisi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // "Tohumlar", "Gübreler", "Zirai İlaçlar"

        public virtual ICollection<StokItem> StokItemleri { get; set; }
    }
}