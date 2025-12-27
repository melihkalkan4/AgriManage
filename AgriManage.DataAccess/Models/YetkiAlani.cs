using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class YetkiAlani
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ModulAdi { get; set; } // Örn: "StokYonetimi"
        [StringLength(100)]
        public string YetkiTipi { get; set; } // Örn: "Okuma", "Yazma", "Silme"

        public virtual ICollection<PozisyonYetki> PozisyonYetkileri { get; set; }
    }
}