using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Constructora.Presentacion.Web.Models
{
    public class File : Base
    {
        [Column(TypeName = "Varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string Url { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string ContentType { get; set; }
    }
}
