using BetterOwner.Services.FileStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BetterOwner.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IFileStore _fs;

        public AttachmentController(IFileStore fs)
        {
            _fs = fs;
        }

        public List<FileItem> List()
        {
            return _fs.GetFiles();
        }

        public IActionResult Download(Guid id)
        {
            FileDownloadItem fileDownloadItem = _fs.Download(id);
            return File(fileDownloadItem.FileStream, fileDownloadItem.ContentType);
        }

        public IActionResult Upload()
        {
            return Ok(_fs.Upload(0, Request.Form.Files));
        }

        public void Delete(Guid id)
        {
            _fs.Delete(id);
        }
    }
}
