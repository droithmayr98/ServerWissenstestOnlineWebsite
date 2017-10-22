using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    [Table("Antwort_Sliders")]
    public class Antwort_Slider
    {
        [Key]
        public int Inhalt_Id { get; set; }
        [Required]
        public int Min_val { get; set; }
        [Required]
        public int Max_val { get; set; }
        [Required]
        public int Sprungweite { get; set; }
        [Required]
        public int RightVal { get; set; }
        public string Slider_text { get; set; }
    }
}
