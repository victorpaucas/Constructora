using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Constructora.Presentacion.Web.Models
{
    public class Token : Base
    {
        [Column(TypeName = "Int")]
        public int UserId { get; set; }
        [Column(TypeName = "Varchar(500)")]
        public string Key { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime ExpireDate { get; set; }
        [IgnoreDataMember]
        public User User { get; set; }
    }
}
