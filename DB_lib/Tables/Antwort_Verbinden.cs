using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("Antwort_VerbindenM")]
    public class Antwort_Verbinden
    {
        [Key]
        public int Inhalt_id { get; set; }
        [Required]
        public int Anzahl { get; set; }

        public virtual List<Paar> Paare { get; set; }
    }
}
