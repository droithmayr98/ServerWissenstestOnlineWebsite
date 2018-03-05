using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_lib;
using Microsoft.Extensions.Logging;
using DB_lib.Tables;
using WissenstestOnlineWebseite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WissenstestOnlineWebseite.AdminModels;
using System.Data.Entity.Validation;

namespace WissenstestOnline.Controllers
{
    public class AdminController : Controller
    {

        /*private TestDB_Context main_db;*/
        private WissenstestDBEntities1 main_db;
        private ILogger<MainController> logger;

        public AdminController(/*TestDB_Context db,*/ ILogger<MainController> logger, WissenstestDBEntities1 main_db)
        {
            /*this.test_db = db;*/
            this.main_db = main_db;
            this.logger = logger;
        }

        //Admin Login Page
        public IActionResult Login()
        {
            return View();
        }


        //AdminSeiten
        public IActionResult AdminOverview()
        {
            //Alle Aufgaben holen und in Model speichern
            List<DB_lib.Aufgabe> alle_aufgaben = main_db.Aufgabe.Select(x => x).OrderBy(x=>x.Station.StationID).ToList();
            var adminOverwiew_model = new AdminOverview_Model();
            adminOverwiew_model.Aufgaben = alle_aufgaben;

            //Stationen als SelectListItems umwandeln und in Model speichern
            List<DB_lib.Station> stations = main_db.Station.OrderBy(x => x.StationID).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            stationsList.Add(new SelectListItem { Text = "keine ausgewählt", Value = "noItemSelected" });
            foreach (DB_lib.Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.StationID.ToString() };
                stationsList.Add(stationItem);
            }
            adminOverwiew_model.Stationen = stationsList;

            //Admins holen und in Model speichern + Modelübergabe an View
            List<Admintabelle> alle_admins = main_db.Admintabelle.Select(x => x).OrderBy(x => x.Benutzer).ToList();
            adminOverwiew_model.Admins = alle_admins;

            adminOverwiew_model.Can_create_admin = AdminData.Can_create_admin;
            adminOverwiew_model.Can_edit_admin = AdminData.Can_edit_admin;
            adminOverwiew_model.Can_delete_admin = AdminData.Can_delete_admin;
            adminOverwiew_model.Username = AdminData.Username;

            return View(adminOverwiew_model);

        }

        public IActionResult AufgabeEditView(int aufgabe_id)
        {
            var aufgabeInfo_edit_Model = FillAufgabeModel(aufgabe_id);

            var aufgabe_edit_main_Model = new AufgabeEditMain_Model();
            aufgabe_edit_main_Model.AufgabeInfo_Model = aufgabeInfo_edit_Model;
            aufgabe_edit_main_Model.AufgabeEditView_Model = FillSiteModel();
            return View(aufgabe_edit_main_Model);
        }

        public IActionResult AufgabeNew()
        {
            var aufgabeNew_Model = new AufgabeEditMain_Model();
            aufgabeNew_Model.AufgabeInfo_Model = null;
            aufgabeNew_Model.AufgabeEditView_Model = FillSiteModel();
            return View(aufgabeNew_Model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Start", "Main");
        }


        //Ajax calls
        public string CheckAdminInfo(string username, string password)
        {
            Admintabelle admin = main_db.Admintabelle.SingleOrDefault(x => x.Benutzer.Equals(username));
            if (admin == null)
            {
                return "username_fail";
            }
            else if (!admin.Passwort.Equals(password))
            {
                return "password_fail";
            }
            else
            {
                AdminData.Username = username;
                AdminData.Can_create_admin = admin.CanCreateAcc;
                AdminData.Can_edit_admin = admin.CanEditAcc;
                AdminData.Can_delete_admin = admin.CanDeleteAcc;
                return "ok";
            }
        }


        //AdminInteractions
        public IActionResult GetAdminInfo(string admin_id)
        {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminModals/AdminInfo_Modal", adminInfo_model);
        }

        public IActionResult GetAdminEdit(string admin_id)
        {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminModals/AdminEdit_Modal", adminInfo_model);
        }

        public string SaveAdminChanges(int admin_id, string username, string password, bool can_create_acc, bool can_edit_acc, bool can_delete_acc)
        {
            var edit_admin = main_db.Admintabelle.Single(x => x.AdminID == admin_id);
            edit_admin.Benutzer = username;
            edit_admin.Passwort = password;
            edit_admin.CanCreateAcc = can_create_acc;
            edit_admin.CanEditAcc = can_edit_acc;
            edit_admin.CanDeleteAcc = can_delete_acc;
            main_db.SaveChanges();
            return "ok";
        }

        public IActionResult GetAdminDelete(string admin_id)
        {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminModals/AdminDelete_Modal", adminInfo_model);
        }

        public string DeleteAdmin(int admin_id)
        {
            var delete_admin = main_db.Admintabelle.Single(x => x.AdminID == admin_id);
            main_db.Admintabelle.Remove(delete_admin);
            main_db.SaveChanges();
            return "ok";
        }

        public string CreateAdmin(string username, string password, bool can_create_acc, bool can_edit_acc, bool can_delete_acc)
        {
            Admintabelle new_admin = new Admintabelle();
            new_admin.Benutzer = username;
            new_admin.Passwort = password;
            new_admin.CanCreateAcc = can_create_acc;
            new_admin.CanEditAcc = can_edit_acc;
            new_admin.CanDeleteAcc = can_delete_acc;
            main_db.Admintabelle.Add(new_admin);
            main_db.SaveChanges();
            return "ok";
        }



        //FrageInteractions
        public IActionResult GetFrageInfo(int frage_id)
        {
            var frageInfo_Model = FillFrageInfoModel(frage_id);
            return PartialView("Modal_PartialViews/FrageModals/FrageInfo_Modal", frageInfo_Model);
        }

        public IActionResult GetFrageEdit(int frage_id)
        {
            var frageEdit_Model = FillFrageInfoModel(frage_id);
            return PartialView("Modal_PartialViews/FrageModals/FrageEdit_Modal", frageEdit_Model);
        }

        public string SaveFrageChanges(int frage_id, string fragetext)
        {
            DB_lib.Frage edit_frage = main_db.Frage.Single(x => x.FrageID == frage_id);
            edit_frage.FrageText = fragetext;
            edit_frage.Typendefinition = edit_frage.Typendefinition;

            main_db.SaveChanges();

            return "ok";
        }

        public IActionResult GetFrageDelete(int frage_id)
        {
            var frageDelete_Model = FillFrageInfoModel(frage_id);
            return PartialView("Modal_PartialViews/FrageModals/FrageDelete_Modal", frageDelete_Model);
        }

        public string DeleteFrage(int frage_id)
        {
            DB_lib.Frage frage_delete = main_db.Frage.Single(x => x.FrageID == frage_id);
            if (frage_delete.Aufgabe.Count == 0)
            {
                main_db.Frage.Remove(frage_delete);
                main_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }

        }

        public string CreateFrage(string fragetext)
        {
            DB_lib.Frage new_frage = new DB_lib.Frage();
            new_frage.FrageText = fragetext;
            new_frage.Aufgabe = new List<DB_lib.Aufgabe>();
            new_frage.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals("F_T"));

            main_db.Frage.Add(new_frage);
            main_db.SaveChanges();

            return "ok";
        }


        //AntwortInteractions
        public IActionResult GetAntwortInfo(int antwort_id)
        {
            var antwortInfo_Model = FillAntwortModel(antwort_id);
            return PartialView("Modal_PartialViews/AntwortModals/AntwortInfo_Modal", antwortInfo_Model);
        }

        public IActionResult GetAntwortEdit(int antwort_id)
        {
            var antwortEditMain_Model = new AntwortEditMain_Model();
            var antwortEdit_Model = FillAntwortModel(antwort_id);

            List<DB_lib.Typendefinition> antwortTypen = main_db.Typendefinition.Where(x => x.TypName.StartsWith("A_")).ToList<DB_lib.Typendefinition>();
            List<SelectListItem> antwortTypenList = new List<SelectListItem>();
            foreach (DB_lib.Typendefinition t in antwortTypen)
            {
                string typ_string = "";
                switch (t.TypName)
                {
                    case "A_T":
                        typ_string = "Text";
                        break;
                    case "A_S":
                        typ_string = "Slider";
                        break;
                    case "A_DP":
                        typ_string = "DatePicker";
                        break;
                    case "A_CB:T":
                        typ_string = "CheckBox";
                        break;
                    case "A_RB:T":
                        typ_string = "RadioButton";
                        break;
                }
                if (antwortEdit_Model.Antworttyp.Equals(typ_string))
                {
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.TypName, Value = t.TypName };
                    antwortTypItem.Selected = true;
                    antwortTypenList.Add(antwortTypItem);
                }
                else
                {
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.TypName, Value = t.TypName };
                    antwortTypenList.Add(antwortTypItem);
                }

            }
            var antwortEditView_Model = new AntwortEditView_Model();
            antwortEditView_Model.AntwortTypen = antwortTypenList;

            antwortEditMain_Model.AntwortInfo_Model = antwortEdit_Model;
            antwortEditMain_Model.AntwortEditView_Model = antwortEditView_Model;

            return PartialView("Modal_PartialViews/AntwortModals/AntwortEdit_Modal", antwortEditMain_Model);
        }

        public IActionResult LoadAntwortEditAntwort(int antwort_id)
        {
            var antwortEdit_Model = FillAntwortModel(antwort_id);

            switch (antwortEdit_Model.Antworttyp)
            {
                case "Text":
                    return PartialView("Antwort_PartialViews/AntwortEditTextPV", antwortEdit_Model);
                case "Slider":
                    return PartialView("Antwort_PartialViews/AntwortEditSliderPV", antwortEdit_Model);
                case "DatePicker":
                    return PartialView("Antwort_PartialViews/AntwortEditDatePickerPV", antwortEdit_Model);
                case "CheckBox":
                    return PartialView("Antwort_PartialViews/AntwortEditCheckBoxPV", antwortEdit_Model);
                case "RadioButton":
                    return PartialView("Antwort_PartialViews/AntwortEditRadioButtonsPV", antwortEdit_Model);
                default:
                    return PartialView("Antwort_PartialViews/AntwortEditDefaultPV", antwortEdit_Model);
            }

        }

        public IActionResult GetAntwortDelete(int antwort_id)
        {
            var antwortDelete_Model = FillAntwortModel(antwort_id);
            return PartialView("Modal_PartialViews/AntwortModals/AntwortDelete_Modal", antwortDelete_Model);
        }

        public string DeleteAntwort(int antwort_id)
        {
            DB_lib.Antwort antwort_delete = main_db.Antwort.Single(x => x.AntwortID == antwort_id);
            if (antwort_delete.Aufgabe.Count == 0)
            {
                string a_typ = antwort_delete.Typendefinition.TypName;
                int inhalt_id = antwort_delete.AntwortContentID;
                switch (a_typ)
                {
                    case "A_T":
                        Antwort_text antwort_Text_delete = main_db.Antwort_text.Single(x => x.AntwortContentID == inhalt_id);
                        main_db.Antwort_text.Remove(antwort_Text_delete);
                        break;
                    case "A_S":
                        Antwort_slider antwort_Slider_delete = main_db.Antwort_slider.Single(x => x.AntwortContentID == inhalt_id);
                        main_db.Antwort_slider.Remove(antwort_Slider_delete);
                        break;
                    case "A_DP":
                        Antwort_datepicker antwort_DP_delete = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == inhalt_id);
                        main_db.Antwort_datepicker.Remove(antwort_DP_delete);
                        break;
                    case "A_RB:T":
                        Antwort_radiobutton antwort_RB_delete = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == inhalt_id);
                        int rb_inhalt_id = antwort_RB_delete.AntwortContentID;
                        Radiobutton[] radioButtons_delete = main_db.Radiobutton.Where(x => x.Antwort_radiobutton.AntwortContentID == rb_inhalt_id).ToArray();
                        foreach (Radiobutton rb_delete in radioButtons_delete)
                        {
                            main_db.Radiobutton.Remove(rb_delete);
                        }
                        main_db.Antwort_radiobutton.Remove(antwort_RB_delete);
                        break;
                    case "A_CB:T":
                        Antwort_checkbox antwort_CB_delete = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == inhalt_id);
                        int cb_inhalt_id = antwort_CB_delete.AntwortContentID;
                        Checkbox[] checkBoxes_delete = main_db.Checkbox.Where(x => x.Antwort_checkbox.AntwortContentID == cb_inhalt_id).ToArray();
                        foreach (Checkbox cb_delete in checkBoxes_delete)
                        {
                            main_db.Checkbox.Remove(cb_delete);
                        }
                        main_db.Antwort_checkbox.Remove(antwort_CB_delete);
                        break;
                    default:
                        //nothing
                        break;
                }
                main_db.Antwort.Remove(antwort_delete);
                main_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }
        }

        //neue Antwort erstellen
        public string CreateAntwort_Text(string antwortName, string antwortTyp, string antwortText)
        {
            //Console.WriteLine("AntwortTyp: " + antwortTyp);
            //Console.WriteLine("AntwortName: " + antwortName);

            DB_lib.Antwort new_antwort = new DB_lib.Antwort();
            Antwort_text new_antwortText = new Antwort_text();

            new_antwortText.Text = antwortText;
            main_db.Antwort_text.Add(new_antwortText);
            main_db.SaveChanges();

            new_antwort.AntwortName = antwortName;
            new_antwort.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            new_antwort.AntwortContentID = new_antwortText.AntwortContentID;
            new_antwort.Aufgabe = new List<DB_lib.Aufgabe>();
            main_db.Antwort.Add(new_antwort);
            main_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_Slider(
                string antwortName,
                string antwortTyp,
                int sliderMin,
                int sliderMax,
                int sliderSprungweite,
                int sliderRightVal,
                string sliderTitel)
        {
            DB_lib.Antwort new_antwort = new DB_lib.Antwort();
            Antwort_slider new_antwortSlider = new Antwort_slider();

            new_antwortSlider.MinVal = sliderMin;
            new_antwortSlider.MaxVal = sliderMax;
            new_antwortSlider.Sprungweite = sliderSprungweite;
            new_antwortSlider.ErwartungsWert = sliderRightVal;
            if (sliderTitel == "")
            {
                new_antwortSlider.SliderText = null;
            }
            else
            {
                new_antwortSlider.SliderText = sliderTitel;
            }

            main_db.Antwort_slider.Add(new_antwortSlider);
            main_db.SaveChanges();

            new_antwort.AntwortName = antwortName;
            new_antwort.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            new_antwort.AntwortContentID = new_antwortSlider.AntwortContentID;
            new_antwort.Aufgabe = new List<DB_lib.Aufgabe>();
            main_db.Antwort.Add(new_antwort);
            main_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_DP(string antwortName, string antwortTyp, string date)
        {

            DB_lib.Antwort new_antwort = new DB_lib.Antwort();
            Antwort_datepicker new_antwortDP = new Antwort_datepicker();

            DateTime date_formated = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            new_antwortDP.Date = date_formated;
            main_db.Antwort_datepicker.Add(new_antwortDP);
            main_db.SaveChanges();

            new_antwort.AntwortName = antwortName;
            new_antwort.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            new_antwort.AntwortContentID = new_antwortDP.AntwortContentID;
            new_antwort.Aufgabe = new List<DB_lib.Aufgabe>();
            main_db.Antwort.Add(new_antwort);
            main_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_CB(string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {
            DB_lib.Antwort new_antwort = new DB_lib.Antwort();
            Antwort_checkbox new_antwortCB = new Antwort_checkbox();

            new_antwortCB.Anzahl = rightOptions.Length + wrongOptions.Length;
            new_antwortCB.Checkbox = new List<Checkbox>();
            main_db.Antwort_checkbox.Add(new_antwortCB);
            main_db.SaveChanges();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    Checkbox cb_right = new Checkbox();
                    cb_right.Inhalt = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_checkbox = new_antwortCB;
                    main_db.Checkbox.Add(cb_right);
                    main_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Checkbox cb_wrong = new Checkbox();
                    cb_wrong.Inhalt = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_checkbox = new_antwortCB;
                    main_db.Checkbox.Add(cb_wrong);
                    main_db.SaveChanges();
                }
            }

            new_antwort.AntwortName = antwortName;
            new_antwort.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            new_antwort.AntwortContentID = new_antwortCB.AntwortContentID;
            new_antwort.Aufgabe = new List<DB_lib.Aufgabe>();
            main_db.Antwort.Add(new_antwort);
            main_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_RB(string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            DB_lib.Antwort new_antwort = new DB_lib.Antwort();
            Antwort_radiobutton new_antwortRB = new Antwort_radiobutton();

            new_antwortRB.Anzahl = 1 + wrongOptions.Length;
            new_antwortRB.Radiobutton = new List<Radiobutton>();
            main_db.Antwort_radiobutton.Add(new_antwortRB);
            main_db.SaveChanges();

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Radiobutton rb_wrong = new Radiobutton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.ErwartungsWert = false;
                    rb_wrong.Antwort_radiobutton = new_antwortRB;
                    main_db.Radiobutton.Add(rb_wrong);
                    main_db.SaveChanges();
                }
            }

            Radiobutton rb_right = new Radiobutton();
            rb_right.Content = rightOption;
            rb_right.ErwartungsWert = true;
            rb_right.Antwort_radiobutton = new_antwortRB;
            main_db.Radiobutton.Add(rb_right);
            main_db.SaveChanges();

            new_antwort.AntwortName = antwortName;
            new_antwort.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            new_antwort.AntwortContentID = new_antwortRB.AntwortContentID;
            new_antwort.Aufgabe = new List<DB_lib.Aufgabe>();
            main_db.Antwort.Add(new_antwort);
            main_db.SaveChanges();

            return "ok";
        }

        //Antwort bearbeiten
        //Textfeld + Text auf bestehende Antwort einstellen
        public string EditNewAntwort_Text(int antwortId, string antwortName, string antwortTyp, string antwortText)
        {
            DB_lib.Antwort antwort_editNew = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            string a_typ = antwort_editNew.Typendefinition.TypName;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.AntwortContentID);

            Antwort_text new_antwortText = new Antwort_text();
            new_antwortText.Text = antwortText;
            main_db.Antwort_text.Add(new_antwortText);
            main_db.SaveChanges();

            antwort_editNew.AntwortName = antwortName;
            antwort_editNew.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            antwort_editNew.AntwortContentID = new_antwortText.AntwortContentID;
            main_db.SaveChanges();

            return "ok";
        }

        //Text des Textfeldes auf bestehende Antwort ändern
        public string EditAntwort_Text(int antwortId, string antwortName, string antwortTyp, string antwortText)
        {
            DB_lib.Antwort antwort_edit = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            Antwort_text antwort_text = main_db.Antwort_text.Single(x => x.AntwortContentID == antwort_edit.AntwortContentID);
            antwort_text.Text = antwortText;
            antwort_edit.AntwortName = antwortName;
            main_db.SaveChanges();

            return "ok";
        }

        //Slider auf bestehende Antwort einstellen
        public string EditNewAntwort_Slider(int antwortId, string antwortName, string antwortTyp, int sliderMin,
                int sliderMax,
                int sliderSprungweite,
                int sliderRightVal,
                string sliderTitel)
        {

            DB_lib.Antwort antwort_editNew = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            string a_typ = antwort_editNew.Typendefinition.TypName;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.AntwortContentID);

            Antwort_slider new_antwortSlider = new Antwort_slider();
            new_antwortSlider.MinVal = sliderMin;
            new_antwortSlider.MaxVal = sliderMax;
            new_antwortSlider.Sprungweite = sliderSprungweite;
            new_antwortSlider.ErwartungsWert = sliderRightVal;
            if (sliderTitel == "")
            {
                new_antwortSlider.SliderText = null;
            }
            else
            {
                new_antwortSlider.SliderText = sliderTitel;
            }
            main_db.Antwort_slider.Add(new_antwortSlider);
            main_db.SaveChanges();

            antwort_editNew.AntwortName = antwortName;
            antwort_editNew.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            antwort_editNew.AntwortContentID = new_antwortSlider.AntwortContentID;
            main_db.SaveChanges();

            return "ok";
        }

        //Sliderwerte auf bestehende Antwort ändern
        public string EditAntwort_Slider(int antwortId, string antwortName, string antwortTyp, int sliderMin,
                int sliderMax,
                int sliderSprungweite,
                int sliderRightVal,
                string sliderTitel)
        {

            DB_lib.Antwort antwort_edit = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            Antwort_slider antwort_slider = main_db.Antwort_slider.Single(x => x.AntwortContentID == antwort_edit.AntwortContentID);
            antwort_slider.MinVal = sliderMin;
            antwort_slider.MaxVal = sliderMax;
            antwort_slider.Sprungweite = sliderSprungweite;
            antwort_slider.ErwartungsWert = sliderRightVal;
            if (sliderTitel == "")
            {
                antwort_slider.SliderText = null;
            }
            else
            {
                antwort_slider.SliderText = sliderTitel;
            }
            antwort_edit.AntwortName = antwortName;
            main_db.SaveChanges();

            return "ok";
        }

        //DP auf bestehende Antwort einstellen
        public string EditNewAntwort_DP(int antwortId, string antwortName, string antwortTyp, string date)
        {
            DB_lib.Antwort antwort_editNew = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            string a_typ = antwort_editNew.Typendefinition.TypName;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.AntwortContentID);

            Antwort_datepicker new_antwortDP = new Antwort_datepicker();
            new_antwortDP.Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            main_db.Antwort_datepicker.Add(new_antwortDP);
            main_db.SaveChanges();

            antwort_editNew.AntwortName = antwortName;
            antwort_editNew.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            antwort_editNew.AntwortContentID = new_antwortDP.AntwortContentID;
            main_db.SaveChanges();

            return "ok";
        }

        //DP-Datum auf bestehende Antwort ändern
        public string EditAntwort_DP(int antwortId, string antwortName, string antwortTyp, string date)
        {
            DB_lib.Antwort antwort_edit = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            Antwort_datepicker antwort_DP = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == antwort_edit.AntwortContentID);
            antwort_DP.Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            antwort_edit.AntwortName = antwortName;
            main_db.SaveChanges();

            return "ok";
        }

        //CB-Antwortart auf bestehende Antwort einstellen
        public string EditNewAntwort_CB(int antwortId, string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {

            DB_lib.Antwort antwort_editNew = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            string a_typ = antwort_editNew.Typendefinition.TypName;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.AntwortContentID);

            Antwort_checkbox new_antwortCB = new Antwort_checkbox();
            new_antwortCB.Anzahl = rightOptions.Length + wrongOptions.Length;
            new_antwortCB.Checkbox = new List<Checkbox>();
            main_db.Antwort_checkbox.Add(new_antwortCB);
            main_db.SaveChanges();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    Checkbox cb_right = new Checkbox();
                    cb_right.Inhalt = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_checkbox = new_antwortCB;
                    main_db.Checkbox.Add(cb_right);
                    main_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Checkbox cb_wrong = new Checkbox();
                    cb_wrong.Inhalt = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_checkbox = new_antwortCB;
                    main_db.Checkbox.Add(cb_wrong);
                    main_db.SaveChanges();
                }
            }

            antwort_editNew.AntwortName = antwortName;
            antwort_editNew.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            antwort_editNew.AntwortContentID = new_antwortCB.AntwortContentID;
            main_db.SaveChanges();

            return "ok";
        }

        //CBs auf bestehende Antwort ändern
        public string EditAntwort_CB(int antwortId, string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {
            DB_lib.Antwort antwort_edit = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            Antwort_checkbox antwort_CB = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == antwort_edit.AntwortContentID);
            antwort_CB.Anzahl = rightOptions.Length + wrongOptions.Length;

            List<Checkbox> checkBoxesOld = antwort_CB.Checkbox.ToList();
            for (int i = checkBoxesOld.Count - 1; i >= 0; i--)
            {
                main_db.Checkbox.Remove(checkBoxesOld[i]);
            }
            main_db.SaveChanges();

            antwort_CB.Checkbox = new List<Checkbox>();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    Checkbox cb_right = new Checkbox();
                    cb_right.Inhalt = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_checkbox = antwort_CB;
                    main_db.Checkbox.Add(cb_right);
                    main_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Checkbox cb_wrong = new Checkbox();
                    cb_wrong.Inhalt = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_checkbox = antwort_CB;
                    main_db.Checkbox.Add(cb_wrong);
                    main_db.SaveChanges();
                }
            }

            antwort_edit.AntwortName = antwortName;
            main_db.SaveChanges();

            return "ok";
        }

        //RB-Antwortart auf bestehende Antwort einstellen
        public string EditNewAntwort_RB(int antwortId, string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            DB_lib.Antwort antwort_editNew = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            string a_typ = antwort_editNew.Typendefinition.TypName;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.AntwortContentID);

            Antwort_radiobutton new_antwortRB = new Antwort_radiobutton();
            new_antwortRB.Anzahl = 1 + wrongOptions.Length;
            new_antwortRB.Radiobutton = new List<Radiobutton>();
            main_db.Antwort_radiobutton.Add(new_antwortRB);
            main_db.SaveChanges();

            if (rightOption != "")
            {
                Radiobutton rb_right = new Radiobutton();
                rb_right.Content = rightOption;
                rb_right.ErwartungsWert = true;
                rb_right.Antwort_radiobutton = new_antwortRB;
                main_db.Radiobutton.Add(rb_right);
                main_db.SaveChanges();
            }


            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Radiobutton rb_wrong = new Radiobutton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.ErwartungsWert = false;
                    rb_wrong.Antwort_radiobutton = new_antwortRB;
                    main_db.Radiobutton.Add(rb_wrong);
                    main_db.SaveChanges();
                }
            }

            antwort_editNew.AntwortName = antwortName;
            antwort_editNew.Typendefinition = main_db.Typendefinition.Single(x => x.TypName.Equals(antwortTyp));
            antwort_editNew.AntwortContentID = new_antwortRB.AntwortContentID;
            main_db.SaveChanges();

            return "ok";
        }

        //RBs auf bestehende Antwort ändern
        public string EditAntwort_RB(int antwortId, string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            DB_lib.Antwort antwort_edit = main_db.Antwort.Single(x => x.AntwortID == antwortId);
            Antwort_radiobutton antwort_RB = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == antwort_edit.AntwortContentID);
            antwort_RB.Anzahl = 1 + wrongOptions.Length;

            List<Radiobutton> radioButtonsOld = antwort_RB.Radiobutton.ToList();
            for (int i = radioButtonsOld.Count - 1; i >= 0; i--)
            {
                main_db.Radiobutton.Remove(radioButtonsOld[i]);
            }
            main_db.SaveChanges();

            antwort_RB.Radiobutton = new List<Radiobutton>();

            if (rightOption != "")
            {
                Radiobutton rb_right = new Radiobutton();
                rb_right.Content = rightOption;
                rb_right.ErwartungsWert = true;
                rb_right.Antwort_radiobutton = antwort_RB;
                main_db.Radiobutton.Add(rb_right);
                main_db.SaveChanges();
            }
            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    Radiobutton rb_wrong = new Radiobutton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.ErwartungsWert = false;
                    rb_wrong.Antwort_radiobutton = antwort_RB;
                    main_db.Radiobutton.Add(rb_wrong);
                    main_db.SaveChanges();
                }
            }

            antwort_edit.AntwortName = antwortName;
            main_db.SaveChanges();

            return "ok";
        }

        //weitere Antwortmethoden
        public IActionResult SetNewAntwortType(string typ_id)
        {
            switch (typ_id)
            {
                case "A_T":
                    return PartialView("AntwortNew_PartialViews/AntwortNewTextPV");
                case "A_S":
                    return PartialView("AntwortNew_PartialViews/AntwortNewSliderPV");
                case "A_DP":
                    return PartialView("AntwortNew_PartialViews/AntwortNewDatePickerPV");
                case "A_CB:T":
                    return PartialView("AntwortNew_PartialViews/AntwortNewCheckBoxPV");
                case "A_RB:T":
                    return PartialView("AntwortNew_PartialViews/AntwortNewRadioButtonsPV");
                default:
                    return PartialView("AntwortNew_PartialViews/AntwortNewDefaultPV");
            }

        }



        //ZusatzInfoInteractions
        public IActionResult GetZusatzInfo_Info(int info_id)
        {
            var infoZusatzInfo_Model = FillInfoModel(info_id);
            return PartialView("Modal_PartialViews/ZusatzInfoModals/ZusatzInfo_InfoModal", infoZusatzInfo_Model);
        }

        public IActionResult GetZusatzInfoDelete(int info_id)
        {
            var infoDelete_Model = FillInfoModel(info_id);
            return PartialView("Modal_PartialViews/ZusatzInfoModals/ZusatzInfoDelete_Modal", infoDelete_Model);
        }

        public string DeleteZusatzInfo(int info_id)
        {
            DB_lib.Zusatzinfo info_delete = main_db.Zusatzinfo.Single(x => x.ZusatzinfoID == info_id);
            if (info_delete.Aufgabe.Count == 0)
            {
                DB_lib.Infocontent[] infoContentDel = info_delete.Infocontent.ToArray();
                foreach (DB_lib.Infocontent infC in infoContentDel)
                {
                    main_db.Infocontent.Remove(infC);
                }
                main_db.Zusatzinfo.Remove(info_delete);
                main_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }
        }

        public IActionResult GetZusatzInfo_Edit(int info_id)
        {
            var editZusatzInfo_Model = FillInfoModel(info_id);
            return PartialView("Modal_PartialViews/ZusatzInfoModals/ZusatzInfoEdit_Modal", editZusatzInfo_Model);
        }

        public string CreateNewZusatzInfo(string zusatzInfoName, string[] headings, string[] contents)
        {

            string type_generated = "I_";

            for (int i = 0; i < contents.Length; i++)
            {
                type_generated = type_generated + "T";
            }

            DB_lib.Typendefinition typ = main_db.Typendefinition.SingleOrDefault(x => x.TypName.Equals(type_generated));
            if (typ == null)
            {
                typ = new DB_lib.Typendefinition();
                typ.TypName = type_generated;
                typ.Zusatzinfo = new List<DB_lib.Zusatzinfo>();
                typ.Antwort = new List<DB_lib.Antwort>();
                typ.Frage = new List<DB_lib.Frage>();
                main_db.Typendefinition.Add(typ);
                main_db.SaveChanges();
            }

            DB_lib.Zusatzinfo new_zusatzInfo = new DB_lib.Zusatzinfo();
            new_zusatzInfo.Aufgabe = new List<DB_lib.Aufgabe>();
            new_zusatzInfo.Infocontent = new List<DB_lib.Infocontent>();
            new_zusatzInfo.InfoName = zusatzInfoName;
            new_zusatzInfo.Typendefinition = typ;

            main_db.Zusatzinfo.Add(new_zusatzInfo);
            main_db.SaveChanges();

            for (int i = 0; i < contents.Length; i++)
            {
                if (headings[i] == null)
                {
                    DB_lib.Infocontent ic = new DB_lib.Infocontent();
                    ic.Zusatzinfo = new_zusatzInfo;
                    ic.InfoContent1 = contents[i];
                    main_db.Infocontent.Add(ic);
                    main_db.SaveChanges();
                }
                else
                {
                    Infocontent ic = new Infocontent();
                    ic.Zusatzinfo = new_zusatzInfo;
                    ic.Heading = headings[i];
                    ic.InfoContent1 = contents[i];
                    main_db.Infocontent.Add(ic);
                    main_db.SaveChanges();
                }

            }


            return "ok";
        }

        public string EditZusatzInfo(int zusatzInfoId, string zusatzInfoName, string[] headings, string[] contents)
        {

            string type_generated = "I_";

            for (int i = 0; i < contents.Length; i++)
            {
                type_generated = type_generated + "T";
            }

            DB_lib.Typendefinition typ = main_db.Typendefinition.SingleOrDefault(x => x.TypName.Equals(type_generated));
            if (typ == null)
            {
                typ = new DB_lib.Typendefinition();
                typ.TypName = type_generated;
                typ.Zusatzinfo = new List<DB_lib.Zusatzinfo>();
                typ.Antwort = new List<DB_lib.Antwort>();
                typ.Frage = new List<DB_lib.Frage>();
                main_db.Typendefinition.Add(typ);
                main_db.SaveChanges();
            }

            DB_lib.Zusatzinfo edit_zusatzinfo = main_db.Zusatzinfo.Single(x => x.ZusatzinfoID == zusatzInfoId);

            List<DB_lib.Infocontent> old_infoC = edit_zusatzinfo.Infocontent.ToList();
            for (int i = 0; i < old_infoC.Count; i++)
            {
                main_db.Infocontent.Remove(old_infoC[i]);
                main_db.SaveChanges();
            }

            edit_zusatzinfo.InfoName = zusatzInfoName;
            edit_zusatzinfo.Typendefinition = typ;

            main_db.SaveChanges();

            for (int i = 0; i < contents.Length; i++)
            {
                if (headings[i] == null)
                {
                    DB_lib.Infocontent ic = new DB_lib.Infocontent();
                    ic.Zusatzinfo = edit_zusatzinfo;
                    ic.InfoContent1 = contents[i];
                    main_db.Infocontent.Add(ic);
                    main_db.SaveChanges();
                }
                else
                {
                    DB_lib.Infocontent ic = new DB_lib.Infocontent();
                    ic.Zusatzinfo = edit_zusatzinfo;
                    ic.Heading = headings[i];
                    ic.InfoContent1 = contents[i];
                    main_db.Infocontent.Add(ic);
                    main_db.SaveChanges();
                }

            }

            return "ok";
        }



        //AufgabeInteractions
        public IActionResult GetAufgabeInfo(string aufgabe_id)
        {
            int aufgabeId_int = Convert.ToInt32(aufgabe_id);
            var aufgabeInfo_model = FillAufgabeModel(aufgabeId_int);
            return PartialView("Modal_PartialViews/AufgabeModals/AufgabeInfo_Modal", aufgabeInfo_model);
        }

        public string EditAufgabe(int aufgabe_id,
        int aufgabe_station,
        string aufgabe_stufe,
        bool pflichtaufgabe,
        string teilaufgabeVon,
        string aufgabe_bezirk,
        string aufgabe_ort,
        int aufgabe_frage,
        string aufgabe_antwort,
        string aufgabe_zusatzinfo)
        {
            DB_lib.Aufgabe aufgabe_edit = main_db.Aufgabe.Single(x => x.AufgabeID == aufgabe_id);

            aufgabe_edit.Station = main_db.Station.Single(x => x.StationID == aufgabe_station);
            aufgabe_edit.Stufe = main_db.Stufe.Single(x => x.Stufenname.StartsWith(aufgabe_stufe));
            aufgabe_edit.Pflichtaufgabe = pflichtaufgabe;
            if (teilaufgabeVon != null)
            {
                aufgabe_edit.TeilAufgabeVon = main_db.Aufgabe.SingleOrDefault(x => x.AufgabeID == Convert.ToInt32(teilaufgabeVon)).TeilAufgabeVon;//ist int, sollte Aufgabe sein
            }
            else {
                aufgabe_edit.TeilAufgabeVon = null;
            }
            aufgabe_edit.Bezirk = main_db.Bezirk.Single(x => x.BezirkName.Equals(aufgabe_bezirk));
            aufgabe_edit.Standort = main_db.Standort.SingleOrDefault(x => x.Ortsname.Equals(aufgabe_ort));
            if (aufgabe_frage != -1)
            {
                aufgabe_edit.Frage = main_db.Frage.Single(x => x.FrageID == aufgabe_frage);
            }

            if (!aufgabe_antwort.Equals("-1"))
            {
                string[] antwort_split = aufgabe_antwort.Split('_');
                int antwort = Convert.ToInt32(antwort_split[1]);
                aufgabe_edit.Antwort = main_db.Antwort.Single(x => x.AntwortID == antwort);
            }

            if (!aufgabe_zusatzinfo.Equals("-1"))
            {
                string[] zusatzinfo_split = aufgabe_zusatzinfo.Split('_');
                int zusatzinfo = Convert.ToInt32(zusatzinfo_split[1]);
                aufgabe_edit.Zusatzinfo = main_db.Zusatzinfo.Single(x => x.ZusatzinfoID == zusatzinfo);
            }


            main_db.SaveChanges();

            return "ok";
        }

        public string CreateAufgabe(int aufgabe_station,
        string aufgabe_stufe,
        bool pflichtaufgabe,
        string teilaufgabeVon,
        string aufgabe_bezirk,
        string aufgabe_ort,
        int aufgabe_frage,
        string aufgabe_antwort,
        string aufgabe_zusatzinfo)
        {

            string[] antwort_split = aufgabe_antwort.Split('_');
            int antwort = Convert.ToInt32(antwort_split[1]);
            string[] zusatzinfo_split = aufgabe_zusatzinfo.Split('_');
            int zusatzinfo = Convert.ToInt32(zusatzinfo_split[1]);

            DB_lib.Aufgabe new_aufgabe = new DB_lib.Aufgabe();
            new_aufgabe.Station = main_db.Station.Single(x => x.StationID == aufgabe_station);
            new_aufgabe.Stufe = main_db.Stufe.Single(x => x.Stufenname.StartsWith(aufgabe_stufe));
            new_aufgabe.Pflichtaufgabe = pflichtaufgabe;
            if (teilaufgabeVon != null)
            {
                new_aufgabe.TeilAufgabeVon = main_db.Aufgabe.SingleOrDefault(x => x.AufgabeID == Convert.ToInt32(teilaufgabeVon)).TeilAufgabeVon;//ist int, sollte Aufgabe sein
            }
            else {
                new_aufgabe.TeilAufgabeVon = null;
            }
            new_aufgabe.Bezirk = main_db.Bezirk.Single(x => x.BezirkName.Equals(aufgabe_bezirk));
            new_aufgabe.Standort = main_db.Standort.SingleOrDefault(x => x.Ortsname.Equals(aufgabe_ort));
            new_aufgabe.Frage = main_db.Frage.Single(x => x.FrageID == aufgabe_frage);
            new_aufgabe.Antwort = main_db.Antwort.Single(x => x.AntwortID == antwort);
            new_aufgabe.Zusatzinfo = main_db.Zusatzinfo.Single(x => x.ZusatzinfoID == zusatzinfo);

            main_db.Aufgabe.Add(new_aufgabe);
            main_db.SaveChanges();

            return "ok";
        }

        public IActionResult GetAufgabeDelete(int aufgabe_id)
        {
            var aufgabeDelete_Model = FillAufgabeModel(aufgabe_id);
            return PartialView("Modal_PartialViews/AufgabeModals/AufgabeDelete_Modal", aufgabeDelete_Model);
        }

        public string DeleteAufgabe(int aufgabe_id)
        {
            DB_lib.Aufgabe aufgabe_delete = main_db.Aufgabe.Single(x => x.AufgabeID == aufgabe_id);
            main_db.Aufgabe.Remove(aufgabe_delete);
            main_db.SaveChanges();
            return "ok";
        }



        //Anderes
        public IActionResult SetStandorteBezirkComboBox(string bezirk)
        {
            List<Standort> standorte_ff = main_db.Standort.Where(x => x.Bezirk.BezirkName.Equals(bezirk)).ToList();

            List<SelectListItem> standorteList = new List<SelectListItem>();
            SelectListItem kein_standort = new SelectListItem { Text = "kein Standort ausgewählt", Value = "noStandortSelected" };
            kein_standort.Selected = true;
            standorteList.Add(kein_standort);

            foreach (Standort o in standorte_ff)
            {
                SelectListItem standortItem = new SelectListItem { Text = o.Ortsname, Value = o.Ortsname };
                standorteList.Add(standortItem);
            }

            var newComboBoxValues_Model = new NewComboBoxValues_Model();
            newComboBoxValues_Model.New_values = standorteList;

            return PartialView("NewComboBoxValues", newComboBoxValues_Model);
        }

        public string GetTypIdFromTyp(string typ)
        {
            if (typ.Equals("noItemSelected"))
            {
                return "noItemSelected";
            }
            else
            {
                return main_db.Typendefinition.Single(x => x.TypName.Equals(typ)).TypendefinitionID.ToString();
            }
        }

             

        //Fill-Methods
        public AntwortInfo_Model FillAntwortModel(int antwort_id)
        {
            DB_lib.Antwort antwort = main_db.Antwort.Single(x => x.AntwortID == antwort_id);
            var antwortInfo_Model = new AntwortInfo_Model();
            antwortInfo_Model.Antwort_Id = antwort.AntwortID;
            antwortInfo_Model.Antwortname = antwort.AntwortName;
            int antwortInhalt_id = antwort.AntwortContentID;

            switch (antwort.Typendefinition.TypName)
            {
                case "A_T":
                    antwortInfo_Model.Antworttyp = "Text";
                    antwortInfo_Model.Antwort_Text = main_db.Antwort_text.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_S":
                    antwortInfo_Model.Antworttyp = "Slider";
                    antwortInfo_Model.Antwort_Slider = main_db.Antwort_slider.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_DP":
                    antwortInfo_Model.Antworttyp = "DatePicker";
                    antwortInfo_Model.Antwort_DatePicker = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_CB:T":
                    antwortInfo_Model.Antworttyp = "CheckBox";
                    antwortInfo_Model.Antwort_CheckBoxes = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == antwortInhalt_id).Checkbox.ToList();
                    break;
                case "A_RB:T":
                    antwortInfo_Model.Antworttyp = "RadioButton";
                    antwortInfo_Model.Antwort_RadioButtons = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == antwortInhalt_id).Radiobutton.ToList();
                    break;
            }

            return antwortInfo_Model;
        }

        public AdminInfo_Model FillAdminModel(int adminId_int)
        {

            Admintabelle admin = main_db.Admintabelle.Single(x => x.AdminID == adminId_int);
            var adminInfo_model = new AdminInfo_Model();
            adminInfo_model.Id = admin.AdminID;
            adminInfo_model.Username = admin.Benutzer;
            adminInfo_model.Password = admin.Passwort;
            adminInfo_model.Can_create_acc = admin.CanCreateAcc;
            adminInfo_model.Can_edit_acc = admin.CanEditAcc;
            adminInfo_model.Can_delete_acc = admin.CanDeleteAcc;
            return adminInfo_model;
        }

        public AufgabeInfo_Model FillAufgabeModel(int aufgabeId_int)
        {
            DB_lib.Aufgabe aufgabe = main_db.Aufgabe.Single(x => x.AufgabeID == aufgabeId_int);
            var aufgabeInfo_model = new AufgabeInfo_Model();
            aufgabeInfo_model.Aufgabe_Id = aufgabe.AufgabeID;
            aufgabeInfo_model.Station = aufgabe.Station.Stationsname;
            aufgabeInfo_model.Stufe = aufgabe.Stufe.Stufenname;
            aufgabeInfo_model.IsPflichtaufgabe = aufgabe.Pflichtaufgabe;
            aufgabeInfo_model.Bezirk = aufgabe.Bezirk == null ? "-" : aufgabe.Bezirk.BezirkName;
            aufgabeInfo_model.Ort = aufgabe.Standort == null ? "-" : aufgabe.Standort.Ortsname;
            aufgabeInfo_model.TeilAufgabeVon = aufgabe.TeilAufgabeVon == null ? "-" : aufgabe.TeilAufgabeVon.ToString();
            aufgabeInfo_model.Antwort_Id = aufgabe.Antwort.AntwortID;
            aufgabeInfo_model.Antwort_Name = aufgabe.Antwort.AntwortName;

            aufgabeInfo_model.Frage = aufgabe.Frage;

            aufgabeInfo_model.Info = aufgabe.Zusatzinfo.Infocontent.ToList();
            aufgabeInfo_model.Zusatzinfo = aufgabe.Zusatzinfo;


            //Antwort
            string aufgabe_typ = aufgabe.Antwort.Typendefinition.TypName;
            int antwortInhalt_id = aufgabe.Antwort.AntwortContentID;

            switch (aufgabe_typ)
            {
                case "A_T":
                    aufgabeInfo_model.Antworttyp = "Text";
                    aufgabeInfo_model.Antwort_Text = main_db.Antwort_text.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_S":
                    aufgabeInfo_model.Antworttyp = "Slider";
                    aufgabeInfo_model.Antwort_Slider = main_db.Antwort_slider.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_DP":
                    aufgabeInfo_model.Antworttyp = "DatePicker";
                    aufgabeInfo_model.Antwort_DatePicker = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == antwortInhalt_id);
                    break;
                case "A_CB:T":
                    aufgabeInfo_model.Antworttyp = "CheckBox";
                    aufgabeInfo_model.Antwort_CheckBoxes = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == antwortInhalt_id).Checkbox.ToList();
                    break;
                case "A_RB:T":
                    aufgabeInfo_model.Antworttyp = "RadioButton";
                    aufgabeInfo_model.Antwort_RadioButtons = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == antwortInhalt_id).Radiobutton.ToList();
                    break;
            }

            return aufgabeInfo_model;
        }

        public AufgabeEditView_Model FillSiteModel()
        {

            //Stationen
            List<DB_lib.Station> stations = main_db.Station.OrderBy(x => x.StationID).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            foreach (DB_lib.Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.StationID.ToString() };
                stationsList.Add(stationItem);
            }

            //FFs_1
            List<Ort> standorte_ff = new List<Ort>();
            List<SelectListItem> standorteList = new List<SelectListItem>();
            standorteList.Add(new SelectListItem { Text = "kein Standort ausgewählt", Value = "noStandortSelected" });

            //Bezirke
            List<DB_lib.Bezirk> bezirke = main_db.Bezirk.OrderBy(x => x.BezirkName).ToList();
            List<SelectListItem> bezirkeList = new List<SelectListItem>();
            bezirkeList.Add(new SelectListItem { Text = "kein Bezirk ausgewählt", Value = "noBezirkSelected" });
            foreach (DB_lib.Bezirk b in bezirke)
            {
                SelectListItem bezirkItem = new SelectListItem { Text = b.BezirkName, Value = b.BezirkName };
                bezirkeList.Add(bezirkItem);
            }

            //FFs_2
            foreach (Ort o in standorte_ff)
            {
                SelectListItem standortItem = new SelectListItem { Text = o.Ortsname, Value = o.Ortsname };
                standorteList.Add(standortItem);
            }

            //Fragen
            List<DB_lib.Frage> fragen = main_db.Frage.OrderBy(x => x.FrageID).ToList();

            //Antworten
            List<DB_lib.Antwort> antworten = main_db.Antwort.OrderBy(x => x.AntwortID).ToList();

            //Zusatzinfo
            List<DB_lib.Zusatzinfo> infos = main_db.Zusatzinfo.OrderBy(x => x.ZusatzinfoID).ToList();

            //AntwortTypen
            List<DB_lib.Typendefinition> antwort_typen = main_db.Typendefinition.Where(x => x.TypName.StartsWith("A")).ToList();

            List<SelectListItem> antwort_typen_list = new List<SelectListItem>();
            antwort_typen_list.Add(new SelectListItem { Text = "keinen ausgewählt", Value = "noItemSelected" });
            foreach (DB_lib.Typendefinition t in antwort_typen)
            {
                SelectListItem antwortTypItem = new SelectListItem { Text = t.TypName, Value = t.TypName };
                antwort_typen_list.Add(antwortTypItem);
            }

            //ZusatzinfoTypen
            List<DB_lib.Typendefinition> info_typen = main_db.Typendefinition.Where(x => x.TypName.StartsWith("I")).ToList();

            List<SelectListItem> info_typen_list = new List<SelectListItem>();
            info_typen_list.Add(new SelectListItem { Text = "keinen ausgewählt", Value = "noItemSelected" });
            foreach (DB_lib.Typendefinition t in info_typen)
            {
                SelectListItem infoTypItem = new SelectListItem { Text = t.TypName, Value = t.TypName.ToString() };
                info_typen_list.Add(infoTypItem);
            }

            var aufgabe_view_Model = new AufgabeEditView_Model();

            aufgabe_view_Model.Stationen = stationsList;
            aufgabe_view_Model.Bezirke = bezirkeList;
            aufgabe_view_Model.Standorte = standorteList;
            aufgabe_view_Model.Fragen = fragen;
            aufgabe_view_Model.Antworten = antworten;
            aufgabe_view_Model.Infos = infos;
            aufgabe_view_Model.Antwort_Typen = antwort_typen_list;
            aufgabe_view_Model.Info_Typen = info_typen_list;

            return aufgabe_view_Model;
        }

        public FrageInfo_Model FillFrageInfoModel(int frage_id)
        {
            DB_lib.Frage frage = main_db.Frage.Single(x => x.FrageID == frage_id);
            var frageInfo_Model = new FrageInfo_Model();
            frageInfo_Model.FrageId = frage.FrageID;
            frageInfo_Model.FrageText = frage.FrageText;
            return frageInfo_Model;
        }

        public ZusatzInfo_InfoModel FillInfoModel(int info_id)
        {
            DB_lib.Zusatzinfo zusatzInfo = main_db.Zusatzinfo.Single(x => x.ZusatzinfoID == info_id);
            Infocontent[] infoContents = main_db.Infocontent.Where(x => x.Zusatzinfo.ZusatzinfoID == info_id).ToArray();
            List<ZusatzInfoTextblock> infoTextblocks = new List<ZusatzInfoTextblock>();

            ZusatzInfoTextOnly_Model zusatzInfo_Model = new ZusatzInfoTextOnly_Model();
            foreach (Infocontent infoCon in infoContents)
            {
                ZusatzInfoTextblock textBlock = new ZusatzInfoTextblock();
                textBlock.Heading = infoCon.Heading;
                textBlock.Text = infoCon.InfoContent1;
                infoTextblocks.Add(textBlock);
            }

            var zusatzInfo_InfoModel = new ZusatzInfo_InfoModel();
            zusatzInfo_InfoModel.Texte = infoTextblocks;
            zusatzInfo_InfoModel.ZusatzInfo_Id = zusatzInfo.ZusatzinfoID;
            zusatzInfo_InfoModel.ZusatzInfo_Typ = zusatzInfo.Typendefinition.TypName;
            zusatzInfo_InfoModel.ZusatzInfo_Name = zusatzInfo.InfoName;

            return zusatzInfo_InfoModel;
        }



        //Other Methods
        //Alte Antwortdefinition löschen
        public void DeleteOldAntwortDefinition(string a_typ, int inhalt_Id)
        {
            switch (a_typ)
            {
                case "A_T":
                    Antwort_text a_text = main_db.Antwort_text.Single(x => x.AntwortContentID == inhalt_Id);
                    main_db.Antwort_text.Remove(a_text);
                    main_db.SaveChanges();
                    break;
                case "A_S":
                    Antwort_slider a_slider = main_db.Antwort_slider.Single(x => x.AntwortContentID == inhalt_Id);
                    main_db.Antwort_slider.Remove(a_slider);
                    main_db.SaveChanges();
                    break;
                case "A_DP":
                    Antwort_datepicker a_DP = main_db.Antwort_datepicker.Single(x => x.AntwortContentID == inhalt_Id);
                    main_db.Antwort_datepicker.Remove(a_DP);
                    main_db.SaveChanges();
                    break;
                case "A_RB:T":
                    Antwort_radiobutton a_RB = main_db.Antwort_radiobutton.Single(x => x.AntwortContentID == inhalt_Id);
                    List<Radiobutton> radioButtons = a_RB.Radiobutton.ToList();
                    foreach (Radiobutton rb in radioButtons)
                    {
                        main_db.Radiobutton.Remove(rb);
                        main_db.SaveChanges();
                    }
                    main_db.Antwort_radiobutton.Remove(a_RB);
                    main_db.SaveChanges();
                    break;
                case "A_CB:T":
                    Antwort_checkbox a_CB = main_db.Antwort_checkbox.Single(x => x.AntwortContentID == inhalt_Id);
                    List<Checkbox> checkboxes = a_CB.Checkbox.ToList();
                    foreach (Checkbox cb in checkboxes)
                    {
                        main_db.Checkbox.Remove(cb);
                        main_db.SaveChanges();
                    }
                    main_db.Antwort_checkbox.Remove(a_CB);
                    main_db.SaveChanges();
                    break;
                default:
                    //nothing
                    break;
            }
        }



    }

}
