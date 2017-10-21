using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Stufen")]
    public class Stufe
    {
        [Key]
        public int Stufe_Id { get; set; }
        public string Stufenname { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }

    }
}
