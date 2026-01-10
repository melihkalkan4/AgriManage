using AgriManage.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriManage.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- TABLOLAR ---
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Pozisyon> Pozisyonlar { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Vardiya> Vardiyalar { get; set; }
        public DbSet<PersonelVardiya> PersonelVardiyalari { get; set; }
        public DbSet<YetkiAlani> YetkiAlanlari { get; set; }
        public DbSet<PozisyonYetki> PozisyonYetkileri { get; set; }
        public DbSet<Talep> Talepler { get; set; }

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

        public DbSet<EkipmanKategorisi> EkipmanKategorileri { get; set; }
        public DbSet<EkipmanDurumu> EkipmanDurumlari { get; set; }
        public DbSet<Ekipman> Ekipmanlar { get; set; }
        public DbSet<EkipmanLog> EkipmanLoglari { get; set; }
        public DbSet<BakimTipi> BakimTipleri { get; set; }
        public DbSet<BakimPlani> BakimPlanlari { get; set; }
        public DbSet<BakimKaydi> BakimKayitlari { get; set; }

        public DbSet<Depo> Depolar { get; set; }
        public DbSet<Tedarikci> Tedarikciler { get; set; }
        public DbSet<StokKategorisi> StokKategorileri { get; set; }
        public DbSet<StokItem> StokItemleri { get; set; }
        public DbSet<StokHareket> StokHareketleri { get; set; }

        public DbSet<GorevTipi> GorevTipleri { get; set; }
        public DbSet<GorevDurumu> GorevDurumlari { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<GorevLog> GorevLoglari { get; set; }
        public DbSet<OperasyonelRapor> OperasyonelRaporlar { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Identity (Kullanıcı) tablolarını oluşturur. BU SATIR ŞARTTIR.
            base.OnModelCreating(builder);

            // --- HASSASİYET AYARLARI (Decimal tipler için) ---
            builder.Entity<Tarla>().Property(t => t.AlanDonum).HasColumnType("decimal(18,2)");
            builder.Entity<StokHareket>().Property(s => s.BirimFiyat).HasColumnType("decimal(18,2)");
            builder.Entity<StokHareket>().Property(sh => sh.Miktar).HasColumnType("decimal(18,2)");
            builder.Entity<StokItem>().Property(si => si.Miktar).HasColumnType("decimal(18,2)");
            builder.Entity<BakimKaydi>().Property(b => b.Maliyet).HasColumnType("decimal(18,2)");
            builder.Entity<Gorev>().Property(g => g.PlanlananStokMiktari).HasColumnType("decimal(18,2)");

            // --- İLİŞKİ YAPILANDIRMALARI (Cascade Delete Hatalarını Önlemek İçin) ---

            // Ürün silinirse Tarla silinmesin
            builder.Entity<Urun>()
                .HasOne(u => u.Tarla)
                .WithMany(t => t.Urunler)
                .HasForeignKey(u => u.TarlaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Görev silinirse Personel silinmesin
            builder.Entity<Gorev>()
                .HasOne(g => g.Personel)
                .WithMany(p => p.AtananGorevler)
                .HasForeignKey(g => g.PersonelId)
                .OnDelete(DeleteBehavior.Restrict);

            // StokItem silinirse Depo silinmesin (Döngü hatasını önler)
            builder.Entity<StokItem>()
                .HasOne(s => s.Depo)
                .WithMany(d => d.StokItemleri)
                .HasForeignKey(s => s.DepoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}