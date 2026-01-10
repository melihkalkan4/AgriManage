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

        // ... Diğer metodlar aynen kalabilir ...
        public int GetToplamPersonel() => _context.Personeller.Count();
        public int GetAktifGorevSayisi() => _context.Gorevler.Count(g => g.GorevDurumuId != 3);
        public double GetToplamArazi() => (double)(_context.Tarlalar.Sum(t => (decimal?)t.AlanDonum) ?? 0);
        public int GetBugunkuVardiyaSayisi() => _context.PersonelVardiyalari.Count(v => v.Tarih.Date == DateTime.Today);
        public Dictionary<string, double> GetTarlaDagilimi() => new Dictionary<string, double>();
        public Dictionary<string, decimal> GetAylikGiderler() => new Dictionary<string, decimal>();

        // 🔥 GÜNCELLENEN METOD 🔥
        public AnalizDto GetDetayliAnaliz()
        {
            var dto = new AnalizDto();

            // 1. ÖZET VERİLER
            dto.ToplamPersonel = _context.Personeller.Count();
            dto.AktifGorevSayisi = _context.Gorevler.Count(g => g.GorevDurumuId != 3);
            dto.ToplamArazi = (double)(_context.Tarlalar.Sum(t => (decimal?)t.AlanDonum) ?? 0);
            dto.BugunkuVardiya = _context.PersonelVardiyalari.Count(v => v.Tarih.Date == DateTime.Today);

            // 2. ANA SAYFA TABLOSU İÇİN TARLA LİSTESİ (BUNU EKLEDİK)
            var tarlalar = _context.Tarlalar.OrderByDescending(t => t.Id).Take(5).ToList();
            dto.TarlaIsimleri = tarlalar.Select(t => t.Ad).ToList();
            dto.TarlaAlanlari = tarlalar.Select(t => (double)t.AlanDonum).ToList();

            // 3. SİMÜLASYON VERİLERİ (Grafikler için)
            dto.Aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran" };
            dto.Gelirler = new List<decimal> { 45000, 52000, 38000, 75000, 92000, 85000 };
            dto.Giderler = new List<decimal> { 32000, 35000, 41000, 40000, 55000, 48000 };

            dto.UrunAdlari = new List<string> { "Buğday", "Mısır", "Ayçiçeği", "Arpa", "Yonca" };
            dto.BeklenenHasatTon = new List<double> { 150, 120, 80, 95, 200 };

            dto.GiderKalemleri = new List<string> { "Gübre", "Mazot", "Tohum", "İşçilik", "Bakım" };
            dto.GiderTutarlari = new List<decimal> { 55000, 85000, 42000, 60000, 25000 };

            var personeller = _context.Personeller.Include(p => p.ApplicationUser).Take(5).ToList();
            dto.PersonelAdlari = personeller.Select(p => p.ApplicationUser?.TamAd ?? "İsimsiz").ToList();
            dto.TamamlananGorevler = new List<int> { 15, 12, 18, 9, 22 };

            return dto;
        }
    }
}