using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models; // Veritabanı modelleri
using AgriManage.Models; // DashboardViewModel'in olduğu namespace (Senin koduna göre)
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(string userId, bool isAdmin)
        {
            var model = new DashboardViewModel();

            if (isAdmin)
            {
                // 1. Ana Listeleri Çek (Senin modelindeki alanlar)
                model.Tarlalar = await _context.Tarlalar.Include(t => t.Lokasyon).ToListAsync();
                model.Personeller = await _context.Personeller.Include(p => p.Pozisyon).ToListAsync();
                model.Ekipmanlar = await _context.Ekipmanlar.Include(e => e.EkipmanDurumu).ToListAsync();
                model.Stoklar = await _context.StokItemleri.Include(s => s.Depo).ToListAsync();

                // 2. Özet Bilgileri Hesapla
                // Not: StokItem tablosunda 'Fiyat' olmadığı için şimdilik sadece miktarı topluyoruz.
                // Eğer maliyet takibi yapacaksan StokItem'a 'BirimMaliyet' ekleyip burada çarpabilirsin.
                model.ToplamStokDegeri = await _context.StokItemleri.SumAsync(s => s.Miktar);

                // 3: Arızalı ID'si (Seed verisinde 3 numara Arızalı olarak tanımlıydı)
                model.ArizaliEkipmanSayisi = await _context.Ekipmanlar
                                                .CountAsync(e => e.EkipmanDurumuId == 3);

                // 2: Devam Ediyor ID'si
                model.AktifGorevSayisi = await _context.Gorevler
                                            .CountAsync(g => g.GorevDurumuId == 2);

                // 3. Son Hareketler
                model.SonHareketler = await _context.StokHareketleri
                                        .Include(sh => sh.StokItem)
                                        .OrderByDescending(sh => sh.Tarih)
                                        .Take(5)
                                        .ToListAsync();
            }
            else
            {
                // PERSONEL İÇİN KISITLI VERİ (Hata almamak için boş listeler oluşturuyoruz)
                var personel = await _context.Personeller
                    .FirstOrDefaultAsync(p => p.ApplicationUser.Id == userId);

                if (personel != null)
                {
                    // Sadece personeli ilgilendiren özetler
                    model.AktifGorevSayisi = await _context.Gorevler
                        .CountAsync(g => g.PersonelId == personel.Id && g.GorevDurumuId == 2);
                }

                // Personel için listeleri boş geçiyoruz ki View sayfasında "null" hatası almayalım
                model.Tarlalar = new List<Tarla>();
                model.Personeller = new List<Personel>();
                model.Ekipmanlar = new List<Ekipman>();
                model.Stoklar = new List<StokItem>();
                model.SonHareketler = new List<StokHareket>();
                model.ToplamStokDegeri = 0;
                model.ArizaliEkipmanSayisi = 0;
            }

            return model;
        }
    }
}