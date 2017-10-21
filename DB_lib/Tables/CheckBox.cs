using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib.Tables
{
    [Table("CheckBoxes")]
    public class CheckBox
    {
        [Key]
        public int CheckBox_Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool CheckBoxVal { get; set; }
        [Required]
        public virtual Antwort_CheckBox Antwort_CheckBox { get; set; }



    }
}
