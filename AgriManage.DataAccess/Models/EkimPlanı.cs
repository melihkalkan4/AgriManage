using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class EkimPlani
    {
        public int Id { get; set; }

        public int TarlaId { get; set; }
        [ForeignKey("TarlaId")]
        public virtual Tarla Tarla { get; set; }

        public int UrunId { get; set; }
        [ForeignKey("UrunId")]
        public virtual Urun Urun { get; set; }

        public int SezonId { get; set; }
        [ForeignKey("SezonId")]
        public virtual Sezon Sezon { get; set; }

        public DateTime? EkimTarihi { get; set; }
        public DateTime? HasatTarihi { get; set; }
        public decimal BeklenenVerimKg { get; set; }
        public decimal GerceklesenVerimKg { get; set; }
    }
}