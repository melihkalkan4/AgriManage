using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.Service.Inventory.Migrations
{
    /// <inheritdoc />
    public partial class TarlaIdEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TarlaId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TarlaId",
                table: "Products");
        }
    }
}
