using EventMaster.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EventMaster.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange (Подготовка: използваме "празен" логър, за да не гърми)
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            // Act (Действие)
            var result = controller.Index();

            // Assert (Проверка)
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_ReturnsViewResult()
        {
            // Arrange
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            // Act
            var result = controller.About();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            // Arrange
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            // Act
            var result = controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}