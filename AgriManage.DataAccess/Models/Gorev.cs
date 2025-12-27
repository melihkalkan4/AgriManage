using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Gorev
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Baslik { get; set; }

        public string? Aciklama { get; set; }

        // --- TARİH YÖNETİMİ ---
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Required]
        public DateTime PlanlananBaslangic { get; set; } = DateTime.Now; // Eski 'Tarih' yerine bu var

        public DateTime? TamamlanmaTarihi { get; set; }

        // --- İLİŞKİLER ---
        public int GorevDurumuId { get; set; }
        public virtual GorevDurumu? GorevDurumu { get; set; }

        public int? PersonelId { get; set; }
        public virtual Personel? Personel { get; set; }

        public int? TarlaId { get; set; }
        public virtual Tarla? Tarla { get; set; }

        // --- OPERASYONEL ---
        public int? EkipmanId { get; set; }
        public virtual Ekipman? Ekipman { get; set; }
        public int TahminiSureSaat { get; set; } = 0;

        public int? StokItemId { get; set; }
        public virtual StokItem? StokItem { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PlanlananStokMiktari { get; set; } = 0;
    }
}