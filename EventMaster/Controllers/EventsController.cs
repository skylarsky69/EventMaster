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

        
        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            int pageSize = 6; 

            var events = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                events = events.Where(e => e.Title.Contains(searchTerm) || e.Description.Contains(searchTerm));
            }

            int totalEvents = await events.CountAsync();
            int totalPages = (int)Math.Ceiling(totalEvents / (double)pageSize);

            var eventsToDisplay = await events
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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

        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

           
            return View(eventItem);
        }
    }
}