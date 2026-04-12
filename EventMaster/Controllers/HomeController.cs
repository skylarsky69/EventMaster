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

        public async Task<IActionResult> Index()
        {
            // Взимаме събитията за началната страница
            var events = await _context.Events
                .Include(e => e.Venue)
                .OrderBy(e => e.StartDate)
                .Take(4)
                .ToListAsync();

            return View(events);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ЕТО ГО ЛИПСВАЩИЯТ МЕТОД ЗА ТЕСТОВЕТЕ
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}