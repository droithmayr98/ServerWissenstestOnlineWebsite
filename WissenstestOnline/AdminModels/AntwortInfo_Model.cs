using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class AntwortInfo_Model
    {
        public int Antwort_Id { get; set; }
        public string Antwortname { get; set; }
        public string Antworttyp { get; set; }
        public Antwort_Text Antwort_Text { get; set; }
        public Antwort_DatePicker Antwort_DatePicker { get; set; }
        public Antwort_Slider Antwort_Slider { get; set; }
        public List<CheckBox> Antwort_CheckBoxes { get; set; }
        public List<RadioButton> Antwort_RadioButtons { get; set; }
    }
}
