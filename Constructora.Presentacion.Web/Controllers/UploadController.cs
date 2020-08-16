using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Controllers
{
    public class UploadController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly ILogger<UploadController> Logger;

        [Obsolete]
        public UploadController(IHostingEnvironment hostingEnvironment, ILogger<UploadController> logger)
        {
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Single([FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var path = HostingEnvironment.ContentRootPath;

                    Logger.LogInformation("Ruta: " + Path.Combine(path, @"Upload", file.FileName));

                    using var fileStream = new FileStream(Path.Combine(path, @"Upload", file.FileName), FileMode.Create, FileAccess.Write);
                    await file.CopyToAsync(fileStream);
                }

                return Ok(file);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.Message);
                return new EmptyResult();
            }
       
        }
    }
}
