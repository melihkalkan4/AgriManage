using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 12, 17, 21, 17, 6, 554, DateTimeKind.Local).AddTicks(388));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EkipmanLoglari",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tarih",
                value: new DateTime(2025, 12, 17, 20, 34, 11, 409, DateTimeKind.Local).AddTicks(864));
        }
    }
}
