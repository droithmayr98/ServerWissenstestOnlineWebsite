using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Bezirke")]
    public class Bezirk
    {
        [Key]
        public int Bezirk_Id { get; set; }
        public string Bezirksname { get; set; }
        public virtual List<Ort> Orte { get; set; }
    }
}
