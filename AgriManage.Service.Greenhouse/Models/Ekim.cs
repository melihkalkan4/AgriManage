using System;

// 👇 Burasının tam olarak böyle olması ŞART
namespace AgriManage.Service.Greenhouse.Models
{
    public class Ekim
    {
        public int Id { get; set; }
        public int SeraId { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public DateTime EkimTarihi { get; set; } = DateTime.Now;
        public int AdetVeyaM2 { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}