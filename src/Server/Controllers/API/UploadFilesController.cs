using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers.API
{
    //[EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class UploadFilesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFileCollection uploads)
        {
            // Здесь будут лежать полученные файлы.
            IFormFileCollection UploadFiles;

            if (uploads.Count == 0)
                UploadFiles = HttpContext.Request.Form.Files;
            else
                UploadFiles = uploads;

            foreach (var uploadedFile in UploadFiles)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("AddFile");
        }
    }
}
