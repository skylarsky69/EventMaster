using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието е задължително.")]
        [MaxLength(100, ErrorMessage = "Заглавието не може да е над 100 символа.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описанието е задължително.")]
        [MaxLength(1000, ErrorMessage = "Описанието не може да е над 1000 символа.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Датата на започване е задължителна.")]
        [Display(Name = "Дата на започване")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Линкът към снимка е задължителен.")]
        [Url(ErrorMessage = "Моля, въведете валиден линк (URL).")]
        public string ImageUrl { get; set; } = string.Empty;

        // Връзка с Категория
        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        // Връзка с Място
        [Required(ErrorMessage = "Моля, изберете място на провеждане.")]
        public int VenueId { get; set; }

        [ForeignKey(nameof(VenueId))]
        public Venue Venue { get; set; } = null!;
    }
}