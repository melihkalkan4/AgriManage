using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Dtos
{
    public class AnalizDto
    {
        // --- ÖZET KARTLAR ---
        public int ToplamPersonel { get; set; }
        public int AktifGorevSayisi { get; set; }
        public double ToplamArazi { get; set; }
        public int BugunkuVardiya { get; set; }

        // --- 👇 BURASI EKSİKTİ, BUNLARI EKLEMEN LAZIM 👇 ---
        // Ana Sayfa'daki "Arazi Listesi" tablosu bu verileri arıyor
        public List<string> TarlaIsimleri { get; set; }
        public List<double> TarlaAlanlari { get; set; }

        // --- GRAFİK 1: FİNANSAL (Çizgi Grafik) ---
        public List<string> Aylar { get; set; }
        public List<decimal> Gelirler { get; set; }
        public List<decimal> Giderler { get; set; }

        // --- GRAFİK 2: ÜRÜN VERİMİ (Bar Grafik) ---
        public List<string> UrunAdlari { get; set; }
        public List<double> BeklenenHasatTon { get; set; }

        // --- GRAFİK 3: GİDER KALEMLERİ (Pasta Grafik) ---
        public List<string> GiderKalemleri { get; set; }
        public List<decimal> GiderTutarlari { get; set; }

        // --- GRAFİK 4: PERSONEL PERFORMANSI (Yatay Bar) ---
        public List<string> PersonelAdlari { get; set; }
        public List<int> TamamlananGorevler { get; set; }
    }
}