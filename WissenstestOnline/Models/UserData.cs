using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class UserData
    {
        public string Bezirk { get; set; }
        public string Ort{ get; set; }
        public string Stufe { get; set; }
        public string Mode { get; set; }
        public List<string> Stations { get; set; }

    }
}
