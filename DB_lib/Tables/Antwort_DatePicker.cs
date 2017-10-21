using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("Antwort_DatePickers")]
    public class Antwort_DatePicker
    {
        [Key]
        public int Inhalt_Id { get; set; }
        [Required]
        public DateTime Date { get; set; } 
    }
}
