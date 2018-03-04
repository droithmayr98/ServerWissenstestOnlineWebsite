using DB_lib;
using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class AntwortCheckBox_Model
    {
        public List<Checkbox> CheckBoxen { get; set; }
        public List<Checkbox> CheckBoxen_RightVal { get; set; }
    }
}
