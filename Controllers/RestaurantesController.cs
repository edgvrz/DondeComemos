using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DondeComemos.Data;
using DondeComemos.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity; 
=======
using DondeComemos.Services;
using System.Text;
>>>>>>> b808e6f (Avance Mauricio Benavente)

namespace DondeComemos.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;
<<<<<<< HEAD
        private readonly UserManager<IdentityUser> _userManager;



        public RestaurantesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
{
    _context = context;
    _userManager = userManager;
}

=======
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;

        public RestaurantesController(
            ApplicationDbContext context, 
            IFileService fileService,
            IPdfService pdfService)
        {
            _context = context;
            _fileService = fileService;
            _pdfService = pdfService;
        }
>>>>>>> b808e6f (Avance Mauricio Benavente)

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
<<<<<<< HEAD
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

    // 🧩 Si el usuario está logueado, obtener sus favoritos
    List<int> favoritosIds = new();

    if (User.Identity != null && User.Identity.IsAuthenticated)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null) // ✅ Protección extra
        favoritosIds = await _context.Watchlists
            .Where(w => w.UserId == user.Id)
            .Select(w => w.RestauranteId)
            .ToListAsync();
    }

    // Pasar ambos datos a la vista
    ViewBag.FavoritosIds = favoritosIds;

    return View(restaurantes);
        }

        // --- CRUD (solo Admin) ---
=======
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
                .Include(r => r.Resenas)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurante == null)
                return NotFound();

            return View(restaurante);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExportarMenuPdf(int id)
        {
            var restaurante = await _context.Restaurantes
                .Include(r => r.Productos.Where(p => p.Disponible))
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurante == null)
                return NotFound();

            var html = _pdfService.GenerarMenuHtml(restaurante, restaurante.Productos.ToList());
            
            return Content(html, "text/html", Encoding.UTF8);
        }

>>>>>>> b808e6f (Avance Mauricio Benavente)
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
<<<<<<< HEAD
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
=======
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
>>>>>>> b808e6f (Avance Mauricio Benavente)
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
<<<<<<< HEAD
                _context.Update(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
=======
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
>>>>>>> b808e6f (Avance Mauricio Benavente)
            }
            return View(restaurante);
        }

        [Authorize(Roles = "Admin")]
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var restaurante = await _context.Restaurantes
        .FirstOrDefaultAsync(m => m.Id == id);
    if (restaurante == null)
    {
        return NotFound();
    }

    return View(restaurante);
        }

        [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var restaurante = await _context.Restaurantes.FindAsync(id);
    if (restaurante != null)
    {
        _context.Restaurantes.Remove(restaurante);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}
=======
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
                    .Include(r => r.Resenas)
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
>>>>>>> b808e6f (Avance Mauricio Benavente)
    }
}