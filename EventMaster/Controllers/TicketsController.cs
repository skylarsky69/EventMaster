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

        
        public static Dictionary<string, (int EventId, string Seats)> UserTickets = new();

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MyTickets()
        {
            string username = User.Identity?.Name ?? "Guest";

            if (UserTickets.ContainsKey(username))
            {
                var ticketInfo = UserTickets[username];

                var realEvent = _context.Events
                    .Include(e => e.Venue)
                    .Include(e => e.Category)
                    .FirstOrDefault(e => e.Id == ticketInfo.EventId);

                ViewBag.Event = realEvent;
                ViewBag.MySeats = ticketInfo.Seats;
            }
            else
            {
                ViewBag.Event = null;
            }

            return View();
        }
    }
}