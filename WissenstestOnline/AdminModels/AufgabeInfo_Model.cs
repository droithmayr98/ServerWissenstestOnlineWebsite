using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.AdminModels
{
    public class AufgabeInfo_Model
    {
        public int Aufgabe_Id { get; set; }
        public string Station { get; set; }
        public string Stufe { get; set; }
        public string Bezirk { get; set; }
        public string Ort { get; set; }
        public string TeilAufgabeVon { get; set; }
        public bool IsPflichtaufgabe { get; set; }

        public Frage Frage { get; set; }
        public List<InfoContent> Info { get; set; }

        //Antwort
        public string Antworttyp { get; set; }
        public Antwort_Text Antwort_Text { get; set; }
        public Antwort_DatePicker Antwort_DatePicker { get; set; }
        public Antwort_Slider Antwort_Slider { get; set; }
        public List<CheckBox> Antwort_CheckBoxes { get; set; }
        public List<RadioButton> Antwort_RadioButtons { get; set; }
        




    }
}
