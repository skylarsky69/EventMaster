using Microsoft.AspNetCore.Mvc;
using EventMaster.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EventMaster.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // ТРИК: Речник, който пази билетите за всеки отделен потребител (Username -> (EventId, Seats))
        public static Dictionary<string, (int EventId, string Seats)> UserTickets = new();

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MyTickets()
        {
            // Взимаме името на логнатия потребител
            string username = User.Identity?.Name ?? "Guest";

            // Проверяваме дали ТОЗИ потребител има купен билет
            if (UserTickets.ContainsKey(username))
            {
                var ticketInfo = UserTickets[username];

                // Взимаме реалното събитие от базата данни
                var realEvent = _context.Events
                    .Include(e => e.Venue)
                    .Include(e => e.Category)
                    .FirstOrDefault(e => e.Id == ticketInfo.EventId);

                ViewBag.Event = realEvent;
                ViewBag.MySeats = ticketInfo.Seats;
            }
            else
            {
                // Ако няма билет, пращаме null
                ViewBag.Event = null;
            }

            return View();
        }
    }
}