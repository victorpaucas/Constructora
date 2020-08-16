using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Constructora.Presentacion.Web.Models
{
    public class User : Base
    {
        [Column(TypeName = "Int")]
        public int UserTypeId { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
