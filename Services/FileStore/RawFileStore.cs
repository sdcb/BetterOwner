using Dapper;
using System;
using System.Data;
using System.IO;

namespace BetterOwner.Services.FileStore
{
    public class RawFileStore : IFileStore
    {
        private readonly IDbConnection _db;

        public RawFileStore(IDbConnection db)
        {
            _db = db;
        }

        public Stream Download(string collection, Guid fileId)
        {
            return new MemoryStream(_db.QueryFirst<byte[]>($"SELECT Stream FROM [{collection}] WHERE Id = '{fileId}'"));
        }

        public Guid Upload(string collection, Stream content)
        {
            _db.Query<Guid>($"INSERT INTO [{collection}] (Content) VALUES @")
        }
    }
}
