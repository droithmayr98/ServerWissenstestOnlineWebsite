using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    public class Hintergrundbild
    {
        public int Hintergrundbild_Id { get; set; }
        public string Bild { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
