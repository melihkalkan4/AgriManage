using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using AgriManage.DataAccess.Repository;
using AgriManage.BusinessLogic.Services;
using AgriManage.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. VERİTABANI BAĞLANTISI
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    b => b.MigrationsAssembly("AgriManage.DataAccess")));

// 2. KİMLİK VE YETKİLENDİRME (IDENTITY)
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. BAĞIMLILIK ENJEKSİYONU (DI)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITarlaService, TarlaService>();
builder.Services.AddScoped<IAnalizService, AnalizService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IVardiyaService, VardiyaService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<SeraService>();

// 4. MİKROSERVİS BAĞLANTILARI
builder.Services.AddHttpClient("InventoryClient", client =>
{
    var url = builder.Configuration["ApiSettings:InventoryBaseUrl"] ?? "https://localhost:7096";
    client.BaseAddress = new Uri(url);
});

builder.Services.AddHttpClient("GreenhouseClient", client =>
{
    var url = builder.Configuration["ApiSettings:GreenhouseBaseUrl"] ?? "https://localhost:7002";
    client.BaseAddress = new Uri(url);
});

// 5. MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 6. PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// 7. DATA SEEDING (ARTIK TEK SATIR!)
// Tüm karmaşık lojik DbSeeder.cs içine taşındı. Burası tertemiz.
using (var scope = app.Services.CreateScope())
{
    try
    {
        // DbSeeder sınıfındaki Seed metodunu çağırıyoruz
        await DbSeeder.Seed(scope.ServiceProvider);
    }
    catch (Exception ex)
    {
        Console.WriteLine("************* SEED HATA *************");
        Console.WriteLine(ex.Message);
        if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
    }
}

app.Run();