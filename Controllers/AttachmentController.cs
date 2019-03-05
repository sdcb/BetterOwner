using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace BetterOwner.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AttachmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public object List()
        {
            return _db.Attachment.Select(x => new
            {
                Id = x.Id, 
                FileName = x.FileName, 
                Size = x.FileStream.Length
            });
        }

        public IActionResult Download(Guid id)
        {
            var file = _db.Attachment.Find(id);
            return File(file.FileStream, "text/plain");
        }

        public IActionResult Upload()
        {
            var attachments = Request.Form.Files.Select(x => new Attachment
            {
                FileName = x.FileName,
                FileStream = ReadAll(x.OpenReadStream())
            });

            _db.Attachment.AddRange(attachments);
            var count = _db.SaveChanges();
            return Ok(count);


            byte[] ReadAll(Stream stream)
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }   
            }
        }

        public void Delete(Guid id)
        {
            _db.Remove(new Attachment { Id = id });
            _db.SaveChanges();
        }
    }
}
