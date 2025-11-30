using Microsoft.AspNetCore.Mvc;
using VirtualEventTicketingSystem.Data;
using VirtualEventTicketingSystem.Models;

namespace VirtualEventTicketingSystem.Controllers {
    public class PurchasesController : Controller {
        private readonly ApplicationDbContext _db;
        public PurchasesController(ApplicationDbContext db){ _db = db; }

        public IActionResult Create(int eventId){
            var ev = _db.Events.Find(eventId);
            return View(ev);
        }

        [HttpPost]
        public IActionResult Create(int eventId, string guestName, string guestEmail, int quantity){
            var ev = _db.Events.Find(eventId);
            if(ev == null) return NotFound();
            if(quantity > ev.AvailableTickets) ModelState.AddModelError("quantity","Not enough tickets");
            if(!ModelState.IsValid) return View(ev);

            var purchase = new Purchase {
                EventId = eventId,
                GuestName = guestName,
                GuestEmail = guestEmail,
                Quantity = quantity,
                PurchaseDate = DateTime.Now
            };
            ev.AvailableTickets -= quantity;
            _db.Purchases.Add(purchase);
            _db.SaveChanges();
            return RedirectToAction("Success");
        }

        public IActionResult Success() => View();
    }
}
