using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Ekipman
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Ad { get; set; } // Örn: "Case IH Traktör"
        [StringLength(100)]
        public string? Marka { get; set; }
        [StringLength(100)]
        public string? Model { get; set; }
        [StringLength(50)]
        public string? SeriNo { get; set; }

        public int EkipmanKategorisiId { get; set; }
        [ForeignKey("EkipmanKategorisiId")]
        public virtual EkipmanKategorisi EkipmanKategorisi { get; set; }

        public int EkipmanDurumuId { get; set; }
        [ForeignKey("EkipmanDurumuId")]
        public virtual EkipmanDurumu EkipmanDurumu { get; set; }

        public virtual ICollection<EkipmanLog> EkipmanLoglari { get; set; }
        public virtual ICollection<BakimPlani> BakimPlanlari { get; set; }
        public int MevcutCalismaSaati { get; set; } // Toplam kaç saat çalıştı?
    }
}