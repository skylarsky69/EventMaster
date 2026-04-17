using EventMaster.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 101, Name = "Музика" },
                new Category { Id = 102, Name = "Театър" },
                new Category { Id = 103, Name = "Спорт" },
                new Category { Id = 104, Name = "Фестивали" }
            );

            builder.Entity<Venue>().HasData(
                new Venue { Id = 101, Name = "Арена София", Address = "бул. Асен Йорданов 1" },
                new Venue { Id = 102, Name = "Народен Театър", Address = "ул. Дякон Игнатий 5" }
            );
        }
    }
}