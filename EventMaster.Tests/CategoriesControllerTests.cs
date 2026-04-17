using EventMaster.Areas.Admin.Controllers;
using EventMaster.Data;
using EventMaster.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventMaster.Tests
{
    public class CategoriesControllerTests
    {
        private ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithAllCategories()
        {
            var db = GetDatabase();
            db.Categories.Add(new Category { Id = 1, Name = "Театър" });
            db.Categories.Add(new Category { Id = 2, Name = "Кино" });
            await db.SaveChangesAsync();

            var controller = new CategoriesController(db);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Create_ValidCategory_AddsToDatabaseAndRedirects()
        {
            var db = GetDatabase();
            var controller = new CategoriesController(db);
            var newCategory = new Category { Id = 3, Name = "Фестивали" };

            var result = await controller.Create(newCategory);

           
            
            Assert.IsType<RedirectToActionResult>(result);

            var categoryInDb = await db.Categories.FirstOrDefaultAsync(c => c.Name == "Фестивали");
            Assert.NotNull(categoryInDb);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsViewWithModel()
        {
          
            var db = GetDatabase();
            var controller = new CategoriesController(db);

            controller.ModelState.AddModelError("Name", "Името е задължително");
            var invalidCategory = new Category { Id = 4, Name = "" };

         
            var result = await controller.Create(invalidCategory);

           
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidCategory, viewResult.Model);
            Assert.Empty(db.Categories);
        }
        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            
            var db = GetDatabase();
            var controller = new CategoriesController(db);

           
            var result = await controller.Edit(null);

        
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsView_WhenIdIsValid()
        {
        
            var db = GetDatabase();
            db.Categories.Add(new Category { Id = 10, Name = "Изкуство" });
            await db.SaveChangesAsync();

            var controller = new CategoriesController(db);

           
            var result = await controller.Edit(10);

          
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Category>(viewResult.Model);
            Assert.Equal("Изкуство", model.Name);
        }
    }
}