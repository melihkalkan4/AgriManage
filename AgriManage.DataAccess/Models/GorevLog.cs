using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class GorevLog
    {
        public int Id { get; set; }

        public int GorevId { get; set; }
        [ForeignKey("GorevId")]
        public virtual Gorev Gorev { get; set; }

        [Required]
        public string Aciklama { get; set; } // Örn: "Durum 'Devam Ediyor' olarak değiştirildi."
        public DateTime Tarih { get; set; }
    }
}