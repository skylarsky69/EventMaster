using System.ComponentModel.DataAnnotations;

namespace EventMaster.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

      
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}