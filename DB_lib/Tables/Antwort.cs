using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    public class Antwort
    {
        public int Antwort_Id { get; set; }
        public virtual Typendefinition Typ { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
