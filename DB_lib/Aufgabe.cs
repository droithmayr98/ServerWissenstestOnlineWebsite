//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB_lib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Aufgabe
    {
        public int AufgabeID { get; set; }
        public bool Pflichtaufgabe { get; set; }
        public int FkAntwort { get; set; }
        public int FkFrage { get; set; }
        public Nullable<int> FkHintergrundbild { get; set; }
        public int FkStation { get; set; }
        public int FkStufe { get; set; }
        public int FkZusatzinfo { get; set; }
        public Nullable<int> TeilAufgabeVon { get; set; }
        public Nullable<int> FkBezirk { get; set; }
        public Nullable<int> FkStandort { get; set; }
    
        public virtual Antwort Antwort { get; set; }
        public virtual Bezirk Bezirk { get; set; }
        public virtual Frage Frage { get; set; }
        public virtual Hintergrund Hintergrund { get; set; }
        public virtual Standort Standort { get; set; }
        public virtual Station Station { get; set; }
        public virtual Stufe Stufe { get; set; }
        public virtual Zusatzinfo Zusatzinfo { get; set; }
    }
}
