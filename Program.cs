using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;
using DondeComemos.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

=======
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;
using DondeComemos.Services;
>>>>>>> b808e6f (Avance Mauricio Benavente)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ Identity con Roles
<<<<<<< HEAD
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
=======
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false; // ⚠️ Cambiado a false para desarrollo
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
>>>>>>> b808e6f (Avance Mauricio Benavente)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

<<<<<<< HEAD
// ✅ Agrega Razor Pages
builder.Services.AddRazorPages();

// ✅ Servicios personalizados
builder.Services.AddScoped<IHomeService, HomeService>();

// ✅ Servicio de correo falso (simulado)
builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();




=======
// Agrega Razor Pages
builder.Services.AddRazorPages();

// ✅ Registrar servicios (IMPORTANTE: IEmailSender debe registrarse)
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IEmailSender, IdentityEmailSender>(); // ⭐ NUEVO
>>>>>>> b808e6f (Avance Mauricio Benavente)

var app = builder.Build();

// ✅ Crear roles por defecto (Admin, Cliente)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "Cliente" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

<<<<<<< HEAD
    // ✅ Crear un Admin por defecto si no existe
=======
    // Opcional: Crear un Admin por defecto si no existe
>>>>>>> b808e6f (Avance Mauricio Benavente)
    string adminEmail = "admin@dondecomemos.com";
    string adminPassword = "Admin123!"; // cámbialo luego

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

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
<<<<<<< HEAD

app.UseAuthentication(); 
app.UseAuthorization();  

// ✅ Rutas por defecto
=======
app.UseAuthentication();
app.UseAuthorization();

>>>>>>> b808e6f (Avance Mauricio Benavente)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

<<<<<<< HEAD
// ✅ Razor Pages
app.MapRazorPages();

app.Run();
=======
app.MapRazorPages();

app.Run();
>>>>>>> b808e6f (Avance Mauricio Benavente)
