using System.ComponentModel.DataAnnotations;

namespace EventMaster.Data.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Темата е задължителна")]
        [MaxLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Съобщението е задължително")]
        [MaxLength(2000)]
        public string Message { get; set; } = string.Empty;

        public DateTime SentOn { get; set; } = DateTime.Now;
    }
}