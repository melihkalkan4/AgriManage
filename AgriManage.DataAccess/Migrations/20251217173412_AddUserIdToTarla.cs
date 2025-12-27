using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTarla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tarlalar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 12, 17, 20, 34, 11, 409, DateTimeKind.Local).AddTicks(864));

            migrationBuilder.UpdateData(
                table: "Tarlalar",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tarlalar",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tarlalar");

            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 10, 31, 9, 48, 41, 830, DateTimeKind.Local).AddTicks(2031));
        }
    }
}
