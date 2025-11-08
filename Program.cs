using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DondeComemos.Data;
using DondeComemos.Services;

var builder = WebApplication.CreateBuilder(args);

// Cargar appsettings.json explícitamente (evita que variables de entorno reemplacen la cadena)
var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");

// Registrar DbContext con SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Encuentra esta sección y agrega:
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IChatService, ChatService>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Crear/asegurar la base de datos y crear roles iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        var roleManager = services.GetService<RoleManager<IdentityRole>>();
        if (roleManager != null)
        {
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                var exists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!exists)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }
        }
        // Crear usuario administrador por defecto si no existe
        var userManager = services.GetService<UserManager<IdentityUser>>();
        if (userManager != null)
        {
            var adminEmail = "admin@dondecomemos.test";
            var adminPassword = "Admin123!";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var createResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (createResult.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                    Console.WriteLine("Usuario administrador creado: " + adminEmail);
                }
                else
                {
                    Console.WriteLine("No se pudo crear el usuario admin. Errores:");
                    foreach (var err in createResult.Errors) Console.WriteLine(err.Description);
                }
            }
            else
            {
                // Asegurar que el usuario esté en el rol Admin
                var inRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!inRole)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                }
            }
        }
    
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error inicializando la base de datos: " + ex.Message);
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
=======
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
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
<<<<<<< HEAD
app.UseAuthentication();
app.UseAuthorization();

=======
<<<<<<< HEAD

app.UseAuthentication(); 
app.UseAuthorization();  

// ✅ Rutas por defecto
=======
app.UseAuthentication();
app.UseAuthorization();

>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

<<<<<<< HEAD
app.MapRazorPages();

app.Run();
=======
<<<<<<< HEAD
// ✅ Razor Pages
app.MapRazorPages();

app.Run();
=======
app.MapRazorPages();

app.Run();
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
