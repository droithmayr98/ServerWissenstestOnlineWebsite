using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    public class Stufe
    {
        public int Stufe_Id { get; set; }
        public string Stufenname { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }

    }
}
