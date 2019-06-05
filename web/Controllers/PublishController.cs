using BetterOwner.Services.TreasureManager;
using Microsoft.AspNetCore.Mvc;

namespace BetterOwner.Controllers
{
    public class PublishController : Controller
    {
        private readonly ITreasureManager treasureManager;

        public PublishController(ITreasureManager treasureManager)
        {
            this.treasureManager = treasureManager;
        }

        public void Create(TreasureCreateDto treasure)
        {
            treasureManager.Create(treasure, Request.Form.Files);
        }
    }
}
