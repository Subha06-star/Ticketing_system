namespace VirtualEventTicketingSystem.Models {
    public class Purchase {
        public int Id { get; set; }
        public string GuestName { get; set; } = "";
        public string GuestEmail { get; set; } = "";
        public int EventId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
