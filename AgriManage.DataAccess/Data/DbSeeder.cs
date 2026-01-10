using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriManage.DataAccess.Data
{
    public static class DbSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            // 1. Veritabanını Garantiye Al
            context.Database.EnsureCreated();

            // ==========================================
            // 2. ROLLERİ EKLE
            // ==========================================
            string[] roller = { "Admin", "ZiraatMuhendisi", "Operator", "Ciftci", "User" };
            foreach (var rol in roller)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                    await roleManager.CreateAsync(new IdentityRole(rol));
            }

            // ==========================================
            // 3. LOOKUP TABLOLARI (ADIM ADIM KAYIT)
            // ==========================================

            // A) DEPARTMANLAR
            if (!context.Departmanlar.Any())
            {
                context.Departmanlar.AddRange(
                    new Departman { Ad = "Saha Operasyon" },
                    new Departman { Ad = "Teknik Bakım" },
                    new Departman { Ad = "Yönetim" }
                );
                await context.SaveChangesAsync(); // HATA OLMASIN DİYE BURADA KAYDEDİYORUZ
            }

            // B) BÖLGELER (Önce Bölge eklenmeli!)
            if (!context.Bolgeler.Any())
            {
                context.Bolgeler.AddRange(
                    new Bolge { Ad = "Trakya Bölgesi" },
                    new Bolge { Ad = "Ege Bölgesi" }
                );
                await context.SaveChangesAsync(); // KAYDET Kİ ID OLUŞSUN!
            }

            // C) LOKASYONLAR (Artık Bölge ID'si veritabanında var)
            if (!context.Lokasyonlar.Any())
            {
                // Veritabanından gerçek bir Bölge ID'si çekiyoruz
                // First yerine FirstOrDefault kullanıyoruz ki hata patlamasın
                var bolge = context.Bolgeler.FirstOrDefault();

                if (bolge != null)
                {
                    context.Lokasyonlar.AddRange(
                        new Lokasyon { Ad = "Merkez Çiftlik", Adres = "Tekirdağ", BolgeId = bolge.Id },
                        new Lokasyon { Ad = "Kuzey Arazisi", Adres = "Edirne", BolgeId = bolge.Id }
                    );
                    await context.SaveChangesAsync(); // Lokasyonları kaydet
                }
            }

            // ==========================================
            // 4. KULLANICILAR VE PERSONEL
            // ==========================================

            // Pozisyonlar (Departman ID lazım)
            if (!context.Pozisyonlar.Any())
            {
                var dep = context.Departmanlar.FirstOrDefault();
                if (dep != null)
                {
                    context.Pozisyonlar.Add(new Pozisyon { Ad = "Mühendis", DepartmanId = dep.Id });
                    context.Pozisyonlar.Add(new Pozisyon { Ad = "Operatör", DepartmanId = dep.Id });
                    await context.SaveChangesAsync();
                }
            }

            // Kullanıcı Ekleme
            // Not: Kullanıcı zaten varsa tekrar eklemeye çalışmaz
            var pozisyon = context.Pozisyonlar.FirstOrDefault();

            if (pozisyon != null)
            {
                var usersToCheck = new List<(string Email, string Ad, string Rol)>
                {
                    ("admin@agri.com", "Melih Kalkan", "Admin"),
                    ("ali@agri.com", "Ali Yılmaz", "ZiraatMuhendisi"),
                    ("ayse@agri.com", "Ayşe Demir", "Operator")
                };

                foreach (var u in usersToCheck)
                {
                    var existingUser = await userManager.FindByEmailAsync(u.Email);
                    if (existingUser == null)
                    {
                        var newUser = new ApplicationUser
                        {
                            UserName = u.Email,
                            Email = u.Email,
                            TamAd = u.Ad,
                            EmailConfirmed = true
                        };
                        var result = await userManager.CreateAsync(newUser, "Agri123!");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newUser, u.Rol);

                            // Personel kaydı
                            context.Personeller.Add(new Personel
                            {
                                ApplicationUserId = newUser.Id,
                                SicilNo = "P-" + new Random().Next(100, 999),
                                IseBaslamaTarihi = DateTime.Now.AddYears(-1),
                                PozisyonId = pozisyon.Id
                            });
                        }
                    }
                    else
                    {
                        // Kullanıcı var ama Personel kaydı yoksa ekle
                        if (!context.Personeller.Any(p => p.ApplicationUserId == existingUser.Id))
                        {
                            context.Personeller.Add(new Personel
                            {
                                ApplicationUserId = existingUser.Id,
                                SicilNo = "P-" + new Random().Next(100, 999),
                                IseBaslamaTarihi = DateTime.Now.AddYears(-1),
                                PozisyonId = pozisyon.Id
                            });
                        }
                    }
                }
                // Kullanıcı/Personel döngüsü bitince kaydet
                await context.SaveChangesAsync();
            }

            // ==========================================
            // 5. TARLALAR (Lokasyon ID lazım)
            // ==========================================
            if (!context.Tarlalar.Any())
            {
                var lok = context.Lokasyonlar.FirstOrDefault();
                if (lok != null)
                {
                    context.Tarlalar.AddRange(
                        new Tarla { Ad = "Büyük Ova", AlanDonum = 120.5m, LokasyonId = lok.Id, TapuAdaParsel = "101/1" },
                        new Tarla { Ad = "Dere Kenarı", AlanDonum = 85.0m, LokasyonId = lok.Id, TapuAdaParsel = "102/5" },
                        new Tarla { Ad = "Sera Bölgesi", AlanDonum = 15.0m, LokasyonId = lok.Id, TapuAdaParsel = "103/9" }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // ==========================================
            // 6. GÖREVLER (Personel, Tarla ve Lookup lazım)
            // ==========================================
            if (!context.Gorevler.Any())
            {
                // Önce Lookup'ları ekle ve KAYDET
                if (!context.GorevDurumlari.Any())
                {
                    context.GorevDurumlari.AddRange(
                        new GorevDurumu { Ad = "Bekliyor" },
                        new GorevDurumu { Ad = "Devam Ediyor" },
                        new GorevDurumu { Ad = "Tamamlandı" }
                    );
                    await context.SaveChangesAsync();
                }

                if (!context.GorevTipleri.Any())
                {
                    context.GorevTipleri.AddRange(new GorevTipi { Ad = "Ekim" }, new GorevTipi { Ad = "Hasat" });
                    await context.SaveChangesAsync();
                }

                // Şimdi verileri çekip görev oluşturabiliriz
                var personeller = context.Personeller.ToList();
                var tarlalar = context.Tarlalar.ToList();
                var durumTamam = context.GorevDurumlari.FirstOrDefault(x => x.Ad == "Tamamlandı");

                if (personeller.Any() && tarlalar.Any() && durumTamam != null)
                {
                    var gorevListesi = new List<Gorev>();
                    var rnd = new Random();

                    foreach (var p in personeller)
                    {
                        int sayi = rnd.Next(5, 10);
                        for (int i = 0; i < sayi; i++)
                        {
                            gorevListesi.Add(new Gorev
                            {
                                PersonelId = p.Id,
                                TarlaId = tarlalar[rnd.Next(tarlalar.Count)].Id,
                                GorevDurumuId = durumTamam.Id,
                                Baslik = "Görev #" + rnd.Next(1000),
                                Aciklama = "Otomatik veri.",
                                PlanlananBaslangic = DateTime.Now.AddDays(-rnd.Next(10, 60)),
                                TamamlanmaTarihi = DateTime.Now.AddDays(-rnd.Next(1, 9)),
                                OlusturmaTarihi = DateTime.Now // SQL hatası almamak için
                            });
                        }
                    }
                    context.Gorevler.AddRange(gorevListesi);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}