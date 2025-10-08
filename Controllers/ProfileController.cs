using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;
using DondeComemos.Models;
using DondeComemos.Services;
using System.Text.Json;

namespace DondeComemos.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IFileService _fileService;

        public ProfileController(
            ApplicationDbContext context, 
            UserManager<IdentityUser> userManager,
            IFileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return NotFound();

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = userId,
                    NombreUsuario = user.UserName,
                    FotoPerfil = "/img/default-avatar.jpg"
                };
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();
            }

            // Actualizar última actividad
            profile.UltimaActividad = DateTime.Now;
            await _context.SaveChangesAsync();

            // Obtener historial de búsquedas
            var historial = new List<string>();
            if (!string.IsNullOrEmpty(profile.HistorialBusquedas))
            {
                try
                {
                    historial = JsonSerializer.Deserialize<List<string>>(profile.HistorialBusquedas) ?? new List<string>();
                }
                catch { }
            }

            // Obtener restaurantes favoritos
            var favoritos = new List<Restaurante>();
            if (!string.IsNullOrEmpty(profile.RestaurantesFavoritos))
            {
                try
                {
                    var favoritosIds = JsonSerializer.Deserialize<List<int>>(profile.RestaurantesFavoritos) ?? new List<int>();
                    favoritos = await _context.Restaurantes
                        .Where(r => favoritosIds.Contains(r.Id))
                        .ToListAsync();
                }
                catch { }
            }

            ViewBag.Email = user.Email;
            ViewBag.Historial = historial.Take(10).ToList();
            ViewBag.Favoritos = favoritos;
            
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserProfile model)
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound();

            profile.NombreUsuario = model.NombreUsuario;
            profile.Nombres = model.Nombres;
            profile.Apellidos = model.Apellidos;
            profile.Biografia = model.Biografia;
            profile.Telefono = model.Telefono;
            profile.FechaNacimiento = model.FechaNacimiento;
            profile.Ciudad = model.Ciudad;

            // Manejar foto de perfil
            if (model.FotoArchivo != null)
            {
                try
                {
                    // Eliminar foto anterior si existe y no es la predeterminada
                    if (!string.IsNullOrEmpty(profile.FotoPerfil) && 
                        !profile.FotoPerfil.Contains("default-avatar"))
                    {
                        _fileService.DeleteImage(profile.FotoPerfil);
                    }

                    profile.FotoPerfil = await _fileService.SaveImageAsync(
                        model.FotoArchivo, "perfiles");
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }

            _context.Update(profile);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Perfil actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, 
            string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Las contraseñas nuevas no coinciden";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(
                user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Contraseña cambiada exitosamente";
            }
            else
            {
                TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarFavorito(int restauranteId)
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return Json(new { success = false, message = "Perfil no encontrado" });

            var favoritos = new List<int>();
            if (!string.IsNullOrEmpty(profile.RestaurantesFavoritos))
            {
                try
                {
                    favoritos = JsonSerializer.Deserialize<List<int>>(profile.RestaurantesFavoritos) ?? new List<int>();
                }
                catch { }
            }

            if (!favoritos.Contains(restauranteId))
            {
                favoritos.Add(restauranteId);
                profile.RestaurantesFavoritos = JsonSerializer.Serialize(favoritos);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true, message = "Agregado a favoritos" });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarFavorito(int restauranteId)
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return Json(new { success = false, message = "Perfil no encontrado" });

            var favoritos = new List<int>();
            if (!string.IsNullOrEmpty(profile.RestaurantesFavoritos))
            {
                try
                {
                    favoritos = JsonSerializer.Deserialize<List<int>>(profile.RestaurantesFavoritos) ?? new List<int>();
                }
                catch { }
            }

            favoritos.Remove(restauranteId);
            profile.RestaurantesFavoritos = JsonSerializer.Serialize(favoritos);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Eliminado de favoritos" });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarBusqueda(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new { success = false });

            var userId = _userManager.GetUserId(User);
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return Json(new { success = false });

            var historial = new List<string>();
            if (!string.IsNullOrEmpty(profile.HistorialBusquedas))
            {
                try
                {
                    historial = JsonSerializer.Deserialize<List<string>>(profile.HistorialBusquedas) ?? new List<string>();
                }
                catch { }
            }

            // Agregar al inicio y limitar a 50 búsquedas
            historial.Insert(0, $"{termino}|{DateTime.Now:yyyy-MM-dd HH:mm}");
            historial = historial.Take(50).ToList();

            profile.HistorialBusquedas = JsonSerializer.Serialize(historial);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> LimpiarHistorial()
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile != null)
            {
                profile.HistorialBusquedas = null;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Historial limpiado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}