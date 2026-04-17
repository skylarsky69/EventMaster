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

        
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
      
            var events = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .OrderBy(e => e.StartDate)
                .Take(6) 
                .ToListAsync();

            return View(events);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return View("404"); 
            }

            if (statusCode == 500)
            {
                return View("500"); 
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}