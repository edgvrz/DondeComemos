using DondeComemos.Data;
using Microsoft.AspNetCore.Mvc;

namespace DondeComemos.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search)
        {
            var query = _context.Restaurantes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r =>
                    r.Nombre.Contains(search) ||
                    r.Descripcion.Contains(search) ||
                    r.Direccion.Contains(search));
            }

            return View(query.ToList());
        }

        public IActionResult Detalle(int id)
        {
            var restaurante = _context.Restaurantes.FirstOrDefault(r => r.Id == id);
            if (restaurante == null) return NotFound();

            return View(restaurante);
        }
    }
}
