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
        public DB_lib.Antwort_text Antwort_Text { get; set; }
        public DB_lib.Antwort_datepicker Antwort_DatePicker { get; set; }
        public DB_lib.Antwort_slider Antwort_Slider { get; set; }
        public List<DB_lib.Checkbox> Antwort_CheckBoxes { get; set; }
        public List<DB_lib.Radiobutton> Antwort_RadioButtons { get; set; }
    }
}
