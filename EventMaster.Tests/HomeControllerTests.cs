using EventMaster.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Microsoft.AspNetCore.Http; 
using EventMaster.Models;

namespace EventMaster.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

           
            var result = controller.Index();

           
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_ReturnsViewResult()
        {
            
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            
            var result = controller.About();

            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            var result = controller.Privacy();

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Error_ReturnsViewResult_WithModel()
        {
            var logger = NullLogger<HomeController>.Instance;
            var controller = new HomeController(logger);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var result = controller.Error();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ErrorViewModel>(viewResult.Model);
        }
    }
}