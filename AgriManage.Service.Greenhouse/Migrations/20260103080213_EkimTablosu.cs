using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.Service.Greenhouse.Migrations
{
    /// <inheritdoc />
    public partial class EkimTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ekimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeraId = table.Column<int>(type: "int", nullable: false),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EkimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdetVeyaM2 = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekimler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ekimler");
        }
    }
}
