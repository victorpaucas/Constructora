using System.ComponentModel.DataAnnotations.Schema;

namespace Constructora.Presentacion.Web.Models
{
    public class UserType : Base
    {
        [Column(TypeName = "Varchar(100)")]
        public string Name { get; set; }
    }
}
