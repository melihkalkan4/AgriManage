using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Tarla
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tarla adı boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Tarla adı en fazla 150 karakter olabilir.")]
        public string Ad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Alan 0'dan büyük olmalıdır.")]
        public decimal AlanDonum { get; set; }

        [StringLength(50)]
        public string? TapuAdaParsel { get; set; }

        // --- İLİŞKİSEL ALANLAR ---

        [Required(ErrorMessage = "Lütfen bir lokasyon seçiniz.")]
        public int LokasyonId { get; set; }

        [ForeignKey("LokasyonId")]
        public virtual Lokasyon? Lokasyon { get; set; }

        // --- LİSTELER (Boş olabilir '?' ekledik ki hata vermesin) ---
        public virtual ICollection<TarlaAraziTipi>? TarlaAraziTipleri { get; set; }
        public virtual ICollection<EkimPlani>? EkimPlanlari { get; set; }
        public virtual ICollection<Gorev>? Gorevler { get; set; }

        // Bu tarlanın sahibi
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; } // İlişki nesnesi

        // Urunler listesi genellikle tarlada değil ekim planında olur ama modelinde varsa ? koyalım
        public virtual ICollection<Urun>? Urunler { get; set; }
    }
}