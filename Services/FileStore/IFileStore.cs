using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BetterOwner.Services.FileStore
{
    public interface IFileStore
    {
        List<FileItem> GetFiles();

        FileDownloadItem Download(Guid id);

        void Delete(Guid id);

        int Upload(int treasureId, IFormFileCollection files);
    }
}
