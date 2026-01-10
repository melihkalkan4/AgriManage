using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class VardiyaSonGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gun",
                table: "PersonelVardiyalari");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Vardiyalar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Tarlalar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notlar",
                table: "PersonelVardiyalari",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tarih",
                table: "PersonelVardiyalari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Tarlalar_ApplicationUserId",
                table: "Tarlalar",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarlalar_AspNetUsers_ApplicationUserId",
                table: "Tarlalar",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarlalar_AspNetUsers_ApplicationUserId",
                table: "Tarlalar");

            migrationBuilder.DropIndex(
                name: "IX_Tarlalar_ApplicationUserId",
                table: "Tarlalar");

            migrationBuilder.DropColumn(
                name: "Notlar",
                table: "PersonelVardiyalari");

            migrationBuilder.DropColumn(
                name: "Tarih",
                table: "PersonelVardiyalari");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Vardiyalar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Tarlalar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gun",
                table: "PersonelVardiyalari",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
