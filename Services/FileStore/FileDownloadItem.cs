using System.IO;

namespace BetterOwner.Services.FileStore
{
    public class FileDownloadItem
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Stream FileStream { get; set; }
    }
}
