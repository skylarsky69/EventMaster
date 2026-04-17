using Microsoft.AspNetCore.Mvc;

namespace EventMaster.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            
            ViewBag.SelectedSeats = TempData.Peek("SelectedSeats")?.ToString();
            ViewBag.EventId = TempData.Peek("EventId")?.ToString();

            return View();
        }

        [HttpPost]
        public IActionResult Add(int EventId, string SelectedSeats)
        {
            TempData["SelectedSeats"] = SelectedSeats;
            TempData["EventId"] = EventId.ToString();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Pay(int eventId, string selectedSeats)
        {
           
            string username = User.Identity?.Name ?? "Guest";

            
            TicketsController.UserTickets[username] = (eventId, selectedSeats);

            TempData.Remove("SelectedSeats");
            TempData.Remove("EventId");

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}