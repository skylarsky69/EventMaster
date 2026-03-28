using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        // Връзка със събитието
        [Required]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        // Връзка с поръчката (може да е null, защото билетът в началото не е купен)
        public int? OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}