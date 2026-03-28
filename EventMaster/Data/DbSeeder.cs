using EventMaster.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EventMaster.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndDataAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Създаване на задължителните роли
            string[] roleNames = { "Administrator", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Добавяне на първоначални категории за събитията (Seeding Data)
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(
                    new Category { Name = "Концерт" },
                    new Category { Name = "Театър" },
                    new Category { Name = "Спорт" },
                    new Category { Name = "Фестивал" },
                    new Category { Name = "Семинар" }
                );
                await dbContext.SaveChangesAsync();
            }
        }
    }
}