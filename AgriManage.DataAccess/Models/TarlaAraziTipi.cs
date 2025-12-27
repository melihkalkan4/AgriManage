using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class TarlaAraziTipi
    {
        public int Id { get; set; }

        public int TarlaId { get; set; }
        [ForeignKey("TarlaId")]
        public virtual Tarla Tarla { get; set; }

        public int AraziTipiId { get; set; }
        [ForeignKey("AraziTipiId")]
        public virtual AraziTipi AraziTipi { get; set; }
    }
}