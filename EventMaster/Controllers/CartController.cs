using Microsoft.AspNetCore.Mvc;

namespace EventMaster.Controllers
{
    public class CartController : Controller
    {
        // Този метод показва самата количка (GET)
        public IActionResult Index()
        {
            // Взимаме запазените данни от TempData
            // Използваме Peek, за да не се изтрият данните при рефреш на страницата преди плащане
            ViewBag.SelectedSeats = TempData.Peek("SelectedSeats")?.ToString();
            ViewBag.EventId = TempData.Peek("EventId")?.ToString();

            return View();
        }

        // Този метод "улавя" избраните места от залата и ги праща към количката
        [HttpPost]
        public IActionResult Add(int EventId, string SelectedSeats)
        {
            // Запазваме данните временно в TempData
            TempData["SelectedSeats"] = SelectedSeats;
            TempData["EventId"] = EventId.ToString();

            return RedirectToAction("Index");
        }

        // Този метод обработва плащането и записва билета за текущия потребител
        [HttpPost]
        public IActionResult Pay(int eventId, string selectedSeats)
        {
            // 1. Взимаме името на логнатия потребител (ако не е логнат, ползваме "Guest")
            string username = User.Identity?.Name ?? "Guest";

            // 2. Записваме билета в "речника" на TicketsController, свързан с този Username
            // Това гарантира, че Профил А няма да вижда билетите на Профил Б
            TicketsController.UserTickets[username] = (eventId, selectedSeats);

            // 3. Изчистваме количката след успешно плащане
            TempData.Remove("SelectedSeats");
            TempData.Remove("EventId");

            return RedirectToAction("Success");
        }

        // Този метод показва финалния екран за успех
        public IActionResult Success()
        {
            return View();
        }
    }
}