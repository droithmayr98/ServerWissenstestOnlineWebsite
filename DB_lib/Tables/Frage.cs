using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    public class Frage
    {
        public int Frage_Id { get; set; }
        public string Fragetext { get; set; }
        public string Fragebild { get; set; }
        public string Fragevideo { get; set; }
        public virtual Typendefinition Typ { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
