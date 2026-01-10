namespace AgriManage.Service.Greenhouse.Models
{
    public class Hasat
    {
        public int Id { get; set; }
        public int SeraId { get; set; }

        // 👇 YENİ: Hangi ekimden hasat edildi? (Boş olabilir, eski kayıtlar bozulmasın diye nullable yaptım)
        public int? EkimId { get; set; }

        public string UrunAdi { get; set; } = string.Empty;
        public double MiktarKg { get; set; }
        public decimal Gelir { get; set; }
        public DateTime Tarih { get; set; }
    }
}