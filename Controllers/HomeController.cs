using Microsoft.AspNetCore.Mvc;
using DondeComemos.Models;
using DondeComemos.Services;

namespace DondeComemos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            var model = _homeService.GetHomeData();
            return View(model);
        }
    }
}
