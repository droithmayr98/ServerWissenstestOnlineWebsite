using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class Start_Model
    {
        public List<SelectListItem> BezikeList { get; set; }

        public List<SelectListItem> StandorteList { get; set; }
    }
}
