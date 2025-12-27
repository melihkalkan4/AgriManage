using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StokYeniAlanlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "StokItemleri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "KritikStokSeviyesi",
                table: "StokItemleri",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RafNo",
                table: "StokItemleri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Aciklama", "KritikStokSeviyesi", "RafNo" },
                values: new object[] { null, 0m, null });

            migrationBuilder.UpdateData(
                table: "StokItemleri",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aciklama", "KritikStokSeviyesi", "RafNo" },
                values: new object[] { null, 0m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "StokItemleri");

            migrationBuilder.DropColumn(
                name: "KritikStokSeviyesi",
                table: "StokItemleri");

            migrationBuilder.DropColumn(
                name: "RafNo",
                table: "StokItemleri");
        }
    }
}
