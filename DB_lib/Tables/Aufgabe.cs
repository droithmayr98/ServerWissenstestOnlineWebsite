using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    public class Aufgabe
    {
        public int Aufgabe_Id { get; set; }
        public bool Pflichtaufgabe { get; set; }
        public int TeilaufgabeVon { get; set; }
        public string AufgabeBezirk { get; set; }
        public string AufgabeOrt { get; set; }

        //Foreign Keys
        public virtual Station Station { get; set; }
        public virtual Stufe Stufe { get; set; }
        public virtual Hintergrundbild Hintergrundbild { get; set; }

        public virtual Frage Frage { get; set; }
        public virtual Zusatzinfo Zusatzinfo { get; set; }
        public virtual Antwort Antwort { get; set; }

    }
}
