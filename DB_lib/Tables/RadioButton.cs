using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("RadioButtons")]
    public class RadioButton
    {
        [Key]
        public int Radio_Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsTrue { get; set; }
        [Required]
        public virtual Antwort_RadioButton Antwort_RadioButton { get; set;}
    }
}
