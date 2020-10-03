using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MrJuerga.Api.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public static IHostingEnvironment _enviroment;

        public FileUploadController(IHostingEnvironment enviroment)
        {


            _enviroment = enviroment;
        }

        public class FileUpLoad
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUpLoad objfile)
        {
            if (objfile.files.Length > 0)
            {
                try
                {
                    string path = @"C:\Users\foi12\Music\MrJuerga\files\";                   

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objfile.files.FileName))
                    {
                        objfile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return path + objfile.files.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            else
            {
                return "Failed";
            }
        }
    }
}