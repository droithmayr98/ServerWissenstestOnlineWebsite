using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DB_lib;
using Microsoft.Extensions.Logging;
using DB_lib.Tables;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using WissenstestOnlineWebseite.Models;
using System.Data.Entity;
using DB_lib.Migrations;

namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {
        private readonly TestDB_Context test_db;
        private ILogger<MainController> logger;

        //Constructor, wenn UserData über Singleton in der Startup übergeben wird
        //public MainController(TestDB_Context db, ILogger<MainController> logger, UserData myClass)

        public MainController(TestDB_Context db, ILogger<MainController> logger)
        {
            //Testdatenbankinitialisierung --> wenn Datenbank im Hauptordner nicht vorhanden
            // --> auskommentieren und einmal ausführen
            //var migration = new MigrateDatabaseToLatestVersion<TestDB_Context, Configuration>();
            //Database.SetInitializer(migration);

            this.test_db = db;
            this.logger = logger;

        }

        //Wissenstest StartPage --> Bezirk, Feuerwehr, Stufe
        public IActionResult Start()
        {
            //Datenbankobjekte brauchen Zeit zum initialisieren
            System.Threading.Thread.Sleep(3000);

            //DB-ConnectionTest + TestLog
            var test_bezirke_count = test_db.Bezirke.Count();
            logger.LogInformation($"DB Bezirke: {test_bezirke_count}");

            //Bezirksnamen in SelectListItems umwandeln --> Value: Bezirksname  --> eventuell auf BezirksID umändern
            List<Bezirk> bezirke = test_db.Bezirke.OrderBy(x => x.Bezirksname).ToList();
            List<SelectListItem> bezirkeList = new List<SelectListItem>();
            foreach (Bezirk b in bezirke) {
                SelectListItem bezirkItem = new SelectListItem { Text = b.Bezirksname, Value = b.Bezirksname };
                bezirkeList.Add(bezirkItem);
            }

            //Datenübergabe an Model / Modelübergabe an View
            var start_Model = new Start_Model();
            start_Model.BezikeList = bezirkeList;
            return View(start_Model);
        }

        //Wissenstest Stationsauswahl + Modusauswahl
        public IActionResult SelectStation()
        {
            //Stationennamen in SelectListItems umwandeln --> Value: Station_Id
            List<Station> stations = test_db.Stationen.OrderBy(x => x.Station_Id).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            foreach (Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                stationsList.Add(stationItem);
            }

            //Datenübergabe an Model / Modelübergabe an View
            var selectStation_Model = new SelectStation_Model();
            selectStation_Model.StationsList = stationsList;
            return View(selectStation_Model);
        }

        public IActionResult AufgabeUmgebungLearn()
        {
            return View();
        }

        public IActionResult AufgabeUmgebungPractise()
        {
            return View();
        }

        public IActionResult ZusatzInfo()
        {
            return View();
        }

        //Aufruf nach Übungsmodus
        public IActionResult ErgebnisOverview()
        {
            //UserData Info in Model + Übergabe an View
            var ergebnis_Model = new Ergebnis_Model();
            ergebnis_Model.Punkte = UserData.PracticePoints.ToString();
            ergebnis_Model.Max_Punkte = UserData.AufgabenCount.ToString();
            int percentRight = (int)Math.Round((double)(100 * UserData.PracticePoints) / UserData.AufgabenCount);
            ergebnis_Model.ProzentRichtig = percentRight.ToString();
            return View(ergebnis_Model);
        }

        //AllgemeinInfo
        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Kontakt()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }



        //Ajax Calls
        //StartInfo überprüfen
        public string CheckUserInfo(string bezirk, string ort, string stufe){
            //alle Feuerwehren vom Bezirk holen
            List<Ort> alle_ff_vom_bezirk = test_db.Orte.Where(x => x.Bezirk.Bezirksname.Equals(bezirk)).Select(x => x).ToList();
            //FF Eingabe überprüfen
            if (Regex.IsMatch(ort, @"^[A-ZÄÖÜ][a-zA-ZÄÖÜäöü\-\. ]+$"))
            {
                //ist Ort vorhanden?
                foreach (Ort o in alle_ff_vom_bezirk)
                {
                    //Wenn ja --> Werte werden in UserData gespeichert und Kontrolle wird beendet
                    if (o.Ortsname.Equals(ort))
                    {
                        UserData.Bezirk = bezirk;
                        UserData.Ort = ort;
                        UserData.Stufe = stufe;

                        return "ok";
                    }

                }
            }
            //Wenn nein
            return "wrong";
        }

        public string GlobalStationData(List<string> stations, string mode){
            //Array to string
            string stationsString = string.Join(",", stations.ToArray());

            //Eingabewerte in UserData speichern
            UserData.Mode = mode;
            UserData.Stations = stationsString;

            //Aufgaben selektieren grob --> gehört noch genauer (Ort, Stufe, andere Faktoren...)
            for (int i = 0; i < stations.Count; i++)
            {
                int selected_stationId = Convert.ToInt32(stations[i]);
                List<Aufgabe> stationsteil = test_db.Aufgaben
                    //Include selektiert nur Wert des Foreign Keys, nicht jedoch das Oject!!!
                    //.Include(x => x.Frage).Include(x => x.Antwort).Include(x => x.Zusatzinfo).Include(x => x.Station).Include(x => x.Stufe).Include(x => x.Hintergrundbild)
                    .Where(x => x.Station.Station_Id == selected_stationId)
                    .ToList();
                //Stationsaufgaben an Gesamtliste ranhängen
                UserData.Aufgaben.AddRange(stationsteil);
                //Aktuelle Station setzen
                if (i == 0)
                {
                    UserData.AktuelleStation = stationsteil[0].Station.Stationsname;
                }
            }
            //Gesamtaufgabenanzahl in Userdata speichern
            UserData.AufgabenCount = UserData.Aufgaben.Count;

            return "ok";
        }

        //Daten für jquery Code übergeben
        public IActionResult GetGlobalData()
        {
            //Daten sammeln
            var RightAufgabeNr = UserData.AufgabeNr + 1;
            var RightAufgabenCount = UserData.AufgabenCount;
            var aktuellerTyp = "";

            if (UserData.AufgabenCount > 0)
            {
                Aufgabe nexteAufgabe = UserData.Aufgaben[UserData.AufgabeNr];
                UserData.AktuelleStation = nexteAufgabe.Station.Stationsname;
                aktuellerTyp = UserData.Aufgaben[UserData.AufgabeNr].Antwort.Typ.Typ;
                UserData.Aufgabe = nexteAufgabe;
            }

            //Daten übergeben
            var data = new Dictionary<string, string>()
            {
                ["bezirk"] = UserData.Bezirk,
                ["ort"] = UserData.Ort,
                ["stufe"] = UserData.Stufe,
                ["mode"] = UserData.Mode,
                ["stations"] = UserData.Stations,
                ["aufgabenNr"] = RightAufgabeNr.ToString(),
                ["aufgabenCount"] = RightAufgabenCount.ToString(),
                ["aktuelleStation"] = UserData.AktuelleStation,
                ["antwortTypPractice"] = aktuellerTyp
            };
            return Json(data);
        }

        //Frage wird in die View geladen
        public IActionResult LoadFrage(string aufgabenNr){
            //AufgabeNr -1, wegen Array
            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;

            //Holen der benötigten Daten
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string fragetyp = aufgabe.Frage.Typ.Typ;
            string fragetext = aufgabe.Frage.Fragetext;

            //Aufruf der PartialView, je nach Fragetyp
            if (fragetyp.Equals("F_T"))
            {
                var frageText_model = new FrageText_Model();
                frageText_model.FrageText = fragetext;
                return PartialView("PartialViews/LoadFrageText", frageText_model);
            }
            else if (fragetyp.Equals("F_T+B")){
                //gehört noch gemacht
            }
            else if (fragetyp.Equals("F_T+V")){
                //gehört noch gemacht
            }

            //darf nicht passieren
            //gehört noch gemacht
            return null;
        }

        public string PressedButtonLearn(string pressedButtonLearn){
            //in UserData abspeichern
            UserData.pressedButtonLearn = pressedButtonLearn;

            //welcher Button wurde gedrückt?? --> AufgabeNr erhöhen oder senken
            if (UserData.pressedButtonLearn.Equals("zurueck"))
            {
                UserData.AufgabeNr = UserData.AufgabeNr - 1;
            }
            else if (UserData.pressedButtonLearn.Equals("weiter"))
            {
                UserData.AufgabeNr = UserData.AufgabeNr + 1;
            }

            //ArrayOutOfBounds verhindern
            if (UserData.AufgabeNr == -1 || UserData.AufgabeNr == UserData.AufgabenCount)
            {
                if (UserData.pressedButtonLearn.Equals("zurueck"))
                {
                    UserData.AufgabeNr = UserData.AufgabeNr + 1;
                }
                else if (UserData.pressedButtonLearn.Equals("weiter"))
                {
                    UserData.AufgabeNr = UserData.AufgabeNr - 1;
                }
                //User steht Am Anfang oder Ende --> Alert zur Bekanntmachung
                return "ok";
            }
            else
            {
                return "ok";
            }

        }

        public IActionResult LoadAntwortLearn(string aufgabenNr){
            //AufgabeNr -1, wegen Array
            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;

            //Holen der benötigten Daten
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string antwort_typ = aufgabe.Antwort.Typ.Typ;
            int inhalt_id = aufgabe.Antwort.Inhalt_Id;

            //Aufruf der PartialView, je nach Fragetyp
            //Füllen des dazugehörigen Models
            switch (antwort_typ)
            {
                case "A_T":
                    var antwortText_model = FillTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortText", antwortText_model);
                case "A_S":
                    var antwortSlider_Model = FillSliderModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortSlider", antwortSlider_Model);
                case "A_DP":
                    var antwortDatePicker_Model = FillDatePickerModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortDatePicker", antwortDatePicker_Model);
                case "A_CB:T":
                    var antwortCheckBox_Model = FillCheckBoxTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortCheckBox", antwortCheckBox_Model);
                case "A_RB:T":
                    var antwortRadioButtons_Model = FillRadioButtonsTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortRadioButtons", antwortRadioButtons_Model);
                default:
                    return PartialView();
            }
        }

        public string CancelAufgabe(){
            //Werte werden auf Ausgangswert im UserData gesetzt
            UserData.Aufgaben = new List<Aufgabe>();
            UserData.AufgabeNr = 0;
            UserData.AufgabenCount = 0;
            UserData.lastPracticeAufgabeCorrect = false;
            UserData.PracticePoints = 0;
            return "ok";
        }

        public IActionResult LoadZusatzinfo()
        {
            //Benötigte Informationen holen
            int aufgabenNr_Int = Convert.ToInt32(UserData.AufgabeNr);
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string info_typ = aufgabe.Zusatzinfo.Typ.Typ;

            //Typtrennung für weitere Vorgehensweise
            string[] splitInfo_ = info_typ.Split('_');

            //Je nach Typ, wird eine spezielle Partialview aufgerufen und die benötigten Daten geholt
            if (splitInfo_[1].Contains('+'))
            {
                //gehört noch gemacht
            }
            else
            {
                if (splitInfo_[1].Contains('T'))
                {
                    int zusatzinfo_id = aufgabe.Zusatzinfo.Zusatzinfo_Id;
                    List<InfoContent> infoContent = test_db.InfoContentM.Where(x => x.Zusatzinfo.Zusatzinfo_Id == zusatzinfo_id).Select(x => x).ToList();
                    var zusatzInfoTextOnly_Model = new ZusatzInfoTextOnly_Model();
                    foreach (InfoContent ic in infoContent)
                    {
                        var zit = new ZusatzInfoTextblock { Heading = ic.Heading, Text = ic.Info_Content };
                        zusatzInfoTextOnly_Model.Texte.Add(zit);
                    }
                    return PartialView("PartialViews/LoadZusatzinfoTextOnly", zusatzInfoTextOnly_Model);

                }
            }

            return PartialView();
        }


        public string PressedButtonPractise(string buttonActionPractice){
            //Bei "Weiter" wird UserData erhöht
            if (buttonActionPractice.Equals("Weiter"))
            {
                UserData.AufgabeNr = UserData.AufgabeNr + 1;
                if (UserData.lastPracticeAufgabeCorrect) {
                    UserData.PracticePoints++;
                    UserData.lastPracticeAufgabeCorrect = false;
                }
                //ArrayOutOfBounds vermeiden
                if (UserData.AufgabeNr == UserData.AufgabenCount)
                {
                    UserData.AufgabeNr = UserData.AufgabeNr - 1;
                    return "Weiter";
                }
                else
                {
                    return "Weiter";
                }

            }
            //Bei "Zurück" wird UserData gesenkt
            else if (buttonActionPractice.Equals("zurueck")) {
                UserData.AufgabeNr = UserData.AufgabeNr - 1;
                if (UserData.AufgabeNr < 0)
                {
                    UserData.AufgabeNr = UserData.AufgabeNr + 1;
                    return "zurueck";
                }
                else
                {
                    return "zurueck";
                }
            }
            //andere Rückmeldung
            else if (buttonActionPractice.Equals("Auswertung"))
            {
                return "Auswertung";
            }
            else
            {
                return "Check";
            }



        }
        public IActionResult LoadAntwortPractise(string aufgabenNr){
            //AufgabeNr -1, wegen Array
            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;

            //Holen der benötigten Daten
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string antwort_typ = aufgabe.Antwort.Typ.Typ;
            int inhalt_id = aufgabe.Antwort.Inhalt_Id;

            //Aufruf der PartialView, je nach Fragetyp
            //Füllen des dazugehörigen Models
            switch (antwort_typ)
            {
                case "A_T":
                    var antwortText_model = FillTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortTextPractice", antwortText_model);
                case "A_S":
                    var antwortSlider_Model = FillSliderModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortSliderPractice", antwortSlider_Model);
                case "A_DP":
                    var antwortDatePicker_Model = FillDatePickerModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortDatePickerPractice", antwortDatePicker_Model);
                case "A_CB:T":
                    var antwortCheckBox_Model = FillCheckBoxTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortCheckBoxPractice", antwortCheckBox_Model);
                case "A_RB:T":
                    var antwortRadioButtons_Model = FillRadioButtonsTextModel(inhalt_id);
                    return PartialView("PartialViews/LoadAntwortRadioButtonsPractice", antwortRadioButtons_Model);
                default:
                    return PartialView();
            }

        }




        //ModelFüllMethoden
        public AntwortText_Model FillTextModel(int inhalt_id)
        {
            Antwort_Text antwortTextObject = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == inhalt_id);
            string antwortText = antwortTextObject.Text;
            var antwortText_model = new AntwortText_Model();
            antwortText_model.Antwort_text = antwortText;
            return antwortText_model;
        }

        public AntwortSlider_Model FillSliderModel(int inhalt_id)
        {
            Antwort_Slider antwortSliderObject = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == inhalt_id);
            var antwortSlider_Model = new AntwortSlider_Model();
            antwortSlider_Model.Min = antwortSliderObject.Min_val;
            antwortSlider_Model.Max = antwortSliderObject.Max_val;
            antwortSlider_Model.RightVal = antwortSliderObject.RightVal;
            antwortSlider_Model.Sprungweite = antwortSliderObject.Sprungweite;
            antwortSlider_Model.Slider_Text = antwortSliderObject.Slider_text;
            return antwortSlider_Model;
        }

        public AntwortDatePicker_Model FillDatePickerModel(int inhalt_id)
        {
            Antwort_DatePicker antwortDatePickerObject = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == inhalt_id);
            var antwortDatePicker_Model = new AntwortDatePicker_Model();
            antwortDatePicker_Model.Datum = antwortDatePickerObject.Date;
            return antwortDatePicker_Model;
        }

        public AntwortCheckBox_Model FillCheckBoxTextModel(int inhalt_id)
        {
            Antwort_CheckBox antwortCheckBoxObject = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == inhalt_id);
            var antwortCheckBox_Model = new AntwortCheckBox_Model();
            antwortCheckBox_Model.CheckBoxen = test_db.CheckBoxes.Where(x => x.Antwort_CheckBox.Inhalt_Id == antwortCheckBoxObject.Inhalt_Id).ToList();
            antwortCheckBox_Model.CheckBoxen_RightVal = test_db.CheckBoxes.Where(x => x.Antwort_CheckBox.Inhalt_Id == antwortCheckBoxObject.Inhalt_Id && x.CheckBoxVal == true).ToList();
            return antwortCheckBox_Model;
        }

        public AntwortRadioButtons_Model FillRadioButtonsTextModel(int inhalt_id)
        {
            Antwort_RadioButton antwortRadioButtonObject = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == inhalt_id);
            var antwortRadioButtons_Model = new AntwortRadioButtons_Model();
            antwortRadioButtons_Model.RadioButtons = test_db.RadioButtons.Where(x => x.Antwort_RadioButton.Inhalt_Id == antwortRadioButtonObject.Inhalt_Id).ToList();
            RadioButton rb_rightVal = test_db.RadioButtons.Where(x => x.Antwort_RadioButton.Inhalt_Id == antwortRadioButtonObject.Inhalt_Id).Single(x => x.IsTrue);
            antwortRadioButtons_Model.RadioButton_rightVal = rb_rightVal;
            return antwortRadioButtons_Model;
        }



        //Überprüfen der Antwort im Übungsmodus
        public bool CheckAntwortText(string texteingabe)
        {
            int id_antwort = UserData.Aufgabe.Antwort.Inhalt_Id;
            
            Antwort_Text antwortTextObject = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == id_antwort);
            if (texteingabe == null)
            {


                return false;
            }
            else if (antwortTextObject.Text.ToUpper().Equals(texteingabe.ToUpper()))
            {
                UserData.lastPracticeAufgabeCorrect = true;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckAntwortSlider(string slidervalue)
        {
            int id_antwort = UserData.Aufgabe.Antwort.Inhalt_Id;
            Antwort_Slider antwortSliderObject = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == id_antwort);
            if (antwortSliderObject.RightVal.ToString().Equals(slidervalue))
            {
                UserData.lastPracticeAufgabeCorrect = true;
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool CheckAntwortRadioButtons(string rbValue)
        {
            int id_antwort = UserData.Aufgabe.Antwort.Inhalt_Id;
            Antwort_RadioButton antwortRadioButtonObject = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == id_antwort);
            RadioButton rb = test_db.RadioButtons.Where(x => x.Antwort_RadioButton.Inhalt_Id == antwortRadioButtonObject.Inhalt_Id).Single(x => x.IsTrue);
            if (rb.Content.Equals(rbValue))
            {
                UserData.lastPracticeAufgabeCorrect = true;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckAntwortCheckBoxes(string[] cbValue)
        {
            bool erg = false;

            int id_antwort = UserData.Aufgabe.Antwort.Inhalt_Id;
            Antwort_CheckBox antwortCheckBoxObject = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == id_antwort);
            List<CheckBox> cb_List = test_db.CheckBoxes.Where(x => x.Antwort_CheckBox.Inhalt_Id == antwortCheckBoxObject.Inhalt_Id).ToList();

            for (int i = 0; i < cbValue.Length; i++)
            {
                if (cbValue[i] == null)
                {
                    CheckBox cb_NOTchecked = cb_List[i];
                    if (!cb_NOTchecked.CheckBoxVal)
                    {
                        erg = true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    CheckBox cb_checked = cb_List[i];
                    if (cb_checked.CheckBoxVal)
                    {
                        erg = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            UserData.lastPracticeAufgabeCorrect = true;
            return erg;
        }

        public bool CheckAntwortDate(DateTime date)
        {
            int id_antwort = UserData.Aufgabe.Antwort.Inhalt_Id;
            Antwort_DatePicker antwortDatePickerObject = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == id_antwort);
            if (antwortDatePickerObject.Date.Equals(date))
            {
                UserData.lastPracticeAufgabeCorrect = true;
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}