using System;

namespace BetterOwner.Services.FileStore
{
    public class FileItem
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public int FileSize { get; set; }
    }
}
