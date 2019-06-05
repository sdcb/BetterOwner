using System;

namespace BetterOwner.Services.Database
{
    public class TreasurePicture
    {
        public Guid Id { get; set; }

        public int TreasureId { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public string ContentType { get; set; }

        public byte[] FileStream { get; set; }

        public Treasure Treasure { get; set; }
    }
}
