using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventMaster.Controllers
{
    [Authorize] // Само логнати потребители могат да влизат тук!
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Orders/Buy
        // Този метод се извиква, когато потребител натисне "Купи билет"
        [HttpPost]
        public async Task<IActionResult> Buy(int eventId)
        {
            var @event = await _context.Events.FindAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            // Взимаме ID-то на текущия логнат потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 1. Създаваме нова поръчка
            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Запазваме, за да генерира ID на поръчката

            // 2. Създаваме самия билет (за момента слагаме твърда цена 50 лв. за простота)
            var ticket = new Ticket
            {
                Price = 50.00m,
                EventId = eventId,
                OrderId = order.Id
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Пренасочваме потребителя към страницата с неговите билети
            return RedirectToAction(nameof(MyOrders));
        }

        // GET: Orders/MyOrders
        // Показва всички закупени билети на текущия потребител
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.Event) // Включваме данните за събитието
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
    }
}