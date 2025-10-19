using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DondeComemos.Data;
using DondeComemos.Models;
using Microsoft.AspNetCore.Identity; 

namespace DondeComemos.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;



        public RestaurantesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
{
    _context = context;
    _userManager = userManager;
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
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
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
                _context.Update(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurante);
        }

        [Authorize(Roles = "Admin")]
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
    }
}