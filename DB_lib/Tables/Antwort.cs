using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Antworten")]
    public class Antwort
    {
        [Key]
        public int Antwort_Id { get; set; }
        [Required]
        public string Antwort_Name { get; set; }
        [Required]
        public virtual Typendefinition Typ { get; set; }
        [Required]
        public int Inhalt_Id { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
