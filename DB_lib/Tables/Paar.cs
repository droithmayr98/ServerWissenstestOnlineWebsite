using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("Paare")]
    public class Paar
    {
        [Key]
        public int Paar_Id { get; set; }
        [Required]
        public string Teil1 { get; set; }
        [Required]
        public string Teil2 { get; set; }
        [Required]
        public virtual Antwort_Verbinden Antwort_Verbinden { get; set; }
    }
}
