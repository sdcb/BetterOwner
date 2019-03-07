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
        protected readonly ApplicationDbContext _db;

        public EFFileStore(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Delete(Guid id)
        {
            _db.Remove(new TreasurePicture { Id = id });
            _db.SaveChanges();
        }

        public virtual FileDownloadItem Download(Guid id)
        {
            TreasurePicture file = _db.TreasurePictures.Find(id);
            return FileDownloadItem.FromTreasurePicture(file);
        }

        public virtual int Upload(IFormFileCollection files)
        {
            var attachments = files.Select(x => new TreasurePicture
            {
                FileName = x.FileName,
                ContentType = x.ContentType, 
                FileStream = ReadAll(x.OpenReadStream())
            });

            _db.TreasurePictures.AddRange(attachments);
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
            return _db.TreasurePictures.Select(x => new FileItem
            {
                Id = x.Id, 
                FileName = x.FileName, 
                ContentType = x.ContentType, 
                FileSize = x.FileStream.Length, 
            }).ToList();
        }
    }
}
