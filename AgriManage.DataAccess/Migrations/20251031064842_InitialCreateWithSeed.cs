using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriManage.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AraziTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AraziTipleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TamAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakimTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakimTipleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolgeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolgeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departmanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EkipmanDurumlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkipmanDurumlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EkipmanKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkipmanKategorileri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GorevDurumlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevDurumlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GorevTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevTipleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sezonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezonlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StokKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokKategorileri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tedarikciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YetkiliKisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedarikciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrunKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunKategorileri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vardiyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vardiyalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YetkiAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YetkiTipi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YetkiAlanlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lokasyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BolgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokasyonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lokasyonlar_Bolgeler_BolgeId",
                        column: x => x.BolgeId,
                        principalTable: "Bolgeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pozisyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozisyonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pozisyonlar_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ekipmanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Marka = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SeriNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EkipmanKategorisiId = table.Column<int>(type: "int", nullable: false),
                    EkipmanDurumuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipmanlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekipmanlar_EkipmanDurumlari_EkipmanDurumuId",
                        column: x => x.EkipmanDurumuId,
                        principalTable: "EkipmanDurumlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ekipmanlar_EkipmanKategorileri_EkipmanKategorisiId",
                        column: x => x.EkipmanKategorisiId,
                        principalTable: "EkipmanKategorileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokItemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Birim = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StokKategorisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokItemleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokItemleri_StokKategorileri_StokKategorisiId",
                        column: x => x.StokKategorisiId,
                        principalTable: "StokKategorileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrunKategorisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urunler_UrunKategorileri_UrunKategorisiId",
                        column: x => x.UrunKategorisiId,
                        principalTable: "UrunKategorileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Depolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LokasyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depolar_Lokasyonlar_LokasyonId",
                        column: x => x.LokasyonId,
                        principalTable: "Lokasyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seralar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AlanMetrekare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LokasyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seralar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seralar_Lokasyonlar_LokasyonId",
                        column: x => x.LokasyonId,
                        principalTable: "Lokasyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarlalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AlanDonum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TapuAdaParsel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LokasyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarlalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarlalar_Lokasyonlar_LokasyonId",
                        column: x => x.LokasyonId,
                        principalTable: "Lokasyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SicilNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IseBaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PozisyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personeller_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Pozisyonlar_PozisyonId",
                        column: x => x.PozisyonId,
                        principalTable: "Pozisyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PozisyonYetkileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PozisyonId = table.Column<int>(type: "int", nullable: false),
                    YetkiAlaniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozisyonYetkileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PozisyonYetkileri_Pozisyonlar_PozisyonId",
                        column: x => x.PozisyonId,
                        principalTable: "Pozisyonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozisyonYetkileri_YetkiAlanlari_YetkiAlaniId",
                        column: x => x.YetkiAlaniId,
                        principalTable: "YetkiAlanlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BakimPlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanlananTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EkipmanId = table.Column<int>(type: "int", nullable: false),
                    BakimTipiId = table.Column<int>(type: "int", nullable: false),
                    Tamamlandi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakimPlanlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BakimPlanlari_BakimTipleri_BakimTipiId",
                        column: x => x.BakimTipiId,
                        principalTable: "BakimTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BakimPlanlari_Ekipmanlar_EkipmanId",
                        column: x => x.EkipmanId,
                        principalTable: "Ekipmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EkipmanLoglari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkipmanId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkipmanLoglari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EkipmanLoglari_Ekipmanlar_EkipmanId",
                        column: x => x.EkipmanId,
                        principalTable: "Ekipmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EkimPlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarlaId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    SezonId = table.Column<int>(type: "int", nullable: false),
                    EkimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasatTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeklenenVerimKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GerceklesenVerimKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkimPlanlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EkimPlanlari_Sezonlar_SezonId",
                        column: x => x.SezonId,
                        principalTable: "Sezonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkimPlanlari_Tarlalar_TarlaId",
                        column: x => x.TarlaId,
                        principalTable: "Tarlalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkimPlanlari_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarlaAraziTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarlaId = table.Column<int>(type: "int", nullable: false),
                    AraziTipiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarlaAraziTipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarlaAraziTipleri_AraziTipleri_AraziTipiId",
                        column: x => x.AraziTipiId,
                        principalTable: "AraziTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarlaAraziTipleri_Tarlalar_TarlaId",
                        column: x => x.TarlaId,
                        principalTable: "Tarlalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gorevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanlananBaslangic = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TamamlanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevTipiId = table.Column<int>(type: "int", nullable: false),
                    GorevDurumuId = table.Column<int>(type: "int", nullable: false),
                    TarlaId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorevler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gorevler_GorevDurumlari_GorevDurumuId",
                        column: x => x.GorevDurumuId,
                        principalTable: "GorevDurumlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gorevler_GorevTipleri_GorevTipiId",
                        column: x => x.GorevTipiId,
                        principalTable: "GorevTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gorevler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gorevler_Tarlalar_TarlaId",
                        column: x => x.TarlaId,
                        principalTable: "Tarlalar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonelVardiyalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    VardiyaId = table.Column<int>(type: "int", nullable: false),
                    Gun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelVardiyalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelVardiyalari_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelVardiyalari_Vardiyalar_VardiyaId",
                        column: x => x.VardiyaId,
                        principalTable: "Vardiyalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BakimKayitlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TamamlanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maliyet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BakimPlaniId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakimKayitlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BakimKayitlari_BakimPlanlari_BakimPlaniId",
                        column: x => x.BakimPlaniId,
                        principalTable: "BakimPlanlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BakimKayitlari_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevLoglari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevLoglari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevLoglari_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperasyonelRaporlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaporTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RaporOzeti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GorevId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperasyonelRaporlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperasyonelRaporlar_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HareketTipi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StokItemId = table.Column<int>(type: "int", nullable: false),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    TedarikciId = table.Column<int>(type: "int", nullable: true),
                    OperasyonelRaporId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_OperasyonelRaporlar_OperasyonelRaporId",
                        column: x => x.OperasyonelRaporId,
                        principalTable: "OperasyonelRaporlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StokHareketleri_StokItemleri_StokItemId",
                        column: x => x.StokItemId,
                        principalTable: "StokItemleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Tedarikciler_TedarikciId",
                        column: x => x.TedarikciId,
                        principalTable: "Tedarikciler",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AraziTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Killi Toprak" },
                    { 2, "Kumlu Toprak" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1e1a1a1-1a1a-1a1a-1a1a-1a1a1a1a1a1a", null, "Admin", "ADMIN" },
                    { "b2b2b2b2-2b2b-2b2b-2b2b-2b2b2b2b2b2b", null, "Kullanici", "KULLANICI" }
                });

            migrationBuilder.InsertData(
                table: "BakimTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Periyodik Yağ Değişimi" },
                    { 2, "Arıza Onarımı" }
                });

            migrationBuilder.InsertData(
                table: "Bolgeler",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 1, "Trakya Bölgesi" });

            migrationBuilder.InsertData(
                table: "Departmanlar",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Ziraat Operasyonları" },
                    { 2, "Teknik Bakım" },
                    { 3, "Stok ve Lojistik" },
                    { 4, "Yönetim" }
                });

            migrationBuilder.InsertData(
                table: "EkipmanDurumlari",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Aktif" },
                    { 2, "Bakımda" },
                    { 3, "Arızalı" }
                });

            migrationBuilder.InsertData(
                table: "EkipmanKategorileri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Traktörler" },
                    { 2, "Ekim Ekipmanları" }
                });

            migrationBuilder.InsertData(
                table: "GorevDurumlari",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Atandı" },
                    { 2, "Devam Ediyor" },
                    { 3, "Tamamlandı" },
                    { 4, "İptal Edildi" }
                });

            migrationBuilder.InsertData(
                table: "GorevTipleri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Ekim" },
                    { 2, "Gübreleme" },
                    { 3, "İlaçlama" },
                    { 4, "Sulama" },
                    { 5, "Hasat" }
                });

            migrationBuilder.InsertData(
                table: "Sezonlar",
                columns: new[] { "Id", "Ad", "BaslangicTarihi", "BitisTarihi" },
                values: new object[] { 1, "2024 Sonbahar Ekimi", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "StokKategorileri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Gübreler" },
                    { 2, "Tohumlar" },
                    { 3, "Zirai İlaçlar" }
                });

            migrationBuilder.InsertData(
                table: "Tedarikciler",
                columns: new[] { "Id", "Ad", "Telefon", "YetkiliKisi" },
                values: new object[,]
                {
                    { 1, "Gübretaş A.Ş.", "02120000001", "Ahmet Yılmaz" },
                    { 2, "Tohumcular Ltd.", "02120000002", "Zeynep Kaya" }
                });

            migrationBuilder.InsertData(
                table: "UrunKategorileri",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Tahıllar" },
                    { 2, "Yağlı Tohumlar" }
                });

            migrationBuilder.InsertData(
                table: "Vardiyalar",
                columns: new[] { "Id", "Ad", "BaslangicSaati", "BitisSaati" },
                values: new object[,]
                {
                    { 1, "Sabah (08:00 - 16:00)", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 16, 0, 0, 0) },
                    { 2, "Akşam (16:00 - 00:00)", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "YetkiAlanlari",
                columns: new[] { "Id", "ModulAdi", "YetkiTipi" },
                values: new object[,]
                {
                    { 1, "StokYonetimi", "TamErisim" },
                    { 2, "GorevYonetimi", "Okuma" },
                    { 3, "GorevYonetimi", "Yazma" }
                });

            migrationBuilder.InsertData(
                table: "Ekipmanlar",
                columns: new[] { "Id", "Ad", "EkipmanDurumuId", "EkipmanKategorisiId", "Marka", "Model", "SeriNo" },
                values: new object[,]
                {
                    { 1, "Büyük Kırmızı Traktör", 1, 1, "Case IH", "Puma 240", "TR-001" },
                    { 2, "Ekim Mibzeri", 1, 2, "Paksan", "24'lü", "TR-002" }
                });

            migrationBuilder.InsertData(
                table: "Lokasyonlar",
                columns: new[] { "Id", "Ad", "Adres", "BolgeId" },
                values: new object[,]
                {
                    { 1, "Merkez Çiftlik (Lüleburgaz)", "Lüleburgaz Yolu Üzeri", 1 },
                    { 2, "Tekirdağ Depo", "Tekirdağ Liman", 1 }
                });

            migrationBuilder.InsertData(
                table: "Pozisyonlar",
                columns: new[] { "Id", "Ad", "DepartmanId" },
                values: new object[,]
                {
                    { 1, "Yönetici", 4 },
                    { 2, "Ziraat Mühendisi", 1 },
                    { 3, "Traktör Operatörü", 1 },
                    { 4, "Bakım Teknisyeni", 2 },
                    { 5, "Depo Sorumlusu", 3 }
                });

            migrationBuilder.InsertData(
                table: "StokItemleri",
                columns: new[] { "Id", "Ad", "Birim", "StokKategorisiId" },
                values: new object[,]
                {
                    { 1, "DAP Gübresi", "KG", 1 },
                    { 2, "ÜRE Gübresi", "KG", 1 },
                    { 3, "Ceyhan-99 Buğday Tohumu", "KG", 2 },
                    { 4, "Yabancı Ot İlacı", "LT", 3 }
                });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "Id", "Ad", "UrunKategorisiId" },
                values: new object[,]
                {
                    { 1, "Buğday (Ceyhan-99)", 1 },
                    { 2, "Ayçiçeği (Tunka)", 2 },
                    { 3, "Kanola", 2 }
                });

            migrationBuilder.InsertData(
                table: "BakimPlanlari",
                columns: new[] { "Id", "BakimTipiId", "EkipmanId", "PlanlananTarih", "Tamamlandi" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Depolar",
                columns: new[] { "Id", "Ad", "LokasyonId" },
                values: new object[,]
                {
                    { 1, "Ana Gübre Deposu", 1 },
                    { 2, "Zirai İlaç Deposu", 1 },
                    { 3, "Lojistik Merkezi", 2 }
                });

            migrationBuilder.InsertData(
                table: "EkipmanLoglari",
                columns: new[] { "Id", "Aciklama", "EkipmanId", "Tarih" },
                values: new object[] { 1, "Ekipman sisteme eklendi.", 1, new DateTime(2025, 10, 31, 9, 48, 41, 830, DateTimeKind.Local).AddTicks(2031) });

            migrationBuilder.InsertData(
                table: "Personeller",
                columns: new[] { "Id", "ApplicationUserId", "IseBaslamaTarihi", "PozisyonId", "SicilNo" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "P-001" },
                    { 2, null, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "P-002" },
                    { 3, null, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "P-003" },
                    { 4, null, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "P-004" },
                    { 5, null, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "P-005" }
                });

            migrationBuilder.InsertData(
                table: "PozisyonYetkileri",
                columns: new[] { "Id", "PozisyonId", "YetkiAlaniId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 5, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Seralar",
                columns: new[] { "Id", "Ad", "AlanMetrekare", "LokasyonId" },
                values: new object[] { 1, "Fide Serası", 500m, 1 });

            migrationBuilder.InsertData(
                table: "Tarlalar",
                columns: new[] { "Id", "Ad", "AlanDonum", "LokasyonId", "TapuAdaParsel" },
                values: new object[,]
                {
                    { 1, "Ana Tarla - A1", 150m, 1, "101/1" },
                    { 2, "Dere Kenarı - B2", 85m, 1, "102/3" }
                });

            migrationBuilder.InsertData(
                table: "BakimKayitlari",
                columns: new[] { "Id", "Aciklama", "BakimPlaniId", "Maliyet", "PersonelId", "TamamlanmaTarihi" },
                values: new object[] { 1, "Sol arka lastik değişimi yapıldı.", null, 4500.00m, 4, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "EkimPlanlari",
                columns: new[] { "Id", "BeklenenVerimKg", "EkimTarihi", "GerceklesenVerimKg", "HasatTarihi", "SezonId", "TarlaId", "UrunId" },
                values: new object[,]
                {
                    { 1, 60000m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 1, 1, 1 },
                    { 2, 30000m, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, null, 1, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Gorevler",
                columns: new[] { "Id", "Aciklama", "Baslik", "GorevDurumuId", "GorevTipiId", "OlusturmaTarihi", "PersonelId", "PlanlananBaslangic", "TamamlanmaTarihi", "TarlaId" },
                values: new object[,]
                {
                    { 1, "Ceyhan-99 tohumları kullanılarak ekim yapılacak.", "A1 Tarlası Buğday Ekimi", 3, 1, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Ayçiçeği için DAP gübresi atılacak.", "B2 Tarlası Gübreleme", 1, 2, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "PersonelVardiyalari",
                columns: new[] { "Id", "Gun", "PersonelId", "VardiyaId" },
                values: new object[,]
                {
                    { 1, 1, 3, 1 },
                    { 2, 2, 3, 1 },
                    { 3, 1, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "StokHareketleri",
                columns: new[] { "Id", "BirimFiyat", "DepoId", "HareketTipi", "Miktar", "OperasyonelRaporId", "StokItemId", "Tarih", "TedarikciId" },
                values: new object[,]
                {
                    { 1, 22.5m, 1, "Giriş", 5000m, null, 1, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 25.0m, 1, "Giriş", 10000m, null, 2, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 15.0m, 2, "Giriş", 8000m, null, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "TarlaAraziTipleri",
                columns: new[] { "Id", "AraziTipiId", "TarlaId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "GorevLoglari",
                columns: new[] { "Id", "Aciklama", "GorevId", "Tarih" },
                values: new object[,]
                {
                    { 1, "Görev 'Atandı' durumunda oluşturuldu.", 1, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Görev 'Devam Ediyor' durumuna alındı.", 1, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Görev 'Tamamlandı' durumuna alındı.", 1, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Görev 'Atandı' durumunda oluşturuldu.", 2, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OperasyonelRaporlar",
                columns: new[] { "Id", "GorevId", "RaporOzeti", "RaporTarihi" },
                values: new object[] { 1, 1, "150 dönüm tarla başarıyla ekildi. 8000 KG Ceyhan-99 tohumu kullanıldı.", new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "StokHareketleri",
                columns: new[] { "Id", "BirimFiyat", "DepoId", "HareketTipi", "Miktar", "OperasyonelRaporId", "StokItemId", "Tarih", "TedarikciId" },
                values: new object[] { 4, 15.0m, 2, "Sarf", 8000m, 1, 3, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BakimKayitlari_BakimPlaniId",
                table: "BakimKayitlari",
                column: "BakimPlaniId");

            migrationBuilder.CreateIndex(
                name: "IX_BakimKayitlari_PersonelId",
                table: "BakimKayitlari",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_BakimPlanlari_BakimTipiId",
                table: "BakimPlanlari",
                column: "BakimTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_BakimPlanlari_EkipmanId",
                table: "BakimPlanlari",
                column: "EkipmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Depolar_LokasyonId",
                table: "Depolar",
                column: "LokasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_EkimPlanlari_SezonId",
                table: "EkimPlanlari",
                column: "SezonId");

            migrationBuilder.CreateIndex(
                name: "IX_EkimPlanlari_TarlaId",
                table: "EkimPlanlari",
                column: "TarlaId");

            migrationBuilder.CreateIndex(
                name: "IX_EkimPlanlari_UrunId",
                table: "EkimPlanlari",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipmanlar_EkipmanDurumuId",
                table: "Ekipmanlar",
                column: "EkipmanDurumuId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipmanlar_EkipmanKategorisiId",
                table: "Ekipmanlar",
                column: "EkipmanKategorisiId");

            migrationBuilder.CreateIndex(
                name: "IX_EkipmanLoglari_EkipmanId",
                table: "EkipmanLoglari",
                column: "EkipmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_GorevDurumuId",
                table: "Gorevler",
                column: "GorevDurumuId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_GorevTipiId",
                table: "Gorevler",
                column: "GorevTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_PersonelId",
                table: "Gorevler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_TarlaId",
                table: "Gorevler",
                column: "TarlaId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevLoglari_GorevId",
                table: "GorevLoglari",
                column: "GorevId");

            migrationBuilder.CreateIndex(
                name: "IX_Lokasyonlar_BolgeId",
                table: "Lokasyonlar",
                column: "BolgeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperasyonelRaporlar_GorevId",
                table: "OperasyonelRaporlar",
                column: "GorevId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_ApplicationUserId",
                table: "Personeller",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_PozisyonId",
                table: "Personeller",
                column: "PozisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelVardiyalari_PersonelId",
                table: "PersonelVardiyalari",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelVardiyalari_VardiyaId",
                table: "PersonelVardiyalari",
                column: "VardiyaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pozisyonlar_DepartmanId",
                table: "Pozisyonlar",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_PozisyonYetkileri_PozisyonId",
                table: "PozisyonYetkileri",
                column: "PozisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_PozisyonYetkileri_YetkiAlaniId",
                table: "PozisyonYetkileri",
                column: "YetkiAlaniId");

            migrationBuilder.CreateIndex(
                name: "IX_Seralar_LokasyonId",
                table: "Seralar",
                column: "LokasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_DepoId",
                table: "StokHareketleri",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_OperasyonelRaporId",
                table: "StokHareketleri",
                column: "OperasyonelRaporId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_StokItemId",
                table: "StokHareketleri",
                column: "StokItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_TedarikciId",
                table: "StokHareketleri",
                column: "TedarikciId");

            migrationBuilder.CreateIndex(
                name: "IX_StokItemleri_StokKategorisiId",
                table: "StokItemleri",
                column: "StokKategorisiId");

            migrationBuilder.CreateIndex(
                name: "IX_TarlaAraziTipleri_AraziTipiId",
                table: "TarlaAraziTipleri",
                column: "AraziTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_TarlaAraziTipleri_TarlaId",
                table: "TarlaAraziTipleri",
                column: "TarlaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarlalar_LokasyonId",
                table: "Tarlalar",
                column: "LokasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_UrunKategorisiId",
                table: "Urunler",
                column: "UrunKategorisiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BakimKayitlari");

            migrationBuilder.DropTable(
                name: "EkimPlanlari");

            migrationBuilder.DropTable(
                name: "EkipmanLoglari");

            migrationBuilder.DropTable(
                name: "GorevLoglari");

            migrationBuilder.DropTable(
                name: "PersonelVardiyalari");

            migrationBuilder.DropTable(
                name: "PozisyonYetkileri");

            migrationBuilder.DropTable(
                name: "Seralar");

            migrationBuilder.DropTable(
                name: "StokHareketleri");

            migrationBuilder.DropTable(
                name: "TarlaAraziTipleri");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BakimPlanlari");

            migrationBuilder.DropTable(
                name: "Sezonlar");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Vardiyalar");

            migrationBuilder.DropTable(
                name: "YetkiAlanlari");

            migrationBuilder.DropTable(
                name: "Depolar");

            migrationBuilder.DropTable(
                name: "OperasyonelRaporlar");

            migrationBuilder.DropTable(
                name: "StokItemleri");

            migrationBuilder.DropTable(
                name: "Tedarikciler");

            migrationBuilder.DropTable(
                name: "AraziTipleri");

            migrationBuilder.DropTable(
                name: "BakimTipleri");

            migrationBuilder.DropTable(
                name: "Ekipmanlar");

            migrationBuilder.DropTable(
                name: "UrunKategorileri");

            migrationBuilder.DropTable(
                name: "Gorevler");

            migrationBuilder.DropTable(
                name: "StokKategorileri");

            migrationBuilder.DropTable(
                name: "EkipmanDurumlari");

            migrationBuilder.DropTable(
                name: "EkipmanKategorileri");

            migrationBuilder.DropTable(
                name: "GorevDurumlari");

            migrationBuilder.DropTable(
                name: "GorevTipleri");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Tarlalar");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pozisyonlar");

            migrationBuilder.DropTable(
                name: "Lokasyonlar");

            migrationBuilder.DropTable(
                name: "Departmanlar");

            migrationBuilder.DropTable(
                name: "Bolgeler");
        }
    }
}
