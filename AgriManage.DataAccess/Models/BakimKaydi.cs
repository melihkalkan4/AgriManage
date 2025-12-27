using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class BakimKaydi
    {
        [Key]
        public int Id { get; set; }

        // HATA VEREN KISIM: Bu satırı ekleyerek çözüyoruz
        public DateTime Tarih { get; set; } = DateTime.Now;

        public string? Aciklama { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Maliyet { get; set; } = 0;

        // --- İLİŞKİLER ---

        // Hangi plana istinaden yapıldı?
        public int BakimPlaniId { get; set; }
        public virtual BakimPlani? BakimPlani { get; set; }

        // Bakımı sisteme kim girdi?
        public int? PersonelId { get; set; }
        public virtual Personel? Personel { get; set; }
    }
}