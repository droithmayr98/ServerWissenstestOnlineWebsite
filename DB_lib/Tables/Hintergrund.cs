using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    public class Hintergrundbild
    {
        [Key]
        public int Hintergrundbild_Id { get; set; }
        public string Bild { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
