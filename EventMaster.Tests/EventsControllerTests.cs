using EventMaster.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventMaster.Tests
{
    public class EventsControllerTests
    {
        private ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsAllEvents_WhenSearchTermIsEmpty()
        {
            // 1. Arrange
            var db = GetDatabase();

            // Първо добавяме Категория и Място, защото са задължителни (Required)
            var category = new Category { Id = 1, Name = "Тест Категория" };
            var venue = new Venue { Id = 1, Name = "Тест Място", Address = "Адрес" };
            db.Categories.Add(category);
            db.Venues.Add(venue);

            db.Events.Add(new Event { Title = "Парти", Description = "Описание", StartDate = DateTime.Now.AddDays(1), ImageUrl = "test.jpg", CategoryId = 1, VenueId = 1 });
            db.Events.Add(new Event { Title = "Концерт", Description = "Описание", StartDate = DateTime.Now.AddDays(2), ImageUrl = "test.jpg", CategoryId = 1, VenueId = 1 });
            await db.SaveChangesAsync();

            var controller = new EventsController(db);

            // 2. Act
            var result = await controller.Index(null);

            // 3. Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_FiltersEvents_BySearchTerm()
        {
            // 1. Arrange
            var db = GetDatabase();

            var category = new Category { Id = 1, Name = "Тест Категория" };
            var venue = new Venue { Id = 1, Name = "Тест Място", Address = "Адрес" };
            db.Categories.Add(category);
            db.Venues.Add(venue);

            db.Events.Add(new Event { Title = "Рок Концерт", Description = "Музика", StartDate = DateTime.Now.AddDays(1), ImageUrl = "test.jpg", CategoryId = 1, VenueId = 1 });
            db.Events.Add(new Event { Title = "Опера", Description = "Култура", StartDate = DateTime.Now.AddDays(2), ImageUrl = "test.jpg", CategoryId = 1, VenueId = 1 });
            await db.SaveChangesAsync();

            var controller = new EventsController(db);

            // 2. Act
            var result = await controller.Index("Рок");

            // 3. Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.ViewData.Model);
            Assert.Single(model);
            Assert.Equal("Рок Концерт", model.First().Title);
        }
        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var db = GetDatabase();
            var controller = new EventsController(db);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewWithEvent_WhenIdIsValid()
        {
            // Arrange
            var db = GetDatabase();
            var category = new Category { Id = 1, Name = "Тест" };
            var venue = new Venue { Id = 1, Name = "Място", Address = "Адрес" };
            db.Categories.Add(category);
            db.Venues.Add(venue);

            var testEvent = new Event { Id = 99, Title = "Специално събитие", Description = "Детайли", StartDate = DateTime.Now.AddDays(1), ImageUrl = "img.jpg", CategoryId = 1, VenueId = 1 };
            db.Events.Add(testEvent);
            await db.SaveChangesAsync();

            var controller = new EventsController(db);

            // Act
            var result = await controller.Details(99);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Event>(viewResult.ViewData.Model);
            Assert.Equal("Специално събитие", model.Title);
        }
    }
}