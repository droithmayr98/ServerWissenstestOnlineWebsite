using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Admins")]
    public class Admintable
    {
        [Key]
        public int Admin_Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Can_create_acc { get; set; }
        [Required]
        public bool Can_delete_acc { get; set; }
        [Required]
        public bool Can_edit_acc { get; set; }

    }
}
