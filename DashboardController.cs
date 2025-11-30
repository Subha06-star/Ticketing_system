using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualEventTicketingSystem.Data;

namespace VirtualEventTicketingSystem.Controllers {
    public class DashboardController : Controller {
        private readonly ApplicationDbContext _db;
        public DashboardController(ApplicationDbContext db){ _db = db; }

        public IActionResult Index(){
            var totalEvents = _db.Events.Count();
            var lowStock = _db.Events.Where(e=>e.AvailableTickets < 5).ToList();
            ViewBag.TotalEvents = totalEvents;
            ViewBag.LowStock = lowStock;
            return View();
        }

        public IActionResult SalesByCategory(){
            var data = _db.Events
                .GroupBy(e=>e.Category)
                .Select(g=> new { Category = g.Key, Count = g.Sum(e=> (int?) ( (from p in _db.Purchases where p.EventId==g.FirstOrDefault().Id select p.Quantity).Sum() ?? 0) ) })
                .ToList();
            return Json(data);
        }
    }
}
