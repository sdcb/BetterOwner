using BetterOwner.Services.FileStore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BetterOwner.Services.TreasurePictureStore
{
    public interface ITreasurePictureStore
    {
        List<FileItem> GetFiles(int treasureId);

        FileDownloadItem Download(Guid id);

        void Delete(Guid id);

        int Upload(int treasureId, IFormFileCollection files);
    }
}
