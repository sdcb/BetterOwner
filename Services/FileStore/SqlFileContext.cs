namespace BetterOwner.Services.FileStore
{
    public class SqlFileContext
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string PathName { get; set; }

        public byte[] Context { get; set; }
    }
}
