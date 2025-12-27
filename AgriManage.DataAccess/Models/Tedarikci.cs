using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Tedarikci
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Ad { get; set; } // Firma Adı

        [StringLength(100)]
        public string YetkiliKisi { get; set; } // Eklendi: Hata çözüm 1

        [StringLength(20)]
        public string Telefon { get; set; } // Eklendi: Hata çözüm 2

        [StringLength(500)]
        public string Adres { get; set; }

        public virtual ICollection<StokHareket> StokHareketleri { get; set; }
    }
}