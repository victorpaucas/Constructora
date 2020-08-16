using Constructora.Presentacion.Web.Models;
using Constructora.Presentacion.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Controllers
{
    public class FileController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IFileRepository FileRepository;

        [Obsolete]
        public FileController(IHostingEnvironment hostingEnvironment, IFileRepository fileRepository)
        {
            HostingEnvironment = hostingEnvironment;
            FileRepository = fileRepository;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFile(int Id)
        {
            return Json(await FileRepository.GetFile(Id));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFile()
        {
            return Json(await FileRepository.GetAllFile());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] File file)
        {
            if (file != null)
            {
                if (file.Id > 0)
                {
                    return Json(await FileRepository.Update(file));
                }
                else
                {
                    return Json(await FileRepository.Add(file));
                }
            } else
            {
                return new EmptyResult();
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            return Json(await FileRepository.Delete(Id));
        }

        [Authorize]
        [HttpGet]
        [Obsolete]
        public async Task<IActionResult> Download([FromQuery] int Id)
        {
            var result = await FileRepository.GetFile(Id);

            if (result != null)
            {
                string path = HostingEnvironment.ContentRootPath;
                string fileName = System.IO.Path.GetFileName(result.Url);
                string fullPath = System.IO.Path.Combine(path, @"Upload", fileName);

                var memory = new System.IO.MemoryStream();
                using (var stream = new System.IO.FileStream(fullPath, System.IO.FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, result.ContentType, result.Url);
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpGet]
        [Obsolete]
        public async Task<IActionResult> Upload([FromForm(Name = "file")] IFormFile file)
        {
            if (file != null)
            {
                using var fileStream = new System.IO.FileStream(System.IO.Path.Combine(HostingEnvironment.ContentRootPath, @"Upload", file.FileName), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                await file.CopyToAsync(fileStream);
            }

            return new EmptyResult();
        }
    }
}
