using DB_lib;
using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class AntwortRadioButtons_Model
    {
        public List<Radiobutton> RadioButtons { get; set; }
        public Radiobutton RadioButton_rightVal { get; set; }
    }
}
