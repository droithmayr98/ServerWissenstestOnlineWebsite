using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    public class Ort
    {
        [Key]
        public int Ort_Id { get; set; }
        public string Ortsname { get; set; }
        public virtual Bezirk Bezirk {get; set;}
    }
}
