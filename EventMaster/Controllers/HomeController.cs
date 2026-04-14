using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventMaster.Data;
using EventMaster.Models;
using System.Diagnostics;

namespace EventMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // БОНУС: Добавяме кеширане за 10 минути (600 секунди)
        // Това драстично намалява заявките към базата данни и ускорява сайта
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            // Взимаме топ събитията за началната страница
            // Включваме Venue и Category, за да се виждат на картичките
            var events = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .OrderBy(e => e.StartDate)
                .Take(6) // Взимаме първите 6, за да имаме избор за "Топ събития"
                .ToListAsync();

            return View(events);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Метод за страницата "За нас" - често се иска в Unit тестовете
        public IActionResult About()
        {
            return View();
        }

        // Метод за управление на грешките (Custom 404 и 500 страници)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return View("404"); // Трябва да имаш файл Views/Home/404.cshtml
            }

            if (statusCode == 500)
            {
                return View("500"); // Трябва да имаш файл Views/Home/500.cshtml
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}