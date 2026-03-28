using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventMaster.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Вашето съобщение беше изпратено успешно!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}