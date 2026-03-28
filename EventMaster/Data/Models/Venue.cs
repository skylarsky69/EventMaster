using System.ComponentModel.DataAnnotations;

namespace EventMaster.Data.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000)]
        public int Capacity { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}