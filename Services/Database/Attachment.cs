using BetterOwner.Services.FileStore;
using System;
using System.IO;

namespace BetterOwner.Services.Database
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] FileStream { get; set; }

        public FileDownloadItem ToFileDownloadItem()
        {
            return new FileDownloadItem
            {
                ContentType = ContentType, 
                FileName = FileName, 
                FileStream = new MemoryStream(FileStream), 
            };
        }
    }
}
