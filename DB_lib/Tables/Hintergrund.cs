using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Hintergrundbilder")]
    public class Hintergrundbild
    {
        [Key]
        public int Hintergrundbild_Id { get; set; }
        [Required]
        public string Bild { get; set; }

        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
