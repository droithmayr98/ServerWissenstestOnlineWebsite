using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class AufgabeEditView_Model
    {
        public List<SelectListItem> Stationen { get; set; }
        public List<SelectListItem> Bezirke { get; set; }
        public List<SelectListItem> Standorte { get; set; }
        public List<SelectListItem> Fragen { get; set; }

    }
}
