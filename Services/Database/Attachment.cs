using System;

namespace BetterOwner.Services.Database
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public byte[] FileStream { get; set; }
    }
}
