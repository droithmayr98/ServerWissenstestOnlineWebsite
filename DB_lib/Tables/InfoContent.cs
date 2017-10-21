using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("InfoContentM")]
    public class InfoContent
    {
        [Key]
        public int Inhalt_Id { get; set; }
        [Required]
        public string Info_Content { get; set; }
        [Required]
        public virtual Zusatzinfo Zusatzinfo{ get; set; }
    }
}
