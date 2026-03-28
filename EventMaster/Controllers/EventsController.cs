using EventMaster.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var eventsQuery = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                eventsQuery = eventsQuery.Where(e => e.Title.Contains(searchTerm)
                                                  || e.Description.Contains(searchTerm));
                ViewBag.CurrentSearch = searchTerm;
            }

            var events = await eventsQuery.ToListAsync();
            return View(events);
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
    }
}