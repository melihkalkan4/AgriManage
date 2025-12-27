using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FinalVeritabaniKurulumu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Personeller_PersonelId",
                table: "Gorevler");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<double>(
                name: "BeklenenRekolte",
                table: "Urunler",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EkimTarihi",
                table: "Urunler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HasatTarihi",
                table: "Urunler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarlaId",
                table: "Urunler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 12, 17, 23, 41, 7, 844, DateTimeKind.Local).AddTicks(3033));

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeklenenRekolte", "EkimTarihi", "HasatTarihi", "TarlaId" },
                values: new object[] { 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeklenenRekolte", "EkimTarihi", "HasatTarihi", "TarlaId" },
                values: new object[] { 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BeklenenRekolte", "EkimTarihi", "HasatTarihi", "TarlaId" },
                values: new object[] { 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_TarlaId",
                table: "Urunler",
                column: "TarlaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Personeller_PersonelId",
                table: "Gorevler",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Tarlalar_TarlaId",
                table: "Urunler",
                column: "TarlaId",
                principalTable: "Tarlalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Personeller_PersonelId",
                table: "Gorevler");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Tarlalar_TarlaId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_TarlaId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "BeklenenRekolte",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "EkimTarihi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "HasatTarihi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "TarlaId",
                table: "Urunler");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Urunler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 12, 17, 21, 17, 6, 554, DateTimeKind.Local).AddTicks(388));

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Personeller_PersonelId",
                table: "Gorevler",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
