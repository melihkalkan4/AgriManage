using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    // ApplicationUser (giriş yapan) ile fiziksel personel kaydını bağlar
    public class Personel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string SicilNo { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }

        // İlişki: Hangi sisteme giriş yapan kullanıcıya bağlı? (1-1 ilişki)
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        // İlişki: Hangi pozisyonda?
        public int PozisyonId { get; set; }
        [ForeignKey("PozisyonId")]
        public virtual Pozisyon Pozisyon { get; set; }

        public virtual ICollection<PersonelVardiya> PersonelVardiyalari { get; set; }
        public virtual ICollection<Gorev> AtananGorevler { get; set; }
        public virtual ICollection<BakimKaydi> BakimKayitlari { get; set; }
    }
}