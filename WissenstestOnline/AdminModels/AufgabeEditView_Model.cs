using DB_lib.Tables;
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
        public List<Frage> Fragen { get; set; }
        public List<Antwort> Antworten { get; set; }
        public List<Zusatzinfo> Infos { get; set; }
        public List<SelectListItem> Antwort_Typen { get; set; }
        public List<SelectListItem> Info_Typen { get; set; }

    }
}
