using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.Service.Greenhouse.Migrations
{
    /// <inheritdoc />
    public partial class HasatEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hasatlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeraId = table.Column<int>(type: "int", nullable: false),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiktarKg = table.Column<double>(type: "float", nullable: false),
                    Gelir = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hasatlar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hasatlar");
        }
    }
}
