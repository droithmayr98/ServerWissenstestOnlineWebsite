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
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Can_create_acc { get; set; }
        public bool Can_delete_acc { get; set; }
        public bool Can_edit_acc { get; set; }

    }
}
