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

// 3. BAĞIMLILIK ENJEKSİYONU (DEPENDENCY INJECTION)
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

// 7. DATA SEEDING (Düzeltilmiş Hali)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Rolleri Oluştur
        if (!roleManager.RoleExistsAsync("Admin").Result) roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        if (!roleManager.RoleExistsAsync("Ciftci").Result) roleManager.CreateAsync(new IdentityRole("Ciftci")).Wait();
        if (!roleManager.RoleExistsAsync("User").Result) roleManager.CreateAsync(new IdentityRole("User")).Wait();

        // Admin Kullanıcısı
        var adminEmail = "MelihTest@gmail.com";
        var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                TamAd = "Melih Admin" // <--- DÜZELTİLEN KISIM BURASI
            };
            var result = userManager.CreateAsync(adminUser, "Admin123!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }
        }

        if (adminUser != null)
        {
            var merkez = context.Lokasyonlar.FirstOrDefault(x => x.Ad == "Merkez");
            if (merkez == null)
            {
                merkez = new Lokasyon { Ad = "Merkez" };
                context.Lokasyonlar.Add(merkez);
                context.SaveChanges();
            }

            if (!context.Tarlalar.Any(t => t.ApplicationUserId == adminUser.Id))
            {
                context.Tarlalar.AddRange(
                    new Tarla { Ad = "Büyük Ova", AlanDonum = 120, TapuAdaParsel = "101/5", LokasyonId = merkez.Id, ApplicationUserId = adminUser.Id },
                    new Tarla { Ad = "Dere Kenarı", AlanDonum = 45.5m, TapuAdaParsel = "203/1", LokasyonId = merkez.Id, ApplicationUserId = adminUser.Id }
                );
                context.SaveChanges();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("************* SEED HATA *************");
        Console.WriteLine(ex.Message);
    }
}

app.Run();