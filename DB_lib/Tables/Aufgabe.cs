using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Aufgaben")]
    public class Aufgabe
    {
        [Key]
        public int Aufgabe_Id { get; set; }
        [Required]
        public bool Pflichtaufgabe { get; set; }
        public Aufgabe TeilaufgabeVon { get; set; }
        public string AufgabeBezirk { get; set; }
        public string AufgabeOrt { get; set; }

        //Foreign Keys
        [Required]
        public virtual Station Station { get; set; }
        [Required]
        public virtual Stufe Stufe { get; set; }
        public virtual Hintergrundbild Hintergrundbild { get; set; }

        [Required]
        public virtual Frage Frage { get; set; }
        [Required]
        public virtual Zusatzinfo Zusatzinfo { get; set; }
        [Required]
        public virtual Antwort Antwort { get; set; }

    }
}
