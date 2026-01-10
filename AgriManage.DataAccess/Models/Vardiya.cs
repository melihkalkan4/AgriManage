using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Vardiya
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public string Gorunum => $"{Ad} ({BaslangicSaati:hh\\:mm} - {BitisSaati:hh\\:mm})";
    }
}