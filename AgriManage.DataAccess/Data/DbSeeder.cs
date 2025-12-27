using AgriManage.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.DataAccess.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Veritabanı yoksa oluşturur
            context.Database.EnsureCreated();

            // ==========================================
            // 1. TEMEL TANIMLAR (Sadece Mevcut Alanlar)
            // ==========================================

            // LOKASYONLAR
            if (!context.Lokasyonlar.Any())
            {
                context.Lokasyonlar.AddRange(new List<Lokasyon>
                {
                    new Lokasyon { Ad = "Kuzey Ovası (Edirne)" },
                    new Lokasyon { Ad = "Güney Yamaç (Tekirdağ)" },
                    new Lokasyon { Ad = "Nehir Kenarı (İpsala)" }
                });
                context.SaveChanges();
            }

            // SEZONLAR
            if (!context.Sezonlar.Any())
            {
                context.Sezonlar.AddRange(new List<Sezon>
                {
                    new Sezon { Ad = "2024 Sezonu", BaslangicTarihi = DateTime.Now.AddYears(-1), BitisTarihi = DateTime.Now.AddMonths(-6), Aktif = false },
                    new Sezon { Ad = "2025 Sezonu", BaslangicTarihi = DateTime.Now.AddMonths(-2), BitisTarihi = DateTime.Now.AddMonths(8), Aktif = true }
                });
                context.SaveChanges();
            }

            // ÜRÜNLER
            if (!context.Urunler.Any())
            {
                context.Urunler.AddRange(new List<Urun>
                {
                    new Urun { Ad = "Buğday (Ceyhan-99)" },
                    new Urun { Ad = "Ayçiçeği (Pioneer)" },
                    new Urun { Ad = "Mısır (Slajlık)" },
                    new Urun { Ad = "Çeltik (Osmancık)" }
                });
                context.SaveChanges();
            }

            // EKİPMAN DURUMLARI (HATA BURADAYDI - DÜZELTİLDİ)
            if (!context.EkipmanDurumlari.Any())
            {
                context.EkipmanDurumlari.AddRange(new List<EkipmanDurumu>
                {
                    new EkipmanDurumu { Ad = "Aktif" },
                    new EkipmanDurumu { Ad = "Bakımda" },
                    new EkipmanDurumu { Ad = "Arızalı" }
                });
                context.SaveChanges();
            }

            // GÖREV DURUMLARI
            if (!context.GorevDurumlari.Any())
            {
                context.GorevDurumlari.AddRange(new List<GorevDurumu>
                {
                    new GorevDurumu { Ad = "Bekliyor" },
                    new GorevDurumu { Ad = "Devam Ediyor" },
                    new GorevDurumu { Ad = "Tamamlandı" }
                });
                context.SaveChanges();
            }

            // DEPOLAR
            if (!context.Depolar.Any())
            {
                context.Depolar.AddRange(new List<Depo>
                {
                    new Depo { Ad = "Merkez Hangar" },
                    new Depo { Ad = "İlaç Deposu" }
                });
                context.SaveChanges();
            }

            // DEPARTMANLAR
            if (!context.Departmanlar.Any())
            {
                context.Departmanlar.AddRange(new List<Departman>
                {
                    new Departman { Ad = "Saha Operasyon" },
                    new Departman { Ad = "Teknik Bakım" },
                    new Departman { Ad = "İdari İşler" }
                });
                context.SaveChanges();
            }

            // ==========================================
            // 2. İLİŞKİSEL VERİLER
            // ==========================================

            // EKİPMANLAR
            if (!context.Ekipmanlar.Any())
            {
                // Ekipman durum ID'lerini güvenli şekilde alıyoruz
                var aktifDurum = context.EkipmanDurumlari.FirstOrDefault(x => x.Ad == "Aktif");
                var arizaliDurum = context.EkipmanDurumlari.FirstOrDefault(x => x.Ad == "Arızalı");

                // Eğer veritabanı boşsa ve yukarıdaki sorgular null dönerse patlamaması için kontrol:
                int aktifId = aktifDurum != null ? aktifDurum.Id : 1;
                int arizaliId = arizaliDurum != null ? arizaliDurum.Id : 1;

                context.Ekipmanlar.AddRange(new List<Ekipman>
                {
                    new Ekipman { Ad = "New Holland TR6", Marka = "New Holland", Model = "2023", SeriNo = "NH-100", EkipmanDurumuId = aktifId },
                    new Ekipman { Ad = "İlaçlama Makinesi", Marka = "Öntar", Model = "2018", SeriNo = "ILC-55", EkipmanDurumuId = arizaliId }
                });
                context.SaveChanges();
            }

            // TARLALAR
            if (!context.Tarlalar.Any())
            {
                var lok1 = context.Lokasyonlar.First().Id;
                var lok2 = context.Lokasyonlar.Last().Id;

                context.Tarlalar.AddRange(new List<Tarla>
                {
                    new Tarla { Ad = "Büyük Çayır", TapuAdaParsel = "105/2", AlanDonum = 60, LokasyonId = lok1 },
                    new Tarla { Ad = "Kurak Tepe", TapuAdaParsel = "108/9", AlanDonum = 25, LokasyonId = lok2 }
                });
                context.SaveChanges();
            }

            // STOKLAR
            if (!context.StokItemleri.Any())
            {
                var depoId = context.Depolar.First().Id;
                context.StokItemleri.AddRange(new List<StokItem>
                {
                    new StokItem { Ad = "Üre Gübresi", Birim = "KG", Miktar = 5000, DepoId = depoId },
                    new StokItem { Ad = "Ot İlacı", Birim = "LT", Miktar = 10, DepoId = depoId }
                });
                context.SaveChanges();
            }

            // POZİSYONLAR
            if (!context.Pozisyonlar.Any())
            {
                var dept = context.Departmanlar.First().Id;
                context.Pozisyonlar.Add(new Pozisyon { Ad = "Mühendis", DepartmanId = dept });
                context.SaveChanges();
            }

            // ==========================================
            // 3. EKİM PLANLARI (TEST İÇİN EN ÖNEMLİSİ)
            // ==========================================

            if (!context.EkimPlanlari.Any())
            {
                var tarlalar = context.Tarlalar.OrderBy(x => x.Id).ToList();
                var urun = context.Urunler.FirstOrDefault();
                var sezon = context.Sezonlar.FirstOrDefault(x => x.Aktif);

                if (tarlalar.Count >= 2 && urun != null && sezon != null)
                {
                    // 1. Durum: AKTİF EKİM (Hasat Bekleyen)
                    context.EkimPlanlari.Add(new EkimPlani
                    {
                        TarlaId = tarlalar[0].Id,
                        UrunId = urun.Id,
                        SezonId = sezon.Id,
                        EkimTarihi = DateTime.Now.AddMonths(-2),
                        HasatTarihi = null,
                        BeklenenVerimKg = 25000,
                        GerceklesenVerimKg = 0
                    });

                    // 2. Durum: BİTMİŞ EKİM (Geçmiş)
                    context.EkimPlanlari.Add(new EkimPlani
                    {
                        TarlaId = tarlalar[1].Id,
                        UrunId = urun.Id,
                        SezonId = sezon.Id,
                        EkimTarihi = DateTime.Now.AddMonths(-6),
                        HasatTarihi = DateTime.Now.AddMonths(-1),
                        BeklenenVerimKg = 10000,
                        GerceklesenVerimKg = 9500
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}