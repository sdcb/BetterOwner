namespace BetterOwner.Services.FileStore
{
    public class SqlFileContext
    {
        public string FilePath { get; set; }

        public byte[] Context { get; set; }
    }
}
