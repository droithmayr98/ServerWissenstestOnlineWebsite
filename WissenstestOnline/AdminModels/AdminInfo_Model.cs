using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class AdminInfo_Model
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Can_create_acc { get; set; }
        public bool Can_edit_acc { get; set; }
        public bool Can_delete_acc { get; set; }
    }
}
