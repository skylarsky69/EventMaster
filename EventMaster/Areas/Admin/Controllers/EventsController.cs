using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,ImageUrl,CategoryId,VenueId")] Event @event)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Venue");

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Events", new { area = "" });
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,StartDate,ImageUrl,CategoryId,VenueId")] Event @event)
        {
            if (id != @event.Id) return NotFound();

            ModelState.Remove("Category");
            ModelState.Remove("Venue");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Events.Any(e => e.Id == @event.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index", "Events", new { area = "" });
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Events", new { area = "" });
        }
    }
}