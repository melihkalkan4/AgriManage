using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class PozisyonYetki
    {
        public int Id { get; set; }

        public int PozisyonId { get; set; }
        [ForeignKey("PozisyonId")]
        public virtual Pozisyon Pozisyon { get; set; }

        public int YetkiAlaniId { get; set; }
        [ForeignKey("YetkiAlaniId")]
        public virtual YetkiAlani YetkiAlani { get; set; }
    }
}