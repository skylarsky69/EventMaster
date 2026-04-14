using Microsoft.AspNetCore.Mvc;

namespace EventMaster.Controllers
{
    public class CartController : Controller
    {
        // Този метод показва самата количка (GET)
        public IActionResult Index()
        {
            // Взимаме запазените данни, ако има такива
            ViewBag.SelectedSeats = TempData["SelectedSeats"]?.ToString();
            ViewBag.EventId = TempData["EventId"]?.ToString();

            return View();
        }

        // Този метод "улавя" кликането на бутона от залата (POST)
        [HttpPost]
        public IActionResult Add(int EventId, string SelectedSeats)
        {
            // Запазваме данните временно в TempData
            TempData["SelectedSeats"] = SelectedSeats;
            TempData["EventId"] = EventId.ToString();

            // Пренасочваме потребителя към страницата на количката (Index метода горе)
            return RedirectToAction("Index");
        }
        // Този метод симулира плащането
        [HttpPost]
        public IActionResult Pay()
        {
            // Тук в реална ситуация бихме записали поръчката в базата 
            // и бихме се свързали със Stripe/PayPal.

            // Засега просто "изчистваме" количката
            TempData.Remove("SelectedSeats");
            TempData.Remove("EventId");

            // Пренасочваме към финалния екран
            return RedirectToAction("Success");
        }

        // Този метод показва финалния екран за успех
        public IActionResult Success()
        {
            return View();
        }
    }
}