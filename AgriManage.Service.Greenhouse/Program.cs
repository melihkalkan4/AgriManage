using AgriManage.Service.Greenhouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models; // Swagger başlığı için gerekli

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. VERİTABANI BAĞLANTISI
// ============================================================
builder.Services.AddDbContext<GreenhouseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ============================================================
// 2. CONTROLLER VE CORS
// ============================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CORS: WebApp ve API farklı portlarda olduğu için izin veriyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

// Swagger Özelleştirme (Hangi serviste olduğumuzu görelim)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgriManage Greenhouse API", Version = "v1" });
});

// ============================================================
// 3. JWT KİMLİK DOĞRULAMA (KRİTİK DÜZELTME ⚠️)
// ============================================================
// DİKKAT: WebApp'teki appsettings.json içindeki şifreyle AYNISI yapıldı.
// Eğer oradakini değiştirdiysen burayı da güncelle!
var key = Encoding.ASCII.GetBytes("AgriManage_Projesi_Icin_Cok_Gizli_Anahtar_2025");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,  // Basitlik için
        ValidateAudience = false // Basitlik için
    };
});

var app = builder.Build();

// ============================================================
// 4. PIPELINE (Çalışma Sırası)
// ============================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Swagger UI'da başlığı düzeltiyoruz
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greenhouse API v1"));
}

app.UseCors("AllowAll"); // Auth'tan önce gelmeli!

app.UseAuthentication(); // Kimsin? (Token kontrolü)
app.UseAuthorization();  // Yetkin var mı?

app.MapControllers();

app.Run();