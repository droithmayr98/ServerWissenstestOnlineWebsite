using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public static class AdminData
    {
        public static string Username { get; set; } = "";

        public static bool Can_create_admin { get; set; } = false;

        public static bool Can_edit_admin { get; set; } = false;

        public static bool Can_delete_admin { get; set; } = false;
    }
}
