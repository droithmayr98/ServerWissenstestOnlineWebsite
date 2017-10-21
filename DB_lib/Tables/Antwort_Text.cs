using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    [Table("Antwort_Texte")]
    public class Antwort_Text
    {
        [Key]
        public int Inhalt_Id {get; set;}
        [Required]
        public string Text { get; set; }
    }
}
