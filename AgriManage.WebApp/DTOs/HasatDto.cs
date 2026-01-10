namespace AgriManage.WebApp.DTOs
{
    public class HasatDto
    {
        public int Id { get; set; }
        public int SeraId { get; set; }

        // 👇 YENİ: Hangi Ekimden geldiğini bilmemiz lazım
        public int? EkimId { get; set; }

        public string UrunAdi { get; set; } = string.Empty;
        public double MiktarKg { get; set; }
        public decimal Gelir { get; set; }
        public DateTime Tarih { get; set; }

        // 👇 YENİ: Sadece taşıma amaçlı (Checkbox bilgisini tutar)
        public bool KokSokumu { get; set; }
    }
}