using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    public class Typendefinition
    {
        [Key]
        public int Typ_Id { get; set; }
        public string Typ { get; set; }
        public virtual List<Zusatzinfo> Zusatzinfos { get; set; }
        public virtual List<Frage> Fragen { get; set; }
        public virtual List<Antwort> Antworten { get; set; }
    }
}
