using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // "Инжектираме" базата данни в контролера, за да можем да четем от нея
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Този метод показва списъка с всички събития
        public async Task<IActionResult> Index()
        {
            // Взимаме всички събития от базата данни и включваме техните Категория и Място
            var events = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .ToListAsync();

            return View(events);
        }
        // GET: Events/Create - Този метод показва празната форма
        public IActionResult Create()
        {
            // Подготвяме данните за падащите менюта
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name");
            return View();
        }

        // POST: Events/Create - Този метод приема попълнените данни и ги записва в базата
        [HttpPost]
        [ValidateAntiForgeryToken] // Защита от CSRF атаки (изискване по проект!)
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,ImageUrl,CategoryId,VenueId")] Event @event)
        {
            // Премахваме валидацията на навигационните свойства, защото те се зареждат по-късно
            ModelState.Remove("Category");
            ModelState.Remove("Venue");

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Връщаме се към списъка със събития
            }

            // Ако има грешка във формата, я показваме отново
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }
    }
}