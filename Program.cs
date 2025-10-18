using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;
using DondeComemos.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ Identity con Roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Agrega Razor Pages aquí
builder.Services.AddRazorPages();  // <-- Agregado

// CORRECTO: Registrar interfaz y clase
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();



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

    // Opcional: Crear un Admin por defecto si no existe
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
app.UseStaticFiles(); // <-- Agrega esta línea para servir archivos estáticos
app.UseRouting();
app.UseAuthentication(); //  primero autenticación
app.UseAuthorization();  //  luego autorización

// Elimina MapStaticAssets y WithStaticAssets, no existen en ASP.NET Core por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapea Razor Pages
app.MapRazorPages();  // <-- Asegúrate de que esto esté aquí

app.Run();
