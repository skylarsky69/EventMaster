using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventMaster.Controllers
{
    // Този атрибут гарантира, че само логнати потребители могат да виждат билетите си
    [Authorize]
    public class TicketsController : Controller
    {
        public IActionResult MyTickets()
        {
            // Тук в реална ситуация ще извлечем билетите от базата данни:
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var tickets = _context.Tickets.Where(t => t.UserId == userId).ToList();

            return View();
        }
    }
}