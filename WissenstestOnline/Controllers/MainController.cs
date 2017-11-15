using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_lib;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.Internal;
using DB_lib.Tables;
using DB_lib.Migrations;
using System.Data.Entity;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using WissenstestOnlineWebseite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Data.Entity;


namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {

        private readonly TestDB_Context test_db;
        private ILogger<MainController> logger;

        //public MainController(TestDB_Context db, ILogger<MainController> logger, MyImplTest myClass)

        public MainController(TestDB_Context db, ILogger<MainController> logger)
        {
            //var migration = new MigrateDatabaseToLatestVersion<TestDB_Context, Configuration>();
            //Database.SetInitializer(migration);
            this.test_db = db;
            this.logger = logger;

        }

        public IActionResult Start()
        {

            //DB_ConnectionTest
            var test = test_db.Bezirke.Count();
            ViewBag.test = test;
            //LoggerTest
            logger.LogInformation("Test Log");

            return View();
        }

        public IActionResult SelectStation()
        {
            List<Station> stations = test_db.Stationen.OrderBy(x => x.Station_Id).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            foreach (Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                stationsList.Add(stationItem);
            }

            ViewBag.Stationen = stationsList;
            return View();
        }

        public IActionResult AufgabeUmgebungLearn()
        {
            return View();
        }

        public IActionResult AufgabeUmgebungPractise()
        {
            return View();
        }

        public IActionResult ErgebnisOverview()
        {
            return View();
        }

        public IActionResult ZusatzInfo()
        {
            return View();
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

        public string CheckUserInfo(string bezirk, string ort, string stufe)
        {

            List<Ort> alle_orte_vom_bezirk = test_db.Orte.Where(x => x.Bezirk.Bezirksname.Equals(bezirk)).Select(x => x).ToList();

            if (Regex.IsMatch(ort, @"^[A-ZÄÖÜ][a-zA-ZÄÖÜäöü\-\. ]+$"))
            {
                foreach (Ort o in alle_orte_vom_bezirk)
                {
                    if (o.Ortsname.Equals(ort))
                    {
                        UserData.Bezirk = bezirk;
                        UserData.Ort = ort;
                        UserData.Stufe = stufe;

                        return "ok";
                    }

                }
            }
            return "wrong";
        }

        public string GlobalStationData(List<string> stations, string mode)
        {

            string stationsString = string.Join(",", stations.ToArray());

            UserData.Mode = mode;
            UserData.Stations = stationsString;

            //Aufgaben selektieren grob
            for (int i = 0; i < stations.Count; i++)
            {
                int selected_stationId = Convert.ToInt32(stations[i]);
                List<Aufgabe> stationsteil = test_db.Aufgaben
                    //.Include(x => x.Frage)
                    //.Include(x => x.Antwort)
                    //.Include(x => x.Zusatzinfo)
                    //.Include(x => x.Station)
                    //.Include(x => x.Stufe)
                    //.Include(x => x.Hintergrundbild)
                    .Where(x => x.Station.Station_Id == selected_stationId)
                    .ToList();
                UserData.Aufgaben.AddRange(stationsteil);
                if (i == 0)
                {
                    UserData.AktuelleStation = stationsteil[0].Station.Stationsname;
                }
            }
            UserData.AufgabenCount = UserData.Aufgaben.Count;

            return "ok";
        }


        public IActionResult GetGlobalData()
        {

            var RightAufgabeNr = UserData.AufgabeNr + 1;
            var RightAufgabenCount = UserData.AufgabenCount;

            if (UserData.AufgabenCount > 0)
            {
                Aufgabe nexteAufgabe = UserData.Aufgaben[UserData.AufgabeNr];
                UserData.AktuelleStation = nexteAufgabe.Station.Stationsname;
            }


            var data = new Dictionary<string, string>()
            {
                ["bezirk"] = UserData.Bezirk,
                ["ort"] = UserData.Ort,
                ["stufe"] = UserData.Stufe,
                ["mode"] = UserData.Mode,
                ["stations"] = UserData.Stations,
                ["aufgabenNr"] = RightAufgabeNr.ToString(),
                ["aufgabenCount"] = RightAufgabenCount.ToString(),
                ["aktuelleStation"] = UserData.AktuelleStation
            };
            return Json(data);
        }


        public IActionResult LoadFrage(string aufgabenNr)
        {

            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;

            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];

            string fragetyp = aufgabe.Frage.Typ.Typ;
            string fragetext = aufgabe.Frage.Fragetext;


            if (fragetyp.Equals("F_T"))
            {
                var frageText_model = new FrageText_Model();
                frageText_model.FrageText = fragetext;
                return PartialView("PartialViews/LoadFrageText", frageText_model);
            }
            else if (fragetyp.Equals("F_T+B"))
            {
                //gehört noch gemacht
            }
            else if (fragetyp.Equals("F_T+V"))
            {
                //gehört noch gemacht
            }

            return null;
        }

        public string PressedButtonLearn(string pressedButtonLearn)
        {
            UserData.pressedButtonLearn = pressedButtonLearn;

            if (UserData.pressedButtonLearn.Equals("zurueck"))
            {
                UserData.AufgabeNr = UserData.AufgabeNr - 1;
            }
            else if (UserData.pressedButtonLearn.Equals("weiter"))
            {
                UserData.AufgabeNr = UserData.AufgabeNr + 1;
            }

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
                //maybe alert?
                return "ok";
            }

        }

        public IActionResult LoadAntwortLearn(string aufgabenNr)
        {

            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;

            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];

            string antwort_typ = aufgabe.Antwort.Typ.Typ;
            int inhalt_id = aufgabe.Antwort.Inhalt_Id;

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

        public string CancelAufgabe()
        {
            UserData.Aufgaben = new List<Aufgabe>();
            UserData.AufgabeNr = 0;
            UserData.AufgabenCount = 0;
            return "ok";
        }

        public IActionResult LoadZusatzinfo()
        {
            int aufgabenNr_Int = Convert.ToInt32(UserData.AufgabeNr);
            logger.LogInformation("AufgabeNr: " + aufgabenNr_Int);
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];

            string info_typ = aufgabe.Zusatzinfo.Typ.Typ;

            string[] splitInfo_ = info_typ.Split('_');

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


        public string PressedButtonPractise()
        {
            UserData.AufgabeNr = UserData.AufgabeNr + 1;

            if (UserData.AufgabeNr == -1 || UserData.AufgabeNr == UserData.AufgabenCount)
            {
                UserData.AufgabeNr = UserData.AufgabeNr - 1;
                return "ok";
            }
            else
            {
                return "ok";
            }

        }
        //LoadAntwortPractise
        public IActionResult LoadAntwortPractise(string aufgabenNr)
        {
            int aufgabenNr_Int = Convert.ToInt32(aufgabenNr);
            aufgabenNr_Int--;
            Aufgabe aufgabe = UserData.Aufgaben[aufgabenNr_Int];
            string antwort_typ = aufgabe.Antwort.Typ.Typ;
            int inhalt_id = aufgabe.Antwort.Inhalt_Id;

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
            return antwortCheckBox_Model;
        }

        public AntwortRadioButtons_Model FillRadioButtonsTextModel(int inhalt_id)
        {
            Antwort_RadioButton antwortRadioButtonObject = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == inhalt_id);
            var antwortRadioButtons_Model = new AntwortRadioButtons_Model();
            antwortRadioButtons_Model.RadioButtons = test_db.RadioButtons.Where(x => x.Antwort_RadioButton.Inhalt_Id == antwortRadioButtonObject.Inhalt_Id).ToList();
            return antwortRadioButtons_Model;
        }
    }
}