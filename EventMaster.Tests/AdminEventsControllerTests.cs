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
            // Arrange
            var db = GetDatabase();

            // Задължително добавяме Категория и Място
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

            // ВАЖНО: Извикваме контролера от Area("Admin")
            var controller = new EventsController(db);

            // Act
            var result = await controller.DeleteConfirmed(55);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);

            // Проверяваме дали събитието вече НЕ съществува в базата
            var deletedEvent = await db.Events.FindAsync(55);
            Assert.Null(deletedEvent);

        }
        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var db = GetDatabase();
            var controller = new EventsController(db); // Това е админският контролер

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var db = GetDatabase();
            var controller = new EventsController(db);

            // Симулираме грешка - липсва заглавие
            controller.ModelState.AddModelError("Title", "Заглавието е задължително");
            var invalidEvent = new Event { Id = 1, Description = "Описание без заглавие" };

            // Act
            var result = await controller.Create(invalidEvent);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidEvent, viewResult.Model);
            // Проверяваме, че събитието не е записано в базата
            Assert.Empty(db.Events);
        }

    }
}