using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Mvc;

namespace BetterOwner.Controllers
{
    public class PublishController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublishController(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Treasure treasure)
        {
            _db.Add(treasure);
            _db.SaveChanges();
        }
    }
}
