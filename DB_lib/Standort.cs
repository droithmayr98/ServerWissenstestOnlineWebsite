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
    
    public partial class Standort
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Standort()
        {
            this.Aufgabe = new HashSet<Aufgabe>();
        }
    
        public int StandortID { get; set; }
        public string Ortsname { get; set; }
        public int FkBezirk { get; set; }
    
        public virtual Bezirk Bezirk { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aufgabe> Aufgabe { get; set; }
    }
}