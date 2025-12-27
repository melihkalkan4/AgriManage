using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class StokHareket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Tarih { get; set; }

        [Required]
        [StringLength(20)]
        public string IslemTipi { get; set; } // "Giris" veya "Cikis"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Miktar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BirimFiyat { get; set; } // Eklendi: Finansal takip

        [StringLength(500)]
        public string Aciklama { get; set; }

        // --- İLİŞKİLER ---
        public int StokItemId { get; set; }
        [ForeignKey("StokItemId")]
        public virtual StokItem StokItem { get; set; }

        public int DepoId { get; set; }
        [ForeignKey("DepoId")]
        public virtual Depo Depo { get; set; }

        public int? TedarikciId { get; set; }
        [ForeignKey("TedarikciId")]
        public virtual Tedarikci Tedarikci { get; set; }

        public int? OperasyonelRaporId { get; set; }
        [ForeignKey("OperasyonelRaporId")]
        public virtual OperasyonelRapor OperasyonelRapor { get; set; }
    }
}