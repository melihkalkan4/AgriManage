using AgriManage.BusinessLogic.Dtos;
using AgriManage.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.BusinessLogic.Services
{
    public class AnalizService : IAnalizService
    {
        private readonly ApplicationDbContext _context;

        public AnalizService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AnalizDto GetDetayliAnaliz()
        {
            var dto = new AnalizDto();

            try
            {
                // ==========================================
                // 1. GENEL İSTATİSTİKLER
                // ==========================================
                if (_context.Tarlalar != null)
                {
                    dto.ToplamArazi = (double)_context.Tarlalar.Sum(x => x.AlanDonum);
                }

                if (_context.Personeller != null)
                {
                    dto.ToplamPersonel = _context.Personeller.Count();
                }

                // Görev İstatistikleri
                if (_context.Gorevler != null)
                {
                    dto.AktifGorevSayisi = _context.Gorevler.Count(x => x.GorevDurumu != null && x.GorevDurumu.Ad != "Tamamlandı");
                }

                dto.BugunkuVardiya = 0; // Vardiya tablosu aktifse burayı doldurabilirsin.

                // ==========================================
                // 2. TARLA DAĞILIMI (Pasta Grafik Verisi)
                // ==========================================
                if (_context.Tarlalar != null)
                {
                    var tarlalar = _context.Tarlalar.OrderByDescending(x => x.Id).Take(5).ToList();
                    dto.TarlaIsimleri = tarlalar.Select(x => x.Ad).ToList();
                    dto.TarlaAlanlari = tarlalar.Select(x => (double)x.AlanDonum).ToList();
                }

                // ==========================================
                // 3. PERSONEL PERFORMANS (Bar Grafik Verisi)
                // ==========================================
                if (_context.Personeller != null)
                {
                    var personeller = _context.Personeller
                                              .Include(p => p.ApplicationUser)
                                              .Include(p => p.AtananGorevler)
                                              .ThenInclude(g => g.GorevDurumu)
                                              .Take(5)
                                              .ToList();

                    dto.PersonelAdlari = personeller.Select(x =>
                        x.ApplicationUser != null && !string.IsNullOrEmpty(x.ApplicationUser.TamAd)
                            ? x.ApplicationUser.TamAd
                            : "P-" + x.SicilNo
                    ).ToList();

                    dto.TamamlananGorevler = personeller.Select(p =>
                        p.AtananGorevler != null
                            ? p.AtananGorevler.Count(g => g.GorevDurumu != null && g.GorevDurumu.Ad == "Tamamlandı")
                            : 0
                    ).ToList();
                }

                // ==========================================
                // 4. FİNANS SİMÜLASYONU (Çizgi Grafik Verisi)
                // ==========================================
                // Veritabanında gerçek finans tablosu olmadığı için simüle ediyoruz
                dto.Aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs" };
                dto.Gelirler = new List<decimal> { 15000, 22000, 18000, 35000, 42000 };
                dto.Giderler = new List<decimal> { 12000, 15000, 13000, 20000, 18000 };


                // ==========================================
                // 5. GELECEK TAHMİN MODELİ (YENİ ÖZELLİK)
                // ==========================================
                // Kural Tabanlı Tahmin: Tarladaki ürüne ve dönüme göre rekolte hesaplar.
                if (_context.Tarlalar != null)
                {
                    var tarlalar = _context.Tarlalar.ToList();

                    // Referans Tablosu: (Verim [Ton/Dönüm], Fiyat [TL/Ton])
                    var verimOranlari = new Dictionary<string, (double Verim, decimal Fiyat)>
                    {
                        { "Buğday", (0.45, 8500) },   // Dönüme 450kg
                        { "Ayçiçek", (0.28, 14000) }, // Dönüme 280kg
                        { "Mısır", (1.2, 5500) },     // Dönüme 1.2 ton (Slajlık)
                        { "Diğer", (0.35, 6000) }     // Varsayılan
                    };

                    decimal toplamTahminiGelir = 0;
                    double toplamTahminiRekolte = 0;
                    var urunBazliRekolte = new Dictionary<string, double>();

                    foreach (var tarla in tarlalar)
                    {
                        // Basit Zeka: Tarla isminden ürün tahmini
                        string urun = "Diğer";
                        string adKucuk = tarla.Ad.ToLower();

                        if (adKucuk.Contains("buğday") || adKucuk.Contains("ova")) urun = "Buğday";
                        else if (adKucuk.Contains("ayçiçek") || adKucuk.Contains("tepe")) urun = "Ayçiçek";
                        else if (adKucuk.Contains("mısır") || adKucuk.Contains("dere")) urun = "Mısır";

                        // Hesaplama: Alan * Verim = Toplam Ürün
                        double rekolte = (double)tarla.AlanDonum * verimOranlari[urun].Verim;
                        decimal gelir = (decimal)rekolte * verimOranlari[urun].Fiyat;

                        toplamTahminiRekolte += rekolte;
                        toplamTahminiGelir += gelir;

                        // Grafik için gruplama
                        if (urunBazliRekolte.ContainsKey(urun))
                            urunBazliRekolte[urun] += rekolte;
                        else
                            urunBazliRekolte.Add(urun, rekolte);
                    }

                    // Hesaplanan verileri DTO'ya yükle
                    dto.TahminiGelecekGelir = toplamTahminiGelir;
                    dto.TahminiToplamRekolte = Math.Round(toplamTahminiRekolte, 1);
                    dto.TahminUrunAdlari = urunBazliRekolte.Keys.ToList();
                    dto.TahminUrunMiktarlari = urunBazliRekolte.Values.Select(v => Math.Round(v, 1)).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Analiz Hatası: " + ex.Message);
            }

            return dto;
        }
    }
}