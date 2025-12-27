using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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

        public AnalizViewModel GetGenelAnaliz(string userId)
        {
            var model = new AnalizViewModel();

            // 1. MEVCUT: EKİM VE ÜRÜN ANALİZİ
            var ekimler = _context.EkimPlanlari
                .Include(e => e.Tarla)
                .Include(e => e.Urun)
                .Include(e => e.Sezon)
                .Where(e => e.Tarla.ApplicationUserId == userId)
                .OrderByDescending(e => e.EkimTarihi)
                .ToList();

            model.AktifEkimler = ekimler;

            if (ekimler.Any())
            {
                var aktifOlanlar = ekimler.Where(x => x.HasatTarihi == null).ToList();
                model.ToplamEkiliAlan = (int)aktifOlanlar.Sum(e => e.Tarla.AlanDonum);
                model.BeklenenHasatMiktari = aktifOlanlar.Sum(e => e.BeklenenVerimKg);
                model.AktifSezonSayisi = ekimler.Select(e => e.SezonId).Distinct().Count();

                var urunGruplari = aktifOlanlar
                    .GroupBy(e => e.Urun.Ad)
                    .Select(g => new { UrunAdi = g.Key, ToplamAlan = g.Sum(x => x.Tarla.AlanDonum) })
                    .ToList();

                model.UrunAdlari = urunGruplari.Select(x => x.UrunAdi).ToList();
                model.UrunAlanlari = urunGruplari.Select(x => x.ToplamAlan).ToList();
            }

            // --- YENİ 1: EKİPMAN SAĞLIK DURUMU ANALİZİ ---
            // Tüm ekipmanları duruma göre grupla (Aktif, Arızalı, Bakımda)
            var ekipmanGruplari = _context.Ekipmanlar
                .Include(e => e.EkipmanDurumu)
                .GroupBy(e => e.EkipmanDurumu.Ad)
                .Select(g => new { Durum = g.Key, Sayi = g.Count() })
                .ToList();

            model.EkipmanDurumAdlari = ekipmanGruplari.Select(x => x.Durum).ToList();
            model.EkipmanDurumSayilari = ekipmanGruplari.Select(x => x.Sayi).ToList();


            // --- YENİ 2: DEPARTMAN PERSONEL ANALİZİ ---
            // Personelleri departmanlarına göre grupla
            var personelGruplari = _context.Personeller
                .Include(p => p.Pozisyon).ThenInclude(pos => pos.Departman)
                .GroupBy(p => p.Pozisyon.Departman.Ad)
                .Select(g => new { Departman = g.Key, Sayi = g.Count() })
                .ToList();

            model.DepartmanAdlari = personelGruplari.Select(x => x.Departman).ToList();
            model.DepartmanPersonelSayilari = personelGruplari.Select(x => x.Sayi).ToList();

            return model;
        }
    }
}