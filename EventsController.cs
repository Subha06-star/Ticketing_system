using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualEventTicketingSystem.Data;
using VirtualEventTicketingSystem.Models;

namespace VirtualEventTicketingSystem.Controllers {
    public class EventsController : Controller {
        private readonly ApplicationDbContext _db;
        public EventsController(ApplicationDbContext db){ _db = db; }

        public IActionResult Index(string? q, string? category, string? sort){
            var query = _db.Events.AsQueryable();
            if(!string.IsNullOrEmpty(q)) query = query.Where(e=>EF.Functions.Like(e.Title, $"%{q}%"));
            if(!string.IsNullOrEmpty(category)) query = query.Where(e=>e.Category==category);
            if(sort=="price") query = query.OrderBy(e=>e.Price);
            else if(sort=="date") query = query.OrderBy(e=>e.Date);
            else query = query.OrderBy(e=>e.Title);
            return View(query.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Event e){
            if(ModelState.IsValid){
                _db.Events.Add(e);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e);
        }

        [HttpPost]
        public IActionResult SearchAjax([FromBody] dynamic body){
            string q = body?.q ?? "";
            var results = _db.Events.Where(e=>e.Title.Contains((string)q)).Select(e=>new { id=e.Id, title=e.Title, price=e.Price }).ToList();
            return Json(results);
        }
    }
}
