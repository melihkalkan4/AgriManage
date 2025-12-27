using Microsoft.EntityFrameworkCore;
using AgriManage.Service.Inventory.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer; // JWT paketi
using Microsoft.IdentityModel.Tokens;              // Token doğrulama araçları
using System.Text;                                 // Encoding (UTF8) için

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// 1. VERİTABANI BAĞLANTISI (Aynen Kalsın)
// =========================================================
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =========================================================
// 2. JWT KİMLİK DOĞRULAMA (YENİ EKLENEN KISIM) 🔒
// =========================================================
// appsettings.json'dan ayarları okuyoruz
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,     // Kimin imzaladığını kontrol et (WebApp mi?)
        ValidateAudience = true,   // Kime gönderildiğini kontrol et (Bana mı?)
        ValidateLifetime = true,   // Süresi dolmuş mu?
        ValidateIssuerSigningKey = true, // İmza geçerli mi?

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// =========================================================
// 3. DİĞER SERVİSLER
// =========================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =========================================================
// 4. PIPELINE (SIRASI ÇOK ÖNEMLİ!)
// =========================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// DİKKAT: Authentication (Kimlik Sorma) mutlaka Authorization'dan (Yetki Verme) ÖNCE gelmeli.
app.UseAuthentication(); // 👈 Kapıdaki Güvenlik (Kimsin?)
app.UseAuthorization();  // 👈 İçerdeki Yetki (Girebilir misin?)

app.MapControllers();

app.Run();