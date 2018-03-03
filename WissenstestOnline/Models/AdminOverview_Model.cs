using DB_lib.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class AdminOverview_Model
    {
        public List<Aufgabe> Aufgaben { get; set; }
        public List<SelectListItem> Stationen { get; set; }
        public List<Admintable> Admins { get; set; }

        public bool Can_create_admin { get; set; }

        public bool Can_edit_admin { get; set; }

        public bool Can_delete_admin { get; set; }

        public string Username { get; set; }

    }
}
