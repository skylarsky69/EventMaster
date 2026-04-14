using EventMaster.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventMaster.Tests
{
    public class EventsControllerTests : IDisposable
    {
        private ApplicationDbContext _context;
        private EventsController _controller;

        public EventsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // 1. Създаваме виртуална Категория и Място
            var testCategory = new Category { Id = 1, Name = "Тест Категория" };
            var testVenue = new Venue { Id = 1, Name = "Тест Зала", Address = "София", Capacity = 100 };

            _context.Categories.Add(testCategory);
            _context.Venues.Add(testVenue);

            // 2. Добавяме 8 събития, за да тестваме търсачката и страницирането
            for (int i = 1; i <= 8; i++)
            {
                _context.Events.Add(new Event
                {
                    Id = i,
                    // Четните ще са Рок, нечетните - Джаз
                    Title = i % 2 == 0 ? $"Рок Концерт {i}" : $"Джаз Вечер {i}",
                    // Първото събитие ще има специално описание
                    Description = i == 1 ? "Специално събитие в София" : "Стандартно описание",
                    StartDate = DateTime.Now.AddDays(i),
                    ImageUrl = "test.jpg",
                    CategoryId = 1,
                    Category = testCategory,
                    VenueId = 1,
                    Venue = testVenue
                });
            }

            _context.SaveChanges();
            _controller = new EventsController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        // ==========================================
        // ТЕСТОВЕ ЗА DETAILS (Детайли на събитие)
        // ==========================================

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenEventDoesNotExist()
        {
            var result = await _controller.Details(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithCorrectEvent()
        {
            var testEvent = await _context.Events.FirstOrDefaultAsync();
            Assert.NotNull(testEvent);

            var result = await _controller.Details(testEvent.Id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.Model);

            Assert.Equal(testEvent.Id, model.Id);
        }

        // ==========================================
        // ТЕСТОВЕ ЗА INDEX (Търсачка и Странициране)
        // ==========================================

        [Fact]
        public async Task Index_ReturnsCorrectPageSize_WhenNoSearchTermProvided()
        {
            // Act: Търсим без дума, страница 1
            var result = await _controller.Index(null, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

            // Тъй като имаме 8 събития, а лимитът е 6 на страница, очакваме точно 6
            Assert.Equal(6, model.Count());

            // Проверяваме дали ViewBag данните за страницирането са верни
            Assert.Equal(1, viewResult.ViewData["CurrentPage"]);
            Assert.Equal(2, viewResult.ViewData["TotalPages"]); // 8 събития / 6 = 2 страници
        }

        [Fact]
        public async Task Index_ReturnsFilteredEvents_WhenSearchingByTitle()
        {
            // Act: Търсим думата "Рок" (имаме 4 такива събития - 2, 4, 6, 8)
            var result = await _controller.Index("Рок", 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

            Assert.Equal(4, model.Count());
            Assert.All(model, e => Assert.Contains("Рок", e.Title));
        }

        [Fact]
        public async Task Index_ReturnsFilteredEvents_WhenSearchingByDescription()
        {
            // Act: Търсим думата "Специално" (само първото събитие го има в описанието)
            var result = await _controller.Index("Специално", 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

            // Трябва да намери точно 1 събитие
            Assert.Single(model);
            Assert.Equal("Джаз Вечер 1", model.First().Title);
        }
    }
}