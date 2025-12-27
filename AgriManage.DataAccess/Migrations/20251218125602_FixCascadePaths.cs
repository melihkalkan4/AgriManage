using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BakimKayitlari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BakimPlanlari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BakimTipleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EkimPlanlari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EkimPlanlari",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GorevDurumlari",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GorevLoglari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GorevLoglari",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GorevLoglari",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GorevLoglari",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GorevTipleri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GorevTipleri",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PersonelVardiyalari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonelVardiyalari",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PersonelVardiyalari",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PozisyonYetkileri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PozisyonYetkileri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PozisyonYetkileri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PozisyonYetkileri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seralar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StokHareketleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StokHareketleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StokHareketleri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StokHareketleri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TarlaAraziTipleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TarlaAraziTipleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AraziTipleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AraziTipleri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BakimTipleri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OperasyonelRaporlar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sezonlar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StokKategorileri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tedarikciler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vardiyalar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vardiyalar",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "YetkiAlanlari",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "YetkiAlanlari",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "YetkiAlanlari",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "HareketTipi",
                table: "StokHareketleri",
                newName: "IslemTipi");

            migrationBuilder.AlterColumn<string>(
                name: "YetkiliKisi",
                table: "Tedarikciler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Tedarikciler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Tedarikciler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepoId",
                table: "StokItemleri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Miktar",
                table: "StokItemleri",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "StokHareketleri",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DepoId", "Miktar" },
                values: new object[] { 1, 500m });

            migrationBuilder.UpdateData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DepoId", "Miktar" },
                values: new object[] { 1, 1000m });

            migrationBuilder.UpdateData(
                table: "Tedarikciler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ad", "Adres", "Telefon" },
                values: new object[] { "Anadolu Tarım A.Ş.", "Lüleburgaz Sanayi Sitesi", "0532 123 45 67" });

            migrationBuilder.CreateIndex(
                name: "IX_StokItemleri_DepoId",
                table: "StokItemleri",
                column: "DepoId");

            migrationBuilder.AddForeignKey(
                name: "FK_StokItemleri_Depolar_DepoId",
                table: "StokItemleri",
                column: "DepoId",
                principalTable: "Depolar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StokItemleri_Depolar_DepoId",
                table: "StokItemleri");

            migrationBuilder.DropIndex(
                name: "IX_StokItemleri_DepoId",
                table: "StokItemleri");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Tedarikciler");

            migrationBuilder.DropColumn(
                name: "DepoId",
                table: "StokItemleri");

            migrationBuilder.DropColumn(
                name: "Miktar",
                table: "StokItemleri");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "StokHareketleri");

            migrationBuilder.RenameColumn(
                name: "IslemTipi",
                table: "StokHareketleri",
                newName: "HareketTipi");

            migrationBuilder.AlterColumn<string>(
                name: "YetkiliKisi",
                table: "Tedarikciler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Tedarikciler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "AraziTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Killi Toprak" },
                    { 2, "Kumlu Toprak" }
                });

            migrationBuilder.InsertData(
                table: "BakimKayitlari",
                columns: new[] { "Id", "Aciklama", "BakimPlaniId", "Maliyet", "PersonelId", "TamamlanmaTarihi" },
                values: new object[] { 1, "Sol arka lastik değişimi yapıldı.", null, 4500.00m, 4, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BakimTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Periyodik Yağ Değişimi" },
                    { 2, "Arıza Onarımı" }
                });

            migrationBuilder.InsertData(
                table: "EkipmanLoglari",
                columns: new[] { "Id", "Aciklama", "EkipmanId", "Tarih" },
                values: new object[] { 1, "Ekipman sisteme eklendi.", 1, new DateTime(2025, 12, 17, 23, 41, 7, 844, DateTimeKind.Local).AddTicks(3033) });

            migrationBuilder.InsertData(
                table: "GorevDurumlari",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 4, "İptal Edildi" });

            migrationBuilder.InsertData(
                table: "GorevTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 4, "Sulama" },
                    { 5, "Hasat" }
                });

            migrationBuilder.InsertData(
                table: "Gorevler",
                columns: new[] { "Id", "Aciklama", "Baslik", "GorevDurumuId", "GorevTipiId", "OlusturmaTarihi", "PersonelId", "PlanlananBaslangic", "TamamlanmaTarihi", "TarlaId" },
                values: new object[,]
                {
                    { 1, "Ceyhan-99 tohumları kullanılarak ekim yapılacak.", "A1 Tarlası Buğday Ekimi", 3, 1, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Ayçiçeği için DAP gübresi atılacak.", "B2 Tarlası Gübreleme", 1, 2, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Seralar",
                columns: new[] { "Id", "Ad", "AlanMetrekare", "LokasyonId" },
                values: new object[] { 1, "Fide Serası", 500m, 1 });

            migrationBuilder.InsertData(
                table: "Sezonlar",
                columns: new[] { "Id", "Ad", "BaslangicTarihi", "BitisTarihi" },
                values: new object[] { 1, "2024 Sonbahar Ekimi", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "StokHareketleri",
                columns: new[] { "Id", "BirimFiyat", "DepoId", "HareketTipi", "Miktar", "OperasyonelRaporId", "StokItemId", "Tarih", "TedarikciId" },
                values: new object[,]
                {
                    { 1, 22.5m, 1, "Giriş", 5000m, null, 1, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 25.0m, 1, "Giriş", 10000m, null, 2, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "StokItemleri",
                columns: new[] { "Id", "Ad", "Birim", "StokKategorisiId" },
                values: new object[] { 3, "Ceyhan-99 Buğday Tohumu", "KG", 2 });

            migrationBuilder.InsertData(
                table: "StokKategorileri",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 3, "Zirai İlaçlar" });

            migrationBuilder.UpdateData(
                table: "Tedarikciler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ad", "Telefon" },
                values: new object[] { "Gübretaş A.Ş.", "02120000001" });

            migrationBuilder.InsertData(
                table: "Tedarikciler",
                columns: new[] { "Id", "Ad", "Telefon", "YetkiliKisi" },
                values: new object[] { 2, "Tohumcular Ltd.", "02120000002", "Zeynep Kaya" });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "Id", "Ad", "BeklenenRekolte", "EkimTarihi", "HasatTarihi", "TarlaId", "UrunKategorisiId" },
                values: new object[] { 3, "Kanola", 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 2 });

            migrationBuilder.InsertData(
                table: "Vardiyalar",
                columns: new[] { "Id", "Ad", "BaslangicSaati", "BitisSaati" },
                values: new object[,]
                {
                    { 1, "Sabah (08:00 - 16:00)", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 16, 0, 0, 0) },
                    { 2, "Akşam (16:00 - 00:00)", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "YetkiAlanlari",
                columns: new[] { "Id", "ModulAdi", "YetkiTipi" },
                values: new object[,]
                {
                    { 1, "StokYonetimi", "TamErisim" },
                    { 2, "GorevYonetimi", "Okuma" },
                    { 3, "GorevYonetimi", "Yazma" }
                });

            migrationBuilder.InsertData(
                table: "BakimPlanlari",
                columns: new[] { "Id", "BakimTipiId", "EkipmanId", "PlanlananTarih", "Tamamlandi" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "EkimPlanlari",
                columns: new[] { "Id", "BeklenenVerimKg", "EkimTarihi", "GerceklesenVerimKg", "HasatTarihi", "SezonId", "TarlaId", "UrunId" },
                values: new object[,]
                {
                    { 1, 60000m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 1, 1, 1 },
                    { 2, 30000m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 1, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "GorevLoglari",
                columns: new[] { "Id", "Aciklama", "GorevId", "Tarih" },
                values: new object[,]
                {
                    { 1, "Görev 'Atandı' durumunda oluşturuldu.", 1, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Görev 'Devam Ediyor' durumuna alındı.", 1, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Görev 'Tamamlandı' durumuna alındı.", 1, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Görev 'Atandı' durumunda oluşturuldu.", 2, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OperasyonelRaporlar",
                columns: new[] { "Id", "GorevId", "RaporOzeti", "RaporTarihi" },
                values: new object[] { 1, 1, "150 dönüm tarla başarıyla ekildi. 8000 KG Ceyhan-99 tohumu kullanıldı.", new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PersonelVardiyalari",
                columns: new[] { "Id", "Gun", "PersonelId", "VardiyaId" },
                values: new object[,]
                {
                    { 1, 1, 3, 1 },
                    { 2, 2, 3, 1 },
                    { 3, 1, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "PozisyonYetkileri",
                columns: new[] { "Id", "PozisyonId", "YetkiAlaniId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 5, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "StokHareketleri",
                columns: new[] { "Id", "BirimFiyat", "DepoId", "HareketTipi", "Miktar", "OperasyonelRaporId", "StokItemId", "Tarih", "TedarikciId" },
                values: new object[] { 3, 15.0m, 2, "Giriş", 8000m, null, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "StokItemleri",
                columns: new[] { "Id", "Ad", "Birim", "StokKategorisiId" },
                values: new object[] { 4, "Yabancı Ot İlacı", "LT", 3 });

            migrationBuilder.InsertData(
                table: "TarlaAraziTipleri",
                columns: new[] { "Id", "AraziTipiId", "TarlaId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "StokHareketleri",
                columns: new[] { "Id", "BirimFiyat", "DepoId", "HareketTipi", "Miktar", "OperasyonelRaporId", "StokItemId", "Tarih", "TedarikciId" },
                values: new object[] { 4, 15.0m, 2, "Sarf", 8000m, 1, 3, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
