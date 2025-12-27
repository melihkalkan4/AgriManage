using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;      // ApplicationUser s�n�f� burada
using AgriManage.DataAccess.Repository;  // Repository ve UnitOfWork aray�zleri burada
using AgriManage.BusinessLogic.Services; // TarlaService ve di�er servisler burada

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. VER�TABANI BA�LANTISI (DATABASE CONNECTION)
// ============================================================
// appsettings.json dosyas�ndan ba�lant� adresini okuyoruz.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// SQL Server kullan�m� ve Migration'lar�n nerede tutuldu�u ayar�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    b => b.MigrationsAssembly("AgriManage.DataAccess")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ============================================================
// 2. K�ML�K VE YETK�LEND�RME S�STEM� (IDENTITY)
// ============================================================
// BURASI �OK �NEML�: 
// Standart 'IdentityUser' yerine kendi olu�turdu�umuz 'ApplicationUser' s�n�f�n� kullan�yoruz.
// Ayr�ca '.AddRoles<IdentityRole>()' ekleyerek Admin/Manager/User gibi rol sistemini a��yoruz.
// .AddRoles<IdentityRole>() kısmını ekliyoruz:
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // <--- KRİTİK EKLEME BURASI
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ============================================================
// 3. BA�IMLILIK ENJEKS�YONU (DEPENDENCY INJECTION)
// ============================================================
// Projemizdeki aray�zlerin (Interface) kar��l���nda hangi s�n�flar�n �al��aca��n� belirtiyoruz.

// A) Generic Repository: "IRepository istenirse Repository �ret"
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// B) Unit Of Work: "Veritaban� kaydetme i�lemlerini y�netecek patron"
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// C) Servislerimiz (�� Mant���): "ITarlaService istenirse TarlaService �al��s�n"
builder.Services.AddScoped<ITarlaService, TarlaService>();
// Diğer servislerin altına ekle:
builder.Services.AddScoped<IAnalizService, AnalizService>(); // --- YENİ EKLENEN ---
// Diğer AddScoped satırlarının yanına ekle:
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IVardiyaService, VardiyaService>();

// --- MİKROSERVİS BAĞLANTISI BAŞLANGIÇ ---
// API ile haberleşmek için HttpClient servisini ekliyoruz.
// Bu sayede Controller'lar "bana InventoryClient ver" diyebilecek.
// Token servisini sisteme tanıtıyoruz
builder.Services.AddScoped<AgriManage.WebApp.Services.TokenService>();
builder.Services.AddHttpClient("InventoryClient", client =>
{
    // appsettings.json'dan adresi okuyoruz (https://localhost:7096)
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:InventoryBaseUrl"]);
});
// --- MİKROSERVİS BAĞLANTISI BİTİŞ ---


// ============================================================
// 4. MVC VE G�R�N�M AYARLARI
// ============================================================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ============================================================
// 5. HTTP �STEK Y�NET�M� (PIPELINE)
// ============================================================

// Geli�tirme ortam�ndaysak hatalar� detayl� g�ster
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // G�venlik i�in HSTS (Production ortam� i�in)
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e �evir
app.UseStaticFiles();      // wwwroot klas�r�n� (CSS, JS, Resimler) d��ar� a�

app.UseRouting();

// �NCE: Kimlik Do�rulama (Sen kimsin? Giri� yapt�n m�?)
app.UseAuthentication();
// SONRA: Yetkilendirme (Bu sayfay� g�rmeye yetkin var m�?)
app.UseAuthorization();

// Varsay�lan Rota: Site a��l�nca Home/Index'e git
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity sayfalar� (Login/Register) i�in gerekli rota
app.MapRazorPages();
// ... Yukarıdaki kodlar (builder, app, pipeline vs.) aynen kalsın ...

// ==================================================================
// ==> GARANTİLİ VERİ YÜKLEME KODU (HATASIZ) <==
// ==================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AgriManage.DataAccess.Data.ApplicationDbContext>();
        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<AgriManage.DataAccess.Models.ApplicationUser>>();
        var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();

        // 1. ROLLERİ OLUŞTUR
        if (!roleManager.RoleExistsAsync("Admin").Result) roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole("Admin")).Wait();
        if (!roleManager.RoleExistsAsync("Ciftci").Result) roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole("Ciftci")).Wait();

        // 2. KULLANICIYI BUL (MAİL ADRESİNİZİ KONTROL EDİN)
        var adminUser = userManager.FindByEmailAsync("MelihTest@gmail.com").Result;

        if (adminUser != null)
        {
            // A) LOKASYONLARI KONTROL ET VE OLUŞTUR
            // "Merkez" lokasyonunu bulamazsa oluşturur
            var merkez = context.Lokasyonlar.FirstOrDefault(x => x.Ad == "Merkez");
            if (merkez == null)
            {
                merkez = new AgriManage.DataAccess.Models.Lokasyon { Ad = "Merkez" };
                context.Lokasyonlar.Add(merkez);
                context.SaveChanges(); // ID oluşsun diye hemen kaydediyoruz
            }

            // "Köy" lokasyonunu bulamazsa oluşturur
            var koy = context.Lokasyonlar.FirstOrDefault(x => x.Ad == "Köy");
            if (koy == null)
            {
                koy = new AgriManage.DataAccess.Models.Lokasyon { Ad = "Köy" };
                context.Lokasyonlar.Add(koy);
                context.SaveChanges(); // ID oluşsun diye hemen kaydediyoruz
            }

            // B) TARLALARI EKLE (Eğer hiç tarlası yoksa)
            if (!context.Tarlalar.Any(t => t.ApplicationUserId == adminUser.Id))
            {
                context.Tarlalar.AddRange(
                    new AgriManage.DataAccess.Models.Tarla { Ad = "Büyük Ova", AlanDonum = 120, TapuAdaParsel = "101/5", LokasyonId = merkez.Id, ApplicationUserId = adminUser.Id },
                    new AgriManage.DataAccess.Models.Tarla { Ad = "Dere Kenarı", AlanDonum = 45.5m, TapuAdaParsel = "203/1", LokasyonId = koy.Id, ApplicationUserId = adminUser.Id },
                    new AgriManage.DataAccess.Models.Tarla { Ad = "Yamaç Bahçesi", AlanDonum = 12, TapuAdaParsel = "115/9", LokasyonId = koy.Id, ApplicationUserId = adminUser.Id },
                    new AgriManage.DataAccess.Models.Tarla { Ad = "Kuzey Tarlası", AlanDonum = 80, TapuAdaParsel = "305/4", LokasyonId = merkez.Id, ApplicationUserId = adminUser.Id }
                );
                context.SaveChanges(); // Tarlaları kaydet
            }
        }
    }
    catch (Exception ex)
    {
        // Bir hata olursa programı durdurma, sadece hatayı ekrana yaz.
        Console.WriteLine("************* HATA OLUŞTU *************");
        Console.WriteLine(ex.Message);
    }
}
// === VERİTABANI SEED (TOHUMLAMA) BAŞLANGICI ===
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AgriManage.DataAccess.Data.ApplicationDbContext>();

        // Seeder sınıfımızı çağırıyoruz
        AgriManage.DataAccess.Data.DbSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        // Hata olursa loglara yaz
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı seed işlemi sırasında bir hata oluştu.");
    }
}*/
// === VERİTABANI SEED BİTİŞİ ===
// ==================================================================
// ==================================================================
// ==> EKSİK OLAN PARÇA (EN ALTA YAPIŞTIRIN) <==
// ==================================================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Identity sayfalarının (Giriş/Kayıt) çalışması için şart!

app.Run(); // <--- İŞTE BU! Motoru çalıştıran kod.