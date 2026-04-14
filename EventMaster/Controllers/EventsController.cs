using EventMaster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaster.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Добавяме параметри searchTerm и page (по подразбиране е 1-ва страница)
        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            int pageSize = 6; // Показваме по 6 събития на страница (можеш да го промениш)

            var events = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .AsQueryable();

            // Логика за търсачката
            if (!string.IsNullOrEmpty(searchTerm))
            {
                events = events.Where(e => e.Title.Contains(searchTerm) || e.Description.Contains(searchTerm));
            }

            // Логика за странициране
            int totalEvents = await events.CountAsync();
            int totalPages = (int)Math.Ceiling(totalEvents / (double)pageSize);

            // Взимаме само събитията за текущата страница
            var eventsToDisplay = await events
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Изпращаме данните към изгледа (View) чрез ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return View(eventsToDisplay);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // --- НОВИЯТ МЕТОД ЗА ИЗБОР НА МЕСТА ---
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            // Връщаме изгледа Book.cshtml и му подаваме данните за събитието
            return View(eventItem);
        }
    }
}