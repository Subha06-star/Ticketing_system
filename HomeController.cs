using Microsoft.AspNetCore.Mvc;
using VirtualEventTicketingSystem.Data;

namespace VirtualEventTicketingSystem.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db){ _db = db; }
        public IActionResult Index(){
            var upcoming = _db.Events.OrderBy(e=>e.Date).Take(5).ToList();
            ViewBag.Categories = _db.Categories.ToList();
            return View(upcoming);
        }
        public IActionResult Error() => View();
    }
}
