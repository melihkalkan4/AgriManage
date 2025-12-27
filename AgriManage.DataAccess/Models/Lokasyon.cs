using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Lokasyon
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Ad { get; set; } // Örn: "Merkez Çiftlik", "Tekirdağ Tesisi"
        public string? Adres { get; set; }

        public int BolgeId { get; set; }
        [ForeignKey("BolgeId")]
        public virtual Bolge Bolge { get; set; }

        public virtual ICollection<Tarla> Tarlalar { get; set; }
        public virtual ICollection<Sera> Seralar { get; set; }
        public virtual ICollection<Depo> Depolar { get; set; }
    }
}