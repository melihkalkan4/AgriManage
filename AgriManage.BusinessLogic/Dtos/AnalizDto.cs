using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Dtos
{
    public class AnalizDto
    {
        // 1. TEKİL DEĞERLER (Özet Kartlar İçin)
        public int ToplamPersonel { get; set; }
        public int AktifGorevSayisi { get; set; }
        public double ToplamArazi { get; set; }
        public int BugunkuVardiya { get; set; }

        // 2. TARLA LİSTELERİ (Pasta Grafik)
        public List<string> TarlaIsimleri { get; set; }
        public List<double> TarlaAlanlari { get; set; }

        // 3. FİNANS LİSTELERİ (Çizgi Grafik)
        public List<string> Aylar { get; set; }
        public List<decimal> Gelirler { get; set; }
        public List<decimal> Giderler { get; set; }

        // 4. PERSONEL PERFORMANS (Bar Grafik)
        public List<string> PersonelAdlari { get; set; }
        public List<int> TamamlananGorevler { get; set; }

        // ==========================================
        // 5. YENİ: GELECEK TAHMİN MODELİ (Forecasting)
        // ==========================================
        public decimal TahminiGelecekGelir { get; set; } // Beklenen Toplam Para (TL)
        public double TahminiToplamRekolte { get; set; } // Beklenen Toplam Ürün (Ton)

        // Tahmin Grafiği İçin Listeler
        public List<string> TahminUrunAdlari { get; set; }
        public List<double> TahminUrunMiktarlari { get; set; } // Ton Bazlı

        // ==========================================
        // CONSTRUCTOR (Yapıcı Metot)
        // ==========================================
        public AnalizDto()
        {
            // Null Reference hatası almamak için tüm listeleri başlatıyoruz
            TarlaIsimleri = new List<string>();
            TarlaAlanlari = new List<double>();

            Aylar = new List<string>();
            Gelirler = new List<decimal>();
            Giderler = new List<decimal>();

            PersonelAdlari = new List<string>();
            TamamlananGorevler = new List<int>();

            // Yeni eklenen listeleri de başlatıyoruz
            TahminUrunAdlari = new List<string>();
            TahminUrunMiktarlari = new List<double>();
        }
    }
}