using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudSplit.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudSplit.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnv;

        public UploadController(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        [HttpPost]
        [Route("UploadFile")]
        // "UploadFileComponent" is the value of the Upload's "Name" property.
        public ActionResult UploadFile(IFormFile file)
        {
            try
            {
                //Specify the target location for the uploaded files.
                string path = Path.Combine(_hostingEnv.WebRootPath, "Uploads");


                //Check the file here (its extension, safity, and so on).
                //Check whether the target directory exists; otherwise, create it.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                    using (FileStream fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                    {
                        //If all checks are passed, save the file.
                        file.CopyTo(fileStream);
                    }
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult DownloadFile()
        {
            return View();
        }
    }
}