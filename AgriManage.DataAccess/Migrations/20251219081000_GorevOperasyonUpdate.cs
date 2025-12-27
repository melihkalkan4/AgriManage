using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GorevOperasyonUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EkipmanId",
                table: "Gorevler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlanlananStokMiktari",
                table: "Gorevler",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StokItemId",
                table: "Gorevler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TahminiSureSaat",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MevcutCalismaSaati",
                table: "Ekipmanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ekipmanlar",
                keyColumn: "Id",
                keyValue: 1,
                column: "MevcutCalismaSaati",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ekipmanlar",
                keyColumn: "Id",
                keyValue: 2,
                column: "MevcutCalismaSaati",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_EkipmanId",
                table: "Gorevler",
                column: "EkipmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_StokItemId",
                table: "Gorevler",
                column: "StokItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Ekipmanlar_EkipmanId",
                table: "Gorevler",
                column: "EkipmanId",
                principalTable: "Ekipmanlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_StokItemleri_StokItemId",
                table: "Gorevler",
                column: "StokItemId",
                principalTable: "StokItemleri",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Ekipmanlar_EkipmanId",
                table: "Gorevler");

            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_StokItemleri_StokItemId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_EkipmanId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_StokItemId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "EkipmanId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "PlanlananStokMiktari",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "StokItemId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "TahminiSureSaat",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "MevcutCalismaSaati",
                table: "Ekipmanlar");
        }
    }
}
