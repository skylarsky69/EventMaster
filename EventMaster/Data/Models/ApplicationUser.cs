using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventMaster.Data.Models
{
    // Наследяваме базовия потребител на ASP.NET Identity
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        // Потребителят може да има много поръчки
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}