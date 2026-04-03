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
        // Метод за създаване на временна база в паметта
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
            // Arrange
            var db = GetDatabase();
            db.Categories.Add(new Category { Id = 1, Name = "Театър" });
            db.Categories.Add(new Category { Id = 2, Name = "Кино" });
            await db.SaveChangesAsync();

            var controller = new CategoriesController(db);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Create_ValidCategory_AddsToDatabaseAndRedirects()
        {
            // Arrange
            var db = GetDatabase();
            var controller = new CategoriesController(db);
            var newCategory = new Category { Id = 3, Name = "Фестивали" };

            // Act
            var result = await controller.Create(newCategory);

            // Assert
            // 1. Проверяваме дали пренасочва към Index (което значи успех)
            Assert.IsType<RedirectToActionResult>(result);

            // 2. Проверяваме дали категорията реално е в базата
            var categoryInDb = await db.Categories.FirstOrDefaultAsync(c => c.Name == "Фестивали");
            Assert.NotNull(categoryInDb);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var db = GetDatabase();
            var controller = new CategoriesController(db);

            // Симулираме грешка във валидацията (напр. празно име)
            controller.ModelState.AddModelError("Name", "Името е задължително");
            var invalidCategory = new Category { Id = 4, Name = "" };

            // Act
            var result = await controller.Create(invalidCategory);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidCategory, viewResult.Model);
            // Проверяваме, че НЕ е добавено в базата
            Assert.Empty(db.Categories);
        }
    }
}