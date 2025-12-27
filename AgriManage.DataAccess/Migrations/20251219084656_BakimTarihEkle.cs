using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BakimTarihEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakimKayitlari_BakimPlanlari_BakimPlaniId",
                table: "BakimKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_BakimKayitlari_Personeller_PersonelId",
                table: "BakimKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_GorevTipleri_GorevTipiId",
                table: "Gorevler");

            migrationBuilder.RenameColumn(
                name: "TamamlanmaTarihi",
                table: "BakimKayitlari",
                newName: "Tarih");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "Gorevler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GorevTipiId",
                table: "Gorevler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Baslik",
                table: "Gorevler",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "BakimKayitlari",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BakimPlaniId",
                table: "BakimKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "BakimKayitlari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Talepler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurumId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talepler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talepler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talepler_PersonelId",
                table: "Talepler",
                column: "PersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BakimKayitlari_BakimPlanlari_BakimPlaniId",
                table: "BakimKayitlari",
                column: "BakimPlaniId",
                principalTable: "BakimPlanlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BakimKayitlari_Personeller_PersonelId",
                table: "BakimKayitlari",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_GorevTipleri_GorevTipiId",
                table: "Gorevler",
                column: "GorevTipiId",
                principalTable: "GorevTipleri",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakimKayitlari_BakimPlanlari_BakimPlaniId",
                table: "BakimKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_BakimKayitlari_Personeller_PersonelId",
                table: "BakimKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_GorevTipleri_GorevTipiId",
                table: "Gorevler");

            migrationBuilder.DropTable(
                name: "Talepler");

            migrationBuilder.RenameColumn(
                name: "Tarih",
                table: "BakimKayitlari",
                newName: "TamamlanmaTarihi");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GorevTipiId",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Baslik",
                table: "Gorevler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "BakimKayitlari",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BakimPlaniId",
                table: "BakimKayitlari",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "BakimKayitlari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BakimKayitlari_BakimPlanlari_BakimPlaniId",
                table: "BakimKayitlari",
                column: "BakimPlaniId",
                principalTable: "BakimPlanlari",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BakimKayitlari_Personeller_PersonelId",
                table: "BakimKayitlari",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_GorevTipleri_GorevTipiId",
                table: "Gorevler",
                column: "GorevTipiId",
                principalTable: "GorevTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
