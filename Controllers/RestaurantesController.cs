using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DondeComemos.Data;
using DondeComemos.Models;
using DondeComemos.Services;

namespace DondeComemos.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public RestaurantesController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // --- Vistas públicas (Clientes) ---
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var restaurantes = await _context.Restaurantes
                                             .OrderByDescending(r => r.Rating)
                                             .ToListAsync();

            return View(restaurantes);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string? q)
        {
            var query = _context.Restaurantes.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(r => r.Nombre.Contains(q) ||
                                         r.Direccion.Contains(q) ||
                                         r.Descripcion.Contains(q));
            }

            var restaurantes = await query.OrderByDescending(r => r.Rating).ToListAsync();

            return View(restaurantes);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var restaurante = await _context.Restaurantes
                .Include(r => r.Productos.Where(p => p.Disponible))
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurante == null)
                return NotFound();

            return View(restaurante);
        }

        // --- CRUD (solo Admin) ---
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                // Manejar subida de imagen
                if (restaurante.ImagenArchivo != null)
                {
                    try
                    {
                        restaurante.ImagenUrl = await _fileService.SaveImageAsync(
                            restaurante.ImagenArchivo, "restaurantes");
                    }
                    catch (InvalidOperationException ex)
                    {
                        ModelState.AddModelError("ImagenArchivo", ex.Message);
                        return View(restaurante);
                    }
                }

                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Restaurante creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(restaurante);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            return View(restaurante);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restauranteExistente = await _context.Restaurantes.AsNoTracking()
                        .FirstOrDefaultAsync(r => r.Id == id);

                    // Manejar nueva imagen
                    if (restaurante.ImagenArchivo != null)
                    {
                        try
                        {
                            // Eliminar imagen anterior si existe
                            if (!string.IsNullOrEmpty(restauranteExistente?.ImagenUrl))
                            {
                                _fileService.DeleteImage(restauranteExistente.ImagenUrl);
                            }

                            restaurante.ImagenUrl = await _fileService.SaveImageAsync(
                                restaurante.ImagenArchivo, "restaurantes");
                        }
                        catch (InvalidOperationException ex)
                        {
                            ModelState.AddModelError("ImagenArchivo", ex.Message);
                            return View(restaurante);
                        }
                    }
                    else
                    {
                        // Mantener imagen existente si no se sube una nueva
                        restaurante.ImagenUrl = restauranteExistente?.ImagenUrl ?? string.Empty;
                    }

                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Restaurante actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestauranteExists(restaurante.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(restaurante);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            return View(restaurante);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var restaurante = await _context.Restaurantes
                    .Include(r => r.Productos)
                    .FirstOrDefaultAsync(r => r.Id == id);
                
                if (restaurante != null)
                {
                    // Eliminar imágenes de productos
                    foreach (var producto in restaurante.Productos)
                    {
                        if (!string.IsNullOrEmpty(producto.ImagenUrl))
                        {
                            _fileService.DeleteImage(producto.ImagenUrl);
                        }
                    }

                    // Eliminar imagen del restaurante
                    if (!string.IsNullOrEmpty(restaurante.ImagenUrl))
                    {
                        _fileService.DeleteImage(restaurante.ImagenUrl);
                    }

                    _context.Restaurantes.Remove(restaurante);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Restaurante eliminado exitosamente";
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool RestauranteExists(int id)
        {
            return _context.Restaurantes.Any(e => e.Id == id);
        }
    }
}