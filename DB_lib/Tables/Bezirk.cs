using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    public class Bezirk
    {
        [Key]
        public int Bezirk_Id { get; set; }
        public string Bezirksname { get; set; }
        public virtual List<Ort> Orte { get; set; }
    }
}
