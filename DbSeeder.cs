using VirtualEventTicketingSystem.Models;

namespace VirtualEventTicketingSystem.Data {
    public static class DbSeeder {
        public static void Seed(ApplicationDbContext db){
            if(db.Categories.Any()) return;
            var cat1 = new Category { Name = "Webinar", Description = "Online talks" };
            var cat2 = new Category { Name = "Concert", Description = "Music events" };
            db.Categories.AddRange(cat1, cat2);
            db.Events.AddRange(
                new Event { Title = "Intro to AI", Category = cat1.Name, Date = DateTime.Now.AddDays(7), Price = 10, AvailableTickets = 50 },
                new Event { Title = "Live Jazz Night", Category = cat2.Name, Date = DateTime.Now.AddDays(14), Price = 25, AvailableTickets = 30 },
                new Event { Title = "Cloud Workshop", Category = cat1.Name, Date = DateTime.Now.AddDays(3), Price = 15, AvailableTickets = 5 }
            );
            db.SaveChanges();
        }
    }
}
