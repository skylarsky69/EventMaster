using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace EventMaster.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // Връзка с потребителя, който прави поръчката
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        // Билетите, включени в тази поръчка
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}