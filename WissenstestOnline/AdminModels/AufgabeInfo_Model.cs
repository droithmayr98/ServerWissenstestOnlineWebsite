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
        public int Antwort_Id { get; set; }
        public string Antwort_Name { get; set; }

        //Frage
        public DB_lib.Frage Frage { get; set; }

        public List<DB_lib.Infocontent> Info { get; set; }

        //Antwort
        public string Antworttyp { get; set; }
        public DB_lib.Antwort_text Antwort_Text { get; set; }
        public DB_lib.Antwort_datepicker Antwort_DatePicker { get; set; }
        public DB_lib.Antwort_slider Antwort_Slider { get; set; }
        public List<DB_lib.Checkbox> Antwort_CheckBoxes { get; set; }
        public List<DB_lib.Radiobutton> Antwort_RadioButtons { get; set; }

        //Zusatzinfo
        public DB_lib.Zusatzinfo Zusatzinfo { get; set; }




    }
}
