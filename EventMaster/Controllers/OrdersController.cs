using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventMaster.Controllers
{
    [Authorize] 
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public async Task<IActionResult> Buy(int eventId)
        {
            var @event = await _context.Events.FindAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); 

            var ticket = new Ticket
            {
                Price = 50.00m,
                EventId = eventId,
                OrderId = order.Id
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyOrders));
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.Event) 
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
    }
}