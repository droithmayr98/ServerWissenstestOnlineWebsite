using DB_lib.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class AntwortEditView_Model
    {
        public List<SelectListItem> AntwortTypen { get; set; }
    }
}
