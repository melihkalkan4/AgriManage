using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Sezon
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ad { get; set; } // Örn: "2025 Sonbahar Ekimi", "2026 İlkbahar"
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public virtual ICollection<EkimPlani> EkimPlanlari { get; set; }
        public bool Aktif { get; set; } = true;
    }
}