using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgriManage.DataAccess.Data
{
    public static class DbSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            // 1. Veritabanı yoksa oluştur (Migration kullanıyorsan burayı kapatabilirsin)
            // context.Database.EnsureCreated(); 

            // ==========================================
            // 2. ROLLERİ EKLE
            // ==========================================
            string[] roller = {
                "Admin", "Ciftci", "User", "ZIRAATMUHENDISI",
                "YONETICI", "TRAKTOROPERATORU", "BAKIMGOREVLISI",
                "BAKIMTEKNISYENI", "DEPOSORUMLUSU"
            };

            foreach (var rol in roller)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                    await roleManager.CreateAsync(new IdentityRole(rol));
            }

            // ==========================================
            // 3. LOOKUP TABLOLARI
            // ==========================================

            // A) DEPARTMANLAR
            if (!context.Departmanlar.Any())
            {
                context.Departmanlar.AddRange(
                    new Departman { Ad = "Saha Operasyon" },
                    new Departman { Ad = "Teknik Bakım" },
                    new Departman { Ad = "Yönetim" }
                );
                await context.SaveChangesAsync();
            }

            // B) BÖLGELER (DÜZELTİLEN KISIM: Sehir alanı kaldırıldı)
            if (!context.Bolgeler.Any())
            {
                context.Bolgeler.AddRange(
                    new Bolge { Ad = "Trakya Bölgesi" },
                    new Bolge { Ad = "Ege Bölgesi" }
                );
                await context.SaveChangesAsync();
            }

            // C) LOKASYONLAR
            if (!context.Lokasyonlar.Any())
            {
                var bolge = context.Bolgeler.FirstOrDefault();
                if (bolge != null)
                {
                    context.Lokasyonlar.AddRange(
                        new Lokasyon { Ad = "Merkez Çiftlik", Adres = "Tekirdağ Mrk", BolgeId = bolge.Id },
                        new Lokasyon { Ad = "Kuzey Arazisi", Adres = "Edirne Yolu", BolgeId = bolge.Id }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // ==========================================
            // 4. KULLANICILAR VE PERSONEL
            // ==========================================

            // Pozisyonlar
            if (!context.Pozisyonlar.Any())
            {
                var dep = context.Departmanlar.FirstOrDefault();
                if (dep != null)
                {
                    context.Pozisyonlar.AddRange(
                        new Pozisyon { Ad = "Mühendis", DepartmanId = dep.Id },
                        new Pozisyon { Ad = "Operatör", DepartmanId = dep.Id }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // Admin Kullanıcısı
            var adminUser = await userManager.FindByEmailAsync("MelihTest@gmail.com");
            if (adminUser == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "MelihTest@gmail.com",
                    Email = "MelihTest@gmail.com",
                    TamAd = "Melih Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");

                    var poz = context.Pozisyonlar.FirstOrDefault();
                    if (poz != null)
                    {
                        context.Personeller.Add(new Personel
                        {
                            ApplicationUserId = newUser.Id,
                            SicilNo = "ADMIN-001",
                            IseBaslamaTarihi = DateTime.Now,
                            PozisyonId = poz.Id
                        });
                        await context.SaveChangesAsync();
                    }
                }
                adminUser = await userManager.FindByEmailAsync("MelihTest@gmail.com");
            }

            // ==========================================
            // 5. TARLALAR
            // ==========================================
            if (!context.Tarlalar.Any())
            {
                var lok = context.Lokasyonlar.FirstOrDefault();

                if (lok != null && adminUser != null)
                {
                    context.Tarlalar.AddRange(
                        new Tarla { Ad = "Büyük Ova", AlanDonum = 120.5m, LokasyonId = lok.Id, TapuAdaParsel = "101/1", ApplicationUserId = adminUser.Id },
                        new Tarla { Ad = "Dere Kenarı", AlanDonum = 85.0m, LokasyonId = lok.Id, TapuAdaParsel = "102/5", ApplicationUserId = adminUser.Id }
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}