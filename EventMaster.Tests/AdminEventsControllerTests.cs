using EventMaster.Areas.Admin.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventMaster.Tests
{
    public class AdminEventsControllerTests
    {
        private ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task DeleteConfirmed_ValidId_RemovesEvent()
        {
            var db = GetDatabase();

           
            db.Categories.Add(new Category { Id = 1, Name = "Тест" });
            db.Venues.Add(new Venue { Id = 1, Name = "Място", Address = "Адрес" });

            var testEvent = new Event
            {
                Id = 55,
                Title = "За триене",
                Description = "Описание",
                StartDate = DateTime.Now.AddDays(1),
                ImageUrl = "img.jpg",
                CategoryId = 1,
                VenueId = 1
            };
            db.Events.Add(testEvent);
            await db.SaveChangesAsync();

            var controller = new EventsController(db);

            var result = await controller.DeleteConfirmed(55);

            Assert.IsType<RedirectToActionResult>(result);

            var deletedEvent = await db.Events.FindAsync(55);
            Assert.Null(deletedEvent);

        }
        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var db = GetDatabase();
            var controller = new EventsController(db); 

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            var db = GetDatabase();
            var controller = new EventsController(db);

            controller.ModelState.AddModelError("Title", "Заглавието е задължително");
            var invalidEvent = new Event { Id = 1, Description = "Описание без заглавие" };

            var result = await controller.Create(invalidEvent);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidEvent, viewResult.Model);
            Assert.Empty(db.Events);
        }

    }
}