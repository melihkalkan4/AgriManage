using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AgriManage.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================================================
        // MODÜL 1: KİMLİK VE PERSONEL YÖNETİMİ
        // =========================================================
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Pozisyon> Pozisyonlar { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Vardiya> Vardiyalar { get; set; }
        public DbSet<PersonelVardiya> PersonelVardiyalari { get; set; }
        public DbSet<YetkiAlani> YetkiAlanlari { get; set; }
        public DbSet<PozisyonYetki> PozisyonYetkileri { get; set; }
        public DbSet<Talep> Talepler { get; set; }

        // =========================================================
        // MODÜL 2: VARLIK VE KAYNAK YÖNETİMİ
        // =========================================================
        public DbSet<Bolge> Bolgeler { get; set; }
        public DbSet<Lokasyon> Lokasyonlar { get; set; }
        public DbSet<Tarla> Tarlalar { get; set; }
        public DbSet<AraziTipi> AraziTipleri { get; set; }
        public DbSet<TarlaAraziTipi> TarlaAraziTipleri { get; set; }
        public DbSet<UrunKategorisi> UrunKategorileri { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Sezon> Sezonlar { get; set; }
        public DbSet<EkimPlani> EkimPlanlari { get; set; }
        public DbSet<Sera> Seralar { get; set; }

        // =========================================================
        // MODÜL 3: EKİPMAN VE BAKIM YÖNETİMİ
        // =========================================================
        public DbSet<EkipmanKategorisi> EkipmanKategorileri { get; set; }
        public DbSet<EkipmanDurumu> EkipmanDurumlari { get; set; }
        public DbSet<Ekipman> Ekipmanlar { get; set; }
        public DbSet<EkipmanLog> EkipmanLoglari { get; set; }
        public DbSet<BakimTipi> BakimTipleri { get; set; }
        public DbSet<BakimPlani> BakimPlanlari { get; set; }
        public DbSet<BakimKaydi> BakimKayitlari { get; set; }

        // =========================================================
        // MODÜL 4: STOK VE ENVANTER YÖNETİMİ
        // =========================================================
        public DbSet<Depo> Depolar { get; set; }
        public DbSet<Tedarikci> Tedarikciler { get; set; }
        public DbSet<StokKategorisi> StokKategorileri { get; set; }
        public DbSet<StokItem> StokItemleri { get; set; }
        public DbSet<StokHareket> StokHareketleri { get; set; }

        // =========================================================
        // MODÜL 5: OPERASYON VE GÖREV YÖNETİMİ
        // =========================================================
        public DbSet<GorevTipi> GorevTipleri { get; set; }
        public DbSet<GorevDurumu> GorevDurumlari { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<GorevLog> GorevLoglari { get; set; }
        public DbSet<OperasyonelRapor> OperasyonelRaporlar { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // --- 1. Ondalık Alanlar İçin Hassasiyet Ayarları ---
            builder.Entity<Tarla>().Property(t => t.AlanDonum).HasColumnType("decimal(18,2)");
            builder.Entity<StokHareket>().Property(s => s.BirimFiyat).HasColumnType("decimal(18,2)");
            builder.Entity<StokHareket>().Property(sh => sh.Miktar).HasColumnType("decimal(18,2)");
            builder.Entity<StokItem>().Property(si => si.Miktar).HasColumnType("decimal(18,2)");
            builder.Entity<BakimKaydi>().Property(b => b.Maliyet).HasColumnType("decimal(18,2)");

            // --- 2. İlişki Yapılandırmaları ---
            builder.Entity<Urun>()
                .HasOne(u => u.Tarla)
                .WithMany(t => t.Urunler)
                .HasForeignKey(u => u.TarlaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Gorev>()
                .HasOne(g => g.Personel)
                .WithMany(p => p.AtananGorevler)
                .HasForeignKey(g => g.PersonelId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- 3. Veri Tohumlama (Data Seeding) ---

            // Roller
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "a1e1a1a1-1a1a-1a1a-1a1a-1a1a1a1a1a1a", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "b2b2b2b2-2b2b-2b2b-2b2b-2b2b2b2b2b2b", Name = "Kullanici", NormalizedName = "KULLANICI" }
            );

            // Departmanlar
            builder.Entity<Departman>().HasData(
                new Departman { Id = 1, Ad = "Ziraat Operasyonları" },
                new Departman { Id = 2, Ad = "Teknik Bakım" },
                new Departman { Id = 3, Ad = "Stok ve Lojistik" },
                new Departman { Id = 4, Ad = "Yönetim" }
            );

            // Pozisyonlar
            builder.Entity<Pozisyon>().HasData(
                new Pozisyon { Id = 1, Ad = "Yönetici", DepartmanId = 4 },
                new Pozisyon { Id = 2, Ad = "Ziraat Mühendisi", DepartmanId = 1 },
                new Pozisyon { Id = 3, Ad = "Traktör Operatörü", DepartmanId = 1 },
                new Pozisyon { Id = 4, Ad = "Bakım Teknisyeni", DepartmanId = 2 },
                new Pozisyon { Id = 5, Ad = "Depo Sorumlusu", DepartmanId = 3 }
            );

            // Personel
            builder.Entity<Personel>().HasData(
                new Personel { Id = 1, SicilNo = "P-001", IseBaslamaTarihi = new DateTime(2023, 1, 15), PozisyonId = 1 },
                new Personel { Id = 2, SicilNo = "P-002", IseBaslamaTarihi = new DateTime(2023, 3, 10), PozisyonId = 2 },
                new Personel { Id = 3, SicilNo = "P-003", IseBaslamaTarihi = new DateTime(2023, 5, 20), PozisyonId = 3 },
                new Personel { Id = 4, SicilNo = "P-004", IseBaslamaTarihi = new DateTime(2023, 2, 1), PozisyonId = 4 },
                new Personel { Id = 5, SicilNo = "P-005", IseBaslamaTarihi = new DateTime(2023, 4, 5), PozisyonId = 5 }
            );

            // Lokasyonlar ve Depolar
            builder.Entity<Bolge>().HasData(new Bolge { Id = 1, Ad = "Trakya Bölgesi" });
            builder.Entity<Lokasyon>().HasData(
                new Lokasyon { Id = 1, Ad = "Merkez Çiftlik (Lüleburgaz)", Adres = "Lüleburgaz Yolu Üzeri", BolgeId = 1 },
                new Lokasyon { Id = 2, Ad = "Tekirdağ Depo", Adres = "Tekirdağ Liman", BolgeId = 1 }
            );

            builder.Entity<Depo>().HasData(
                new Depo { Id = 1, Ad = "Ana Gübre Deposu", LokasyonId = 1 },
                new Depo { Id = 2, Ad = "Zirai İlaç Deposu", LokasyonId = 1 },
                new Depo { Id = 3, Ad = "Lojistik Merkezi", LokasyonId = 2 }
            );

            // Tedarikçiler
            builder.Entity<Tedarikci>().HasData(new Tedarikci
            {
                Id = 1,
                Ad = "Anadolu Tarım A.Ş.",
                YetkiliKisi = "Ahmet Yılmaz",
                Telefon = "0532 123 45 67",
                Adres = "Lüleburgaz Sanayi Sitesi"
            });

            // Tarlalar ve Ürünler
            builder.Entity<Tarla>().HasData(
                new Tarla { Id = 1, Ad = "Ana Tarla - A1", AlanDonum = 150, TapuAdaParsel = "101/1", LokasyonId = 1 },
                new Tarla { Id = 2, Ad = "Dere Kenarı - B2", AlanDonum = 85, TapuAdaParsel = "102/3", LokasyonId = 1 }
            );

            builder.Entity<UrunKategorisi>().HasData(
                new UrunKategorisi { Id = 1, Ad = "Tahıllar" },
                new UrunKategorisi { Id = 2, Ad = "Yağlı Tohumlar" }
            );

            builder.Entity<Urun>().HasData(
                new Urun { Id = 1, Ad = "Buğday (Ceyhan-99)", UrunKategorisiId = 1, TarlaId = 1 },
                new Urun { Id = 2, Ad = "Ayçiçeği (Tunka)", UrunKategorisiId = 2, TarlaId = 2 }
            );

            // Ekipman
            builder.Entity<EkipmanKategorisi>().HasData(
                new EkipmanKategorisi { Id = 1, Ad = "Traktörler" },
                new EkipmanKategorisi { Id = 2, Ad = "Ekim Ekipmanları" }
            );

            builder.Entity<EkipmanDurumu>().HasData(
                new EkipmanDurumu { Id = 1, Ad = "Aktif" },
                new EkipmanDurumu { Id = 2, Ad = "Bakımda" },
                new EkipmanDurumu { Id = 3, Ad = "Arızalı" }
            );

            builder.Entity<Ekipman>().HasData(
                new Ekipman { Id = 1, Ad = "Büyük Kırmızı Traktör", Marka = "Case IH", Model = "Puma 240", SeriNo = "TR-001", EkipmanKategorisiId = 1, EkipmanDurumuId = 1 },
                new Ekipman { Id = 2, Ad = "Ekim Mibzeri", Marka = "Paksan", Model = "24'lü", SeriNo = "TR-002", EkipmanKategorisiId = 2, EkipmanDurumuId = 1 }
            );

            // Görevler
            builder.Entity<GorevTipi>().HasData(
                new GorevTipi { Id = 1, Ad = "Ekim" },
                new GorevTipi { Id = 2, Ad = "Gübreleme" },
                new GorevTipi { Id = 3, Ad = "İlaçlama" }
            );

            builder.Entity<GorevDurumu>().HasData(
                new GorevDurumu { Id = 1, Ad = "Atandı" },
                new GorevDurumu { Id = 2, Ad = "Devam Ediyor" },
                new GorevDurumu { Id = 3, Ad = "Tamamlandı" }
            );

            // Stok Kalemleri
            builder.Entity<StokKategorisi>().HasData(
                new StokKategorisi { Id = 1, Ad = "Gübreler" },
                new StokKategorisi { Id = 2, Ad = "Tohumlar" }
            );

            builder.Entity<StokItem>().HasData(
                new StokItem { Id = 1, Ad = "DAP Gübresi", Birim = "KG", StokKategorisiId = 1, Miktar = 500, DepoId = 1 },
                new StokItem { Id = 2, Ad = "ÜRE Gübresi", Birim = "KG", StokKategorisiId = 1, Miktar = 1000, DepoId = 1 }
            );
            // SQL Döngü Hatasını Çözen Kritik Tanımlama
            builder.Entity<StokItem>()
                .HasOne(s => s.Depo)
                .WithMany(d => d.StokItemleri) // Depo modelinde ICollection<StokItem> StokItemleri olmalı
                .HasForeignKey(s => s.DepoId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict yaparak hatayı çözüyoruz
        }
    }
}