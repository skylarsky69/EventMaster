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

            var testCategory = new Category { Id = 1, Name = "Тест Категория" };
            var testVenue = new Venue { Id = 1, Name = "Тест Зала", Address = "София", Capacity = 100 };

            _context.Categories.Add(testCategory);
            _context.Venues.Add(testVenue);

         
            for (int i = 1; i <= 8; i++)
            {
                _context.Events.Add(new Event
                {
                    Id = i,
                    
                    Title = i % 2 == 0 ? $"Рок Концерт {i}" : $"Джаз Вечер {i}",
                    
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

        [Fact]
        public async Task Index_ReturnsCorrectPageSize_WhenNoSearchTermProvided()
        {
            var result = await _controller.Index(null, 1);

       
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

            Assert.Equal(6, model.Count());

            Assert.Equal(1, viewResult.ViewData["CurrentPage"]);
            Assert.Equal(2, viewResult.ViewData["TotalPages"]); 
        }

        [Fact]
        public async Task Index_ReturnsFilteredEvents_WhenSearchingByTitle()
        {
            var result = await _controller.Index("Рок", 1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

            Assert.Equal(4, model.Count());
            Assert.All(model, e => Assert.Contains("Рок", e.Title));
        }

        [Fact]
        public async Task Index_ReturnsFilteredEvents_WhenSearchingByDescription()
        {
            
            var result = await _controller.Index("Специално", 1);


            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(viewResult.Model);

           
            Assert.Single(model);
            Assert.Equal("Джаз Вечер 1", model.First().Title);
        }
    }
}