using AgriManage.Service.Inventory.Data;
using Microsoft.EntityFrameworkCore;

// 1. ADIM: Önce Builder oluþturulur (Senin hatan bu satýrýn aþaðýda kalmasýydý)
var builder = WebApplication.CreateBuilder(args);

// 2. ADIM: Servisler eklenir (Veritabaný baðlantýsý burada yapýlýr)
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Diðer servisler (Controller, Swagger vb.)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. ADIM: Uygulama inþa edilir (Build)
var app = builder.Build();

// 4. ADIM: HTTP Ýstek hattý (Pipeline) ayarlanýr
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();