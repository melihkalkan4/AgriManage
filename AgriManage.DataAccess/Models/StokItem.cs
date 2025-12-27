using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class StokItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Ad { get; set; } // Örn: "Üre Gübresi", "Ceyhan-99 Tohum"

        // Anlık stok miktarını burada tutuyoruz
        public decimal Miktar { get; set; }

        [Required]
        [StringLength(20)]
        public string Birim { get; set; } // "KG", "LT", "Adet"

        // --- İLİŞKİLER ---

        // 1. Kategori İlişkisi
        public int StokKategorisiId { get; set; }

        [ForeignKey("StokKategorisiId")]
        public virtual StokKategorisi StokKategorisi { get; set; }

        // 2. Depo İlişkisi (Eksik olan kısımdı, eklendi)
        public int DepoId { get; set; }

        [ForeignKey("DepoId")]
        public virtual Depo Depo { get; set; }

        // 3. Hareketler Listesi
        public virtual ICollection<StokHareket> StokHareketleri { get; set; }
        // EKSİK OLAN ALANLAR:
        public string? Aciklama { get; set; }

        public decimal KritikStokSeviyesi { get; set; }

        public string? RafNo { get; set; }

        // Mevcut DepoId ve StokKategorisiId alanlarınız aşağıda devam etsin...
    }
}