using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Fragen")]
    public class Frage
    {
        [Key]
        public int Frage_Id { get; set; }
        [Required]
        public string Fragetext { get; set; }
        public string Fragebild { get; set; }
        public string Fragevideo { get; set; }
        //[Required]
        public virtual Typendefinition Typ { get; set; }

        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
