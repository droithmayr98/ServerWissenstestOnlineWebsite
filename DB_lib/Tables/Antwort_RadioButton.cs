using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("Antwort_RadioButtons")]
    public class Antwort_RadioButton
    {
        [Key]
        public int Inhalt_Id { get; set; }
        [Required]
        public int Anzahl { get; set; }

        public virtual List<RadioButton> RadioButtons { get; set; }
    }
}
