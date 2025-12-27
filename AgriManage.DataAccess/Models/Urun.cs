using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [Display(Name = "Ürün Adı")]
        public string Ad { get; set; } // Örn: Buğday, Arpa, Mısır

        [Required]
        [Display(Name = "Ekim Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EkimTarihi { get; set; }

        [Display(Name = "Hasat Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? HasatTarihi { get; set; } // Boş olabilir (Henüz hasat edilmediyse)

        [Display(Name = "Beklenen Rekolte (Ton)")]
        public double BeklenenRekolte { get; set; }

        // --- İLİŞKİLER (Foreign Key) ---
        // Her ürün mutlaka bir tarlaya ekilir.
        public int TarlaId { get; set; }

        [ForeignKey("TarlaId")]
        public virtual Tarla Tarla { get; set; }
        public int UrunKategorisiId { get; set; }

        // Eğer Navigation Property kullanıyorsan o da burada kalmalı:
        public UrunKategorisi UrunKategorisi { get; set; }
    }
}