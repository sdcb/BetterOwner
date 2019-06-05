using BetterOwner.Services.Database;
using BetterOwner.Services.FileStore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BetterOwner.Services.TreasurePictureStore
{
    public class EFTreasurePictureStore : ITreasurePictureStore
    {
        protected readonly ApplicationDbContext _db;

        public EFTreasurePictureStore(ApplicationDbContext db)
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

        public virtual int Upload(int treasureId, IFormFileCollection files)
        {
            var attachments = files.Select(x => new TreasurePicture
            {
                TreasureId = treasureId,
                FileName = x.FileName,
                ContentType = x.ContentType,
                FileSize = (int)x.Length,
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

        public List<FileItem> GetFiles(int treasureId)
        {
            return _db.TreasurePictures
                .Where(x => x.TreasureId == treasureId)
                .Select(x => new FileItem
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    ContentType = x.ContentType,
                    FileSize = x.FileStream.Length,
                }).ToList();
        }
    }
}
