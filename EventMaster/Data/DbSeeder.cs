using EventMaster.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EventMaster.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndDataAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var adminEmail = "admin@eventmaster.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Главeн",
                    LastName = "Администратор",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Administrator");
                }
            }
            string[] roleNames = { "Administrator", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(
                    new Category { Name = "Концерт" },
                    new Category { Name = "Театър" },
                    new Category { Name = "Спорт" },
                    new Category { Name = "Фестивал" },
                    new Category { Name = "Семинар" }
                );
                if (!dbContext.Venues.Any())
                {
                    dbContext.Venues.AddRange(
                        new Venue { Name = "Зала 1, НДК", Address = "пл. България 1, София", Capacity = 3380 },
                        new Venue { Name = "Арена София", Address = "бул. Асен Йорданов 1, София", Capacity = 15000 },
                        new Venue { Name = "Античен театър", Address = "Стария град, Пловдив", Capacity = 3000 }
                    );
                    await dbContext.SaveChangesAsync();
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}