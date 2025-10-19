using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Models;
using DondeComemos.Data;


namespace DondeComemos.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WatchlistController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 📄 Mostrar favoritos
        public async Task<IActionResult> Index()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var favoritos = await _context.Watchlists
        .Include(w => w.Restaurante)
        .Where(w => w.UserId == user.Id)
        .ToListAsync();

    return View(favoritos);
        }

        // ➕ Agregar a favoritos
        [HttpPost]
public async Task<IActionResult> Add(int restauranteId)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    bool existe = await _context.Watchlists
        .AnyAsync(w => w.RestauranteId == restauranteId && w.UserId == user.Id);

    if (!existe)
    {
        _context.Watchlists.Add(new Watchlist
        {
            UserId = user.Id,
            RestauranteId = restauranteId,
            FechaAgregado = DateTime.Now
        });

        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Search", "Restaurantes");
        }

        // 🗑️ Eliminar de favoritos
        [HttpPost]
public async Task<IActionResult> Remove(int restauranteId)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var item = await _context.Watchlists
        .FirstOrDefaultAsync(w => w.RestauranteId == restauranteId && w.UserId == user.Id);

    if (item != null)
    {
        _context.Watchlists.Remove(item);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
        }
    }
}
