using Constructora.Presentacion.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Constructora.Presentacion.Web.Dto
{
    public class UploadDto
    {
        public IFormFile formFile { get; set; }
        public File file { get; set; }
    }
}
