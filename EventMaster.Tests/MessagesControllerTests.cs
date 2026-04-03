using EventMaster.Areas.Admin.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventMaster.Tests
{
    public class MessagesControllerTests
    {
        
        private ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithAllMessagesSortedByDate()
        {
            var db = GetDatabase();
            db.ContactMessages.Add(new ContactMessage { Id = 1, Name = "Иван", Email = "ivan@test.bg", Subject = "Въпрос", Message = "Здравейте 1", SentOn = DateTime.Now.AddDays(-1) });
            db.ContactMessages.Add(new ContactMessage { Id = 2, Name = "Петър", Email = "petar@test.bg", Subject = "Проблем", Message = "Здравейте 2", SentOn = DateTime.Now });
            await db.SaveChangesAsync();

            var controller = new MessagesController(db);

            var result = await controller.Index();

            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ContactMessage>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
            Assert.Equal("Петър", model.First().Name);
        }

        [Fact]
        public async Task Delete_ValidId_RemovesMessageFromDatabase()
        {
            var db = GetDatabase();
            var message = new ContactMessage { Id = 99, Name = "Георги", Email = "g@test.bg", Subject = "Тема", Message = "За триене" };
            db.ContactMessages.Add(message);
            await db.SaveChangesAsync();

            var controller = new MessagesController(db);

            var result = await controller.Delete(99);

            Assert.IsType<RedirectToActionResult>(result);

            var deletedMessage = await db.ContactMessages.FindAsync(99);
            Assert.Null(deletedMessage);
        }
    }
}