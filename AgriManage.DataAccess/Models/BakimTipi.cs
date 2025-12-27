using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class BakimTipi
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; } // "Periyodik Bakım", "Arıza Onarımı", "Yağ Değişimi"

        public virtual ICollection<BakimPlani> BakimPlanlari { get; set; }
    }
}