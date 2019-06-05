using BetterOwner.Services.Database;
using System.IO;

namespace BetterOwner.Services.FileStore
{
    public class FileDownloadItem
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Stream FileStream { get; set; }

        public static FileDownloadItem FromTreasurePicture(TreasurePicture picture)
        {
            return new FileDownloadItem
            {
                ContentType = picture.ContentType, 
                FileName = picture.FileName, 
                FileStream = new MemoryStream(picture.FileStream), 
            };
        }
    }
}
