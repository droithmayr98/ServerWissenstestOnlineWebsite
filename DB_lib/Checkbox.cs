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
    
    public partial class Checkbox
    {
        public int CheckboxID { get; set; }
        public string Inhalt { get; set; }
        public bool CheckBoxVal { get; set; }
        public int FkAntwortCheckbox { get; set; }
    
        public virtual Antwort_checkbox Antwort_checkbox { get; set; }
    }
}
