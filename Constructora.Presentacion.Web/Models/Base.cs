using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Constructora.Presentacion.Web.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Bit")]
        public bool Remove { get; set; }
    }
}
