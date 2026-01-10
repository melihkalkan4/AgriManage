using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.Service.Greenhouse.Migrations
{
    /// <inheritdoc />
    public partial class HasatEkimId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EkimId",
                table: "Hasatlar",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EkimId",
                table: "Hasatlar");
        }
    }
}
