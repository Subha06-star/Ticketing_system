using System.ComponentModel.DataAnnotations;

namespace VirtualEventTicketingSystem.Models {
    public class Event {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime Date { get; set; }
        [Range(0,9999)] public decimal Price { get; set; }
        [Range(0,10000)] public int AvailableTickets { get; set; }
    }
}
