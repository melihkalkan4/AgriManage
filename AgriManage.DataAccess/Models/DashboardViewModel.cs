using AgriManage.DataAccess.Models;

namespace AgriManage.Models
{
    public class DashboardViewModel
    {
        public List<Tarla> Tarlalar { get; set; }
        public List<Personel> Personeller { get; set; }
        public List<Ekipman> Ekipmanlar { get; set; }
        public List<StokItem> Stoklar { get; set; }
        public List<StokHareket> SonHareketler { get; set; }

        // Finansal Özetler
        public decimal ToplamStokDegeri { get; set; }
        public int ArizaliEkipmanSayisi { get; set; }
        public int AktifGorevSayisi { get; set; }
    }
}