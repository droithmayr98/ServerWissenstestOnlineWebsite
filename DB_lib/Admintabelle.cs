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
    
    public partial class Admintabelle
    {
        public int AdminID { get; set; }
        public string Benutzer { get; set; }
        public string Passwort { get; set; }
        public bool CanCreateAcc { get; set; }
        public bool CanDeleteAcc { get; set; }
        public bool CanEditAcc { get; set; }
    }
}