using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BetterOwner.Services.FileStore
{
    public class EFFileStore : IFileStore
    {
        private readonly ApplicationDbContext _db;

        public EFFileStore(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Delete(Guid id)
        {
            _db.Remove(new Attachment { Id = id });
            _db.SaveChanges();
        }

        public Stream Download(Guid id)
        {
            var file = _db.Attachment.Find(id);
            return new MemoryStream(file.FileStream);
        }

        public int Upload(IFormFileCollection files)
        {
            var attachments = files.Select(x => new Attachment
            {
                FileName = x.FileName,
                FileStream = ReadAll(x.OpenReadStream())
            });

            _db.Attachment.AddRange(attachments);
            return _db.SaveChanges();

            byte[] ReadAll(Stream stream)
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public List<FileItem> GetFiles()
        {
            return _db.Attachment.Select(x => new FileItem
            {
                Id = x.Id, 
                FileName = x.FileName, 
                FileSize = x.FileStream.Length, 
            }).ToList();
        }
    }
}
