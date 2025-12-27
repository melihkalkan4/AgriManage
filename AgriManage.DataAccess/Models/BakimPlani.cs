using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class BakimPlani
    {
        public int Id { get; set; }
        public DateTime PlanlananTarih { get; set; }

        public int EkipmanId { get; set; }
        [ForeignKey("EkipmanId")]
        public virtual Ekipman Ekipman { get; set; }

        public int BakimTipiId { get; set; }
        [ForeignKey("BakimTipiId")]
        public virtual BakimTipi BakimTipi { get; set; }

        public bool Tamamlandi { get; set; }

        public virtual ICollection<BakimKaydi> BakimKayitlari { get; set; }
    }
}