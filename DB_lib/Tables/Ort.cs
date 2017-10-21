using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Orte")]
    public class Ort
    {
        [Key]
        public int Ort_Id { get; set; }
        [Required]
        public string Ortsname { get; set; }
        [Required]
        public virtual Bezirk Bezirk {get; set;}
    }
}
