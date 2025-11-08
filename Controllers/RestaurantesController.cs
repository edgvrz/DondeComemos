using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DondeComemos.Data;
using DondeComemos.Models;
<<<<<<< HEAD
using DondeComemos.Services;
using System.Text;
=======
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity; 
=======
using DondeComemos.Services;
using System.Text;
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7

namespace DondeComemos.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;
<<<<<<< HEAD
=======
<<<<<<< HEAD
        private readonly UserManager<IdentityUser> _userManager;



        public RestaurantesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
{
    _context = context;
    _userManager = userManager;
}

=======
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD

=======
>>>>>>> b808e6f (Avance Mauricio Benavente)

        // --- Vistas públicas (Clientes) ---
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var restaurantes = await _context.Restaurantes
<<<<<<< HEAD
                .OrderByDescending(r => r.Rating)
                .ToListAsync();
=======
                                             .OrderByDescending(r => r.Rating)
                                             .ToListAsync();
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7

            return View(restaurantes);
        }

        [AllowAnonymous]
<<<<<<< HEAD
        public async Task<IActionResult> Search(string? q, string? tipo, string? precio, double? rating)
        {
            var query = _context.Restaurantes.AsQueryable();

            // Buscar por texto general
=======
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

>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(r => r.Nombre.Contains(q) ||
                                         r.Direccion.Contains(q) ||
<<<<<<< HEAD
                                         r.Descripcion.Contains(q) ||
                                         r.TipoCocina.Contains(q));
            }

            // Filtrar por tipo de cocina
            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(r => r.TipoCocina == tipo);
            }

            // Filtrar por rango de precios
            if (!string.IsNullOrEmpty(precio))
            {
                query = query.Where(r => r.RangoPrecios == precio);
            }

            // Filtrar por rating mínimo
            if (rating.HasValue)
            {
                query = query.Where(r => r.Rating >= rating.Value);
=======
                                         r.Descripcion.Contains(q));
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
            }

            var restaurantes = await query.OrderByDescending(r => r.Rating).ToListAsync();

<<<<<<< HEAD
            // Pasar los parámetros a ViewBag para mantener los filtros
            ViewBag.SearchQuery = q;
            ViewBag.TipoFiltro = tipo;
            ViewBag.PrecioFiltro = precio;
            ViewBag.RatingFiltro = rating;

=======
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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

<<<<<<< HEAD
=======
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
=======
<<<<<<< HEAD
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
=======
                // Manejar subida de imagen
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD
                else
                {
                    restaurante.ImagenUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=600";
                }
=======
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7

                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Restaurante creado exitosamente";
<<<<<<< HEAD
=======
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
=======
<<<<<<< HEAD
                _context.Update(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
=======
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                try
                {
                    var restauranteExistente = await _context.Restaurantes.AsNoTracking()
                        .FirstOrDefaultAsync(r => r.Id == id);

<<<<<<< HEAD
=======
                    // Manejar nueva imagen
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                    if (restaurante.ImagenArchivo != null)
                    {
                        try
                        {
<<<<<<< HEAD
                            if (!string.IsNullOrEmpty(restauranteExistente?.ImagenUrl) &&
                                !restauranteExistente.ImagenUrl.StartsWith("http"))
=======
                            // Eliminar imagen anterior si existe
                            if (!string.IsNullOrEmpty(restauranteExistente?.ImagenUrl))
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD
=======
                        // Mantener imagen existente si no se sube una nueva
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD
=======
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
            }
            return View(restaurante);
        }

        [Authorize(Roles = "Admin")]
<<<<<<< HEAD
=======
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
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD
                    foreach (var producto in restaurante.Productos)
                    {
                        if (!string.IsNullOrEmpty(producto.ImagenUrl) &&
                            !producto.ImagenUrl.StartsWith("http"))
=======
                    // Eliminar imágenes de productos
                    foreach (var producto in restaurante.Productos)
                    {
                        if (!string.IsNullOrEmpty(producto.ImagenUrl))
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                        {
                            _fileService.DeleteImage(producto.ImagenUrl);
                        }
                    }

<<<<<<< HEAD
                    if (!string.IsNullOrEmpty(restaurante.ImagenUrl) &&
                        !restaurante.ImagenUrl.StartsWith("http"))
=======
                    // Eliminar imagen del restaurante
                    if (!string.IsNullOrEmpty(restaurante.ImagenUrl))
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
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
<<<<<<< HEAD
=======
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
    }
}