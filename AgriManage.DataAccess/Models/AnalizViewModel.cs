using System.Collections.Generic;

namespace AgriManage.DataAccess.Models
{
    public class AnalizViewModel
    {
        // Özet İstatistikler
        public int ToplamEkiliAlan { get; set; }
        public int AktifSezonSayisi { get; set; }
        public decimal BeklenenHasatMiktari { get; set; }

        // Grafik Verileri (Boş gelirse hata vermesin diye new'liyoruz)
        public List<string> UrunAdlari { get; set; } = new List<string>();
        public List<decimal> UrunAlanlari { get; set; } = new List<decimal>();

        public List<string> EkipmanDurumAdlari { get; set; } = new List<string>();
        public List<int> EkipmanDurumSayilari { get; set; } = new List<int>();

        public List<string> DepartmanAdlari { get; set; } = new List<string>();
        public List<int> DepartmanPersonelSayilari { get; set; } = new List<int>();

        // Listeler
        public List<EkimPlani> AktifEkimler { get; set; } = new List<EkimPlani>();

        // YENİ EKLENEN: Geçmiş hasatları tutacak liste
        public List<EkimPlani> GecmisEkimler { get; set; } = new List<EkimPlani>();

        public IEnumerable<Tarla> Tarlalar { get; set; } = new List<Tarla>();
    }
}