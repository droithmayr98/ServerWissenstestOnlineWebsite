using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WissenstestOnlineWebseite.Models;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class ZusatzInfo_InfoModel
    {
        public int ZusatzInfo_Id { get; set; }
        public string ZusatzInfo_Name { get; set; }
        public string ZusatzInfo_Typ { get; set; }
        public List<ZusatzInfoTextblock> Texte { get; set; }
    }
}