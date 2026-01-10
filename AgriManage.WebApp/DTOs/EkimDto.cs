namespace AgriManage.WebApp.DTOs
{
    public class EkimDto
    {
        public int Id { get; set; }
        public int SeraId { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public DateTime EkimTarihi { get; set; }
        public int AdetVeyaM2 { get; set; }
        public bool AktifMi { get; set; }
    }
}