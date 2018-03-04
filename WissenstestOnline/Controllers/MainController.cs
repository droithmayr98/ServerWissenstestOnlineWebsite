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
        //private readonly TestDB_Context test_db;
        private readonly WissenstestDBEntities main_db;
        private ILogger<MainController> logger;

        public MainController(/*TestDB_Context db,*/ ILogger<MainController> logger, WissenstestDBEntities main_db)
        {
            //Testdatenbankinitialisierung --> wenn Datenbank im Hauptordner nicht vorhanden
            // --> auskommentieren und einmal ausführen
            //var migration = new MigrateDatabaseToLatestVersion<TestDB_Context, Configuration>();
            //Database.SetInitializer(migration);

            //this.test_db = db;
            this.main_db = main_db;
            this.logger = logger;

        }

        //Wissenstest StartPage --> Bezirk, Feuerwehr, Stufe
        public IActionResult Start()
        {
            //Datenbankobjekte brauchen Zeit zum initialisieren
            //System.Threading.Thread.Sleep(3000);

            //DB-ConnectionTest + TestLog
            //var test_bezirke_count = test_db.Bezirk.Count();
            //logger.LogInformation($"DB Bezirke: {test_bezirke_count}");

            //Main-DB-ConetionTest
            var main_test_bezirke_count = main_db.Bezirk.Count();
            logger.LogInformation($"DB Main Bezirke: {main_test_bezirke_count}");

            //Bezirksnamen in SelectListItems umwandeln --> Value: Bezirksname  --> eventuell auf BezirksID umändern
            List<DB_lib.Bezirk> bezirke = main_db.Bezirk.OrderBy(x => x.BezirkName).ToList();
            List<SelectListItem> bezirkeList = new List<SelectListItem>();
            foreach (DB_lib.Bezirk b in bezirke) {
                SelectListItem bezirkItem = new SelectListItem { Text = b.BezirkName, Value = b.BezirkName };
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
            List<DB_lib.Station> stations = main_db.Station.OrderBy(x => x.StationID).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            foreach (DB_lib.Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.StationID.ToString() };
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
            List<Standort> alle_ff_vom_bezirk = main_db.Standort.Where(x => x.Bezirk.BezirkName.Equals(bezirk)).Select(x => x).ToList();
            //FF Eingabe überprüfen
            if (Regex.IsMatch(ort, @"^[A-ZÄÖÜ][a-zA-ZÄÖÜäöü\-\. ]+$"))
            {
                //ist Ort vorhanden?
                foreach (Standort o in alle_ff_vom_bezirk)
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

            for (int i = 0; i < stations.Count; i++)
            {
                int selected_stationId = Convert.ToInt32(stations[i]);
                //Aufgaben selektieren grob
                List<DB_lib.Aufgabe> stationsteil = main_db.Aufgabe
                    .Where(x => x.Station.StationID == selected_stationId)
                    .ToList();
                //Aufgaben selektieren genau --> verschiedenen Faktoren beachten
                List<DB_lib.Aufgabe> stationsteil_genau = main_db.Aufgabe
                    .Where(x => x.Station.StationID == selected_stationId && x.Stufe.Stufenname == UserData.Stufe)
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
                DB_lib.Aufgabe nexteAufgabe = UserData.Aufgaben[UserData.AufgabeNr];
                UserData.AktuelleStation = nexteAufgabe.Station.Stationsname;
                aktuellerTyp = UserData.Aufgaben[UserData.AufgabeNr].Antwort.Typendefinition.TypName;
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
            DB_lib.Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string fragetyp = aufgabe.Frage.Typendefinition.TypName;
            string fragetext = aufgabe.Frage.FrageText;

            //Aufruf der PartialView, je nach Fragetyp
            if (fragetyp.Equals("F_T"))
            {
                var frageText_model = new FrageText_Model();
                frageText_model.FrageText = fragetext;
                return PartialView("PartialViews/LoadFrageText", frageText_model);
            }
            else if (fragetyp.Equals("F_T+B")){
                //gehört noch gemacht - Bilder
            }
            else if (fragetyp.Equals("F_T+V")){
                //gehört noch gemacht - Videos
            }

            //darf nicht passieren
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
            DB_lib.Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string antwort_typ = aufgabe.Antwort.Typendefinition.TypName;
            int inhalt_id = aufgabe.Antwort.AntwortContentID;

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
            UserData.Aufgaben = new List<DB_lib.Aufgabe>();
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
            DB_lib.Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string info_typ = aufgabe.Zusatzinfo.Typendefinition.TypName;

            //Typtrennung für weitere Vorgehensweise
            string[] splitInfo_ = info_typ.Split('_');

            //Je nach Typ, wird eine spezielle Partialview aufgerufen und die benötigten Daten geholt
            if (splitInfo_[1].Contains('+'))
            {
                //gehört noch gemacht - für Bilder und Videos
            }
            else
            {
                if (splitInfo_[1].Contains('T'))
                {
                    int zusatzinfo_id = aufgabe.Zusatzinfo.ZusatzinfoID;
                    List<Infocontent> infoContent = main_db.Infocontent.Where(x => x.Zusatzinfo.ZusatzinfoID == zusatzinfo_id).Select(x => x).ToList();
                    var zusatzInfoTextOnly_Model = new ZusatzInfoTextOnly_Model();
                    foreach (Infocontent ic in infoContent)
                    {
                        var zit = new ZusatzInfoTextblock { Heading = ic.Heading, Text = ic.InfoContent1 };
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
            DB_lib.Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string antwort_typ = aufgabe.Antwort.Typendefinition.TypName;
            int inhalt_id = aufgabe.Antwort.AntwortContentID;

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
            Antwort_text antwortTextObject = main_db.Antwort_text.Single(x => x.AntwortContentID == inhalt_id);
            string antwortText = antwortTextObject.Text;
            var antwortText_model = new AntwortText_Model();
            antwortText_model.Antwort_text = antwortText;
            return antwortText_model;
        }

        public AntwortSlider_Model FillSliderModel(int inhalt_id)
        {
            Antwort_slider antwortSliderObject = main_db.Antwort_slider.Single(x => x.AntwortContentID == inhalt_id);
            var antwortSlider_Model = new AntwortSlider_Model();
            antwortSlider_Model.Min = antwortSliderObject.MinVal;
            antwortSlider_Model.Max = antwortSliderObject.MaxVal;
            antwortSlider_Model.RightVal = antwortSliderObject.ErwartungsWert;
            antwortSlider_Model.Sprungweite = antwortSliderObject.Sprungweite;
            antwortSlider_Model.Slider_Text = antwortSliderObject.SliderText;
            return antwortSlider_Model;
        }

        public AntwortDatePicker_Model FillDatePickerModel(int inhalt_id)
        {
            Antwort_datepicker antwortDatePickerObject = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == inhalt_id);
            var antwortDatePicker_Model = new AntwortDatePicker_Model();
            antwortDatePicker_Model.Datum = antwortDatePickerObject.Date;
            return antwortDatePicker_Model;
        }

        public AntwortCheckBox_Model FillCheckBoxTextModel(int inhalt_id)
        {
            Antwort_checkbox antwortCheckBoxObject = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == inhalt_id);
            var antwortCheckBox_Model = new AntwortCheckBox_Model();
            antwortCheckBox_Model.CheckBoxen = main_db.Checkbox.Where(x => x.Antwort_checkbox.AntwortContentID == antwortCheckBoxObject.AntwortContentID).ToList();
            antwortCheckBox_Model.CheckBoxen_RightVal = main_db.Checkbox.Where(x => x.Antwort_checkbox.AntwortContentID == antwortCheckBoxObject.AntwortContentID && x.CheckBoxVal == true).ToList();
            return antwortCheckBox_Model;
        }

        public AntwortRadioButtons_Model FillRadioButtonsTextModel(int inhalt_id)
        {
            Antwort_radiobutton antwortRadioButtonObject = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == inhalt_id);
            var antwortRadioButtons_Model = new AntwortRadioButtons_Model();
            antwortRadioButtons_Model.RadioButtons = main_db.Radiobutton.Where(x => x.Antwort_radiobutton.AntwortContentID == antwortRadioButtonObject.AntwortContentID).ToList();
            Radiobutton rb_rightVal = main_db.Radiobutton.Where(x => x.Antwort_radiobutton.AntwortContentID == antwortRadioButtonObject.AntwortContentID).Single(x => x.ErwartungsWert);
            antwortRadioButtons_Model.RadioButton_rightVal = rb_rightVal;
            return antwortRadioButtons_Model;
        }



        //Überprüfen der Antwort im Übungsmodus
        public bool CheckAntwortText(string texteingabe)
        {
            int id_antwort = UserData.Aufgabe.Antwort.AntwortContentID;
            
            Antwort_text antwortTextObject = main_db.Antwort_text.Single(x => x.AntwortContentID == id_antwort);
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
            int id_antwort = UserData.Aufgabe.Antwort.AntwortContentID;
            Antwort_slider antwortSliderObject = main_db.Antwort_slider.Single(x => x.AntwortContentID == id_antwort);
            if (antwortSliderObject.ErwartungsWert.ToString().Equals(slidervalue))
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
            int id_antwort = UserData.Aufgabe.Antwort.AntwortContentID;
            Antwort_radiobutton antwortRadioButtonObject = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == id_antwort);
            Radiobutton rb = main_db.Radiobutton.Where(x => x.Antwort_radiobutton.AntwortContentID == antwortRadioButtonObject.AntwortContentID).Single(x => x.ErwartungsWert);
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

            int id_antwort = UserData.Aufgabe.Antwort.AntwortContentID;
            Antwort_checkbox antwortCheckBoxObject = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == id_antwort);
            List<Checkbox> cb_List = main_db.Checkbox.Where(x => x.Antwort_checkbox.AntwortContentID == antwortCheckBoxObject.AntwortContentID).ToList();

            for (int i = 0; i < cbValue.Length; i++)
            {
                if (cbValue[i] == null)
                {
                    Checkbox cb_NOTchecked = cb_List[i];
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
                    Checkbox cb_checked = cb_List[i];
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
            int id_antwort = UserData.Aufgabe.Antwort.AntwortContentID;
            Antwort_datepicker antwortDatePickerObject = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == id_antwort);
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