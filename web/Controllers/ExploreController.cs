using BetterOwner.Services.FileStore;
using BetterOwner.Services.TreasureManager;
using BetterOwner.Services.TreasurePictureStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BetterOwner.Controllers
{
    public class ExploreController : Controller
    {
        private readonly ITreasureManager treasureManager;
        private readonly ITreasurePictureStore pictureStore;

        public ExploreController(
            ITreasureManager treasureManager, 
            ITreasurePictureStore pictureStore)
        {
            this.treasureManager = treasureManager;
            this.pictureStore = pictureStore;
        }

        public List<TreasureExploreDto> Treasures(TreasureQuery query)
        {
            return treasureManager.Query(query);
        }

        public IActionResult Picture(Guid id)
        {
            FileDownloadItem item = pictureStore.Download(id);
            return File(item.FileStream, item.ContentType);
        }

        public IActionResult Download(Guid id)
        {
            FileDownloadItem item = pictureStore.Download(id);
            return File(item.FileStream, item.ContentType, item.FileName);
        }
    }
}
