using EventMaster.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EventMaster.Tests
{
    public class ContactControllerTests
    {
        private ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Index_Get_ReturnsViewResult()
        {
           
            var db = GetDatabase();
            var controller = new ContactController(db);

           
            var result = controller.Index();

      
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Index_Post_ValidModel_SavesToDatabaseAndRedirects()
        {
          
            var db = GetDatabase();
            var controller = new ContactController(db);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var newMessage = new ContactMessage
            {
                Name = "Анна",
                Email = "anna@test.bg",
                Subject = "Въпрос",
                Message = "Супер сайт!"
            };

         
            var result = await controller.Index(newMessage);

            Assert.IsType<RedirectToActionResult>(result);

           
            var savedMessage = await db.ContactMessages.FirstOrDefaultAsync(m => m.Name == "Анна");
            Assert.NotNull(savedMessage);
            Assert.Equal("Супер сайт!", savedMessage.Message);

          
            Assert.True(controller.TempData.ContainsKey("SuccessMessage"));
        }

        [Fact]
        public async Task Index_Post_InvalidModel_ReturnsViewWithModel()
        {
           
            var db = GetDatabase();
            var controller = new ContactController(db);
            controller.ModelState.AddModelError("Email", "Невалиден имейл"); 

            var invalidMessage = new ContactMessage { Name = "Анна" }; 

         
            var result = await controller.Index(invalidMessage);

          
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidMessage, viewResult.Model);
           
            Assert.Empty(db.ContactMessages);
        }
    }
}