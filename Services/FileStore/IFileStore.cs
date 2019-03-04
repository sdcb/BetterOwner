using System;
using System.IO;

namespace BetterOwner.Services.FileStore
{
    public interface IFileStore
    {
        Stream Download(string collection, Guid fileId);

        Guid Upload(string collection, Stream content);
    }
}
