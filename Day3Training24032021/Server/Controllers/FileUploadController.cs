using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController
    {
        private readonly IWebHostEnvironment env;

        public FileUploadController(IWebHostEnvironment _env)
        {
            env = _env;
        }

        [HttpPost]
        public void Post(UploadedFile uploadedFile)
        {

            var path = $"{env.WebRootPath}\\{uploadedFile.FileName}";
            var fs = System.IO.File.Create(path);
            fs.Write(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            fs.Close();
        }



    }
}
