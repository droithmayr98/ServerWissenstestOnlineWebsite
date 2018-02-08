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

        private TestDB_Context test_db;
        private ILogger<MainController> logger;

        public AdminController(TestDB_Context db, ILogger<MainController> logger)
        {
            this.test_db = db;
            this.logger = logger;
        }

        //Admin Login Page
        public IActionResult Login()
        {
            logger.LogInformation("Testlog Admincontroller");
            return View();
        }


        public IActionResult AdminOverview()
        {
            //Alle Aufgaben holen und in Model speichern
            List<Aufgabe> alle_aufgaben = test_db.Aufgaben.Select(x => x).OrderBy(x => x.Station.Station_Id).ToList();
            var adminOverwiew_model = new AdminOverview_Model();
            adminOverwiew_model.Aufgaben = alle_aufgaben;

            //Stationen als SelectListItems umwandeln und in Model speichern
            List<Station> stations = test_db.Stationen.OrderBy(x => x.Station_Id).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            stationsList.Add(new SelectListItem { Text = "keine ausgewählt", Value = "noItemSelected" });
            foreach (Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                stationsList.Add(stationItem);
            }
            adminOverwiew_model.Stationen = stationsList;

            //Admins holen und in Model speichern + Modelübergabe an View
            List<Admintable> alle_admins = test_db.Admins.Select(x => x).OrderBy(x => x.Username).ToList();
            adminOverwiew_model.Admins = alle_admins;

            //Aktuellen Admin holen

            return View(adminOverwiew_model);

        }

        public IActionResult AufgabeEditView(int aufgabe_id)
        {
            logger.LogInformation("Selected Aufgabe ID: " + aufgabe_id);

            var aufgabeInfo_edit_Model = FillAufgabeModel(aufgabe_id);


            //Stationen
            List<Station> stations = test_db.Stationen.OrderBy(x => x.Station_Id).ToList();
            List<SelectListItem> stationsList = new List<SelectListItem>();
            foreach (Station s in stations)
            {
                if (s.Stationsname == aufgabeInfo_edit_Model.Station)
                {
                    SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                    stationItem.Selected = true;
                    stationsList.Add(stationItem);
                }
                else
                {
                    SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                    stationsList.Add(stationItem);
                }
            }

            //FFs_1
            List<Ort> standorte_ff = new List<Ort>();
            List<SelectListItem> standorteList = new List<SelectListItem>();
            standorteList.Add(new SelectListItem { Text = "kein Standort ausgewählt", Value = "noStandortSelected" });

            //Bezirke
            List<Bezirk> bezirke = test_db.Bezirke.OrderBy(x => x.Bezirksname).ToList();
            List<SelectListItem> bezirkeList = new List<SelectListItem>();
            bezirkeList.Add(new SelectListItem { Text = "kein Bezirk ausgewählt", Value = "noBezirkSelected" });
            foreach (Bezirk b in bezirke)
            {
                if (b.Bezirksname == aufgabeInfo_edit_Model.Bezirk)
                {
                    SelectListItem bezirkItem = new SelectListItem { Text = b.Bezirksname, Value = b.Bezirksname };
                    bezirkItem.Selected = true;
                    bezirkeList.Add(bezirkItem);
                    standorte_ff = test_db.Orte.Where(x => x.Bezirk.Bezirk_Id == b.Bezirk_Id).ToList();
                }
                else
                {
                    SelectListItem bezirkItem = new SelectListItem { Text = b.Bezirksname, Value = b.Bezirksname };
                    bezirkeList.Add(bezirkItem);
                }
            }

            //FFs_2
            foreach (Ort o in standorte_ff)
            {
                if (o.Ortsname == aufgabeInfo_edit_Model.Ort)
                {
                    SelectListItem standortItem = new SelectListItem { Text = o.Ortsname, Value = o.Ortsname };
                    standortItem.Selected = true;
                    standorteList.Add(standortItem);
                }
                else
                {
                    SelectListItem standortItem = new SelectListItem { Text = o.Ortsname, Value = o.Ortsname };
                    standorteList.Add(standortItem);
                }
            }

            //Fragen
            List<Frage> fragen = test_db.Fragen.OrderBy(x => x.Frage_Id).ToList();

            //Antworten
            List<Antwort> antworten = test_db.Antworten.OrderBy(x => x.Antwort_Id).ToList();

            //Zusatzinfo
            List<Zusatzinfo> infos = test_db.Zusatzinfos.OrderBy(x => x.Zusatzinfo_Id).ToList();

            //AntwortTypen
            List<Typendefinition> antwort_typen = test_db.Typendefinitionen.Where(x => x.Typ.StartsWith("A")).ToList();

            List<SelectListItem> antwort_typen_list = new List<SelectListItem>();
            antwort_typen_list.Add(new SelectListItem { Text = "keinen ausgewählt", Value = "noItemSelected" });
            foreach (Typendefinition t in antwort_typen)
            {
                SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ };
                antwort_typen_list.Add(antwortTypItem);
            }


            //ZusatzinfoTypen
            List<Typendefinition> info_typen = test_db.Typendefinitionen.Where(x => x.Typ.StartsWith("I")).ToList();

            List<SelectListItem> info_typen_list = new List<SelectListItem>();
            info_typen_list.Add(new SelectListItem { Text = "keinen ausgewählt", Value = "noItemSelected" });
            foreach (Typendefinition t in info_typen)
            {
                SelectListItem infoTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ_Id.ToString() };
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



            var aufgabe_edit_main_Model = new AufgabeEditMain_Model();
            aufgabe_edit_main_Model.AufgabeInfo_Model = aufgabeInfo_edit_Model;
            aufgabe_edit_main_Model.AufgabeEditView_Model = aufgabe_view_Model;
            return View(aufgabe_edit_main_Model);
        }

        public IActionResult Logout()
        {

            return RedirectToAction("Start", "Main");
        }


        //Ajax calls
        public string CheckAdminInfo(string username, string password)
        {

            Admintable admin = test_db.Admins.SingleOrDefault(x => x.Username.Equals(username));
            if (admin == null)
            {
                return "username_fail";
            }
            else if (!admin.Password.Equals(password))
            {
                return "password_fail";
            }
            else
            {
                return "ok";
            }
        }

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

        public IActionResult GetAdminDelete(string admin_id)
        {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminModals/AdminDelete_Modal", adminInfo_model);
        }

        public IActionResult GetAufgabeInfo(string aufgabe_id)
        {
            int aufgabeId_int = Convert.ToInt32(aufgabe_id);
            var aufgabeInfo_model = FillAufgabeModel(aufgabeId_int);
            return PartialView("Modal_PartialViews/AufgabeInfo_Modal", aufgabeInfo_model);
        }

        public AdminInfo_Model FillAdminModel(int adminId_int)
        {

            Admintable admin = test_db.Admins.Single(x => x.Admin_Id == adminId_int);
            var adminInfo_model = new AdminInfo_Model();
            adminInfo_model.Id = admin.Admin_Id;
            adminInfo_model.Username = admin.Username;
            adminInfo_model.Password = admin.Password;
            adminInfo_model.Can_create_acc = admin.Can_create_acc;
            adminInfo_model.Can_edit_acc = admin.Can_edit_acc;
            adminInfo_model.Can_delete_acc = admin.Can_delete_acc;
            return adminInfo_model;
        }

        public AufgabeInfo_Model FillAufgabeModel(int aufgabeId_int)
        {
            Aufgabe aufgabe = test_db.Aufgaben.Single(x => x.Aufgabe_Id == aufgabeId_int);
            var aufgabeInfo_model = new AufgabeInfo_Model();
            aufgabeInfo_model.Aufgabe_Id = aufgabe.Aufgabe_Id;
            aufgabeInfo_model.Station = aufgabe.Station.Stationsname;
            aufgabeInfo_model.Stufe = aufgabe.Stufe.Stufenname;
            aufgabeInfo_model.IsPflichtaufgabe = aufgabe.Pflichtaufgabe;
            aufgabeInfo_model.Bezirk = aufgabe.AufgabeBezirk == null ? "-" : aufgabe.AufgabeBezirk;
            aufgabeInfo_model.Ort = aufgabe.AufgabeOrt == null ? "-" : aufgabe.AufgabeOrt;
            aufgabeInfo_model.TeilAufgabeVon = aufgabe.TeilaufgabeVon == null ? "-" : aufgabe.TeilaufgabeVon.Aufgabe_Id.ToString();
            aufgabeInfo_model.Antwort_Id = aufgabe.Antwort.Antwort_Id;
            aufgabeInfo_model.Antwort_Name = aufgabe.Antwort.Antwort_Name;

            aufgabeInfo_model.Frage = aufgabe.Frage;

            aufgabeInfo_model.Info = aufgabe.Zusatzinfo.InfoContentM;
            aufgabeInfo_model.Zusatzinfo = aufgabe.Zusatzinfo;


            //Antwort
            string aufgabe_typ = aufgabe.Antwort.Typ.Typ;
            int antwortInhalt_id = aufgabe.Antwort.Inhalt_Id;

            switch (aufgabe_typ)
            {
                case "A_T":
                    aufgabeInfo_model.Antworttyp = "Text";
                    aufgabeInfo_model.Antwort_Text = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_S":
                    aufgabeInfo_model.Antworttyp = "Slider";
                    aufgabeInfo_model.Antwort_Slider = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_DP":
                    aufgabeInfo_model.Antworttyp = "DatePicker";
                    aufgabeInfo_model.Antwort_DatePicker = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_CB:T":
                    aufgabeInfo_model.Antworttyp = "CheckBox";
                    aufgabeInfo_model.Antwort_CheckBoxes = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == antwortInhalt_id).CheckBoxes;
                    break;
                case "A_RB:T":
                    aufgabeInfo_model.Antworttyp = "RadioButton";
                    aufgabeInfo_model.Antwort_RadioButtons = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == antwortInhalt_id).RadioButtons;
                    break;
            }

            return aufgabeInfo_model;
        }

        public string SaveAdminChanges(int admin_id, string username, string password, bool can_create_acc, bool can_edit_acc, bool can_delete_acc)
        {
            //Datenbankänderungen u Überprüfungen
            var edit_admin = test_db.Admins.Single(x => x.Admin_Id == admin_id);
            edit_admin.Username = username;
            edit_admin.Password = password;
            edit_admin.Can_create_acc = can_create_acc;
            edit_admin.Can_edit_acc = can_edit_acc;
            edit_admin.Can_delete_acc = can_delete_acc;
            test_db.SaveChanges();
            return "ok";
        }

        public string DeleteAdmin(int admin_id)
        {
            //Datensatz löschen
            var delete_admin = test_db.Admins.Single(x => x.Admin_Id == admin_id);
            test_db.Admins.Remove(delete_admin);
            test_db.SaveChanges();
            return "ok";
        }

        public string CreateAdmin(string username, string password, bool can_create_acc, bool can_edit_acc, bool can_delete_acc)
        {
            Admintable new_admin = new Admintable();
            new_admin.Username = username;
            new_admin.Password = password;
            new_admin.Can_create_acc = can_create_acc;
            new_admin.Can_edit_acc = can_edit_acc;
            new_admin.Can_delete_acc = can_delete_acc;
            test_db.Admins.Add(new_admin);
            test_db.SaveChanges();
            return "ok";
        }

        public IActionResult SetStandorteBezirkComboBox(string bezirk)
        {

            List<Ort> standorte_ff = test_db.Orte.Where(x => x.Bezirk.Bezirksname.Equals(bezirk)).ToList();

            List<SelectListItem> standorteList = new List<SelectListItem>();
            standorteList.Add(new SelectListItem { Text = "kein Standort ausgewählt", Value = "noStandortSelected" });

            foreach (Ort o in standorte_ff)
            {
                SelectListItem standortItem = new SelectListItem { Text = o.Ortsname, Value = o.Ortsname };
                standorteList.Add(standortItem);
            }

            var newComboBoxValues_Model = new NewComboBoxValues_Model();
            newComboBoxValues_Model.New_values = standorteList;

            return PartialView("NewComboBoxValues", newComboBoxValues_Model);
        }

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

        public FrageInfo_Model FillFrageInfoModel(int frage_id)
        {
            Frage frage = test_db.Fragen.Single(x => x.Frage_Id == frage_id);
            var frageInfo_Model = new FrageInfo_Model();
            frageInfo_Model.FrageId = frage.Frage_Id;
            frageInfo_Model.FrageText = frage.Fragetext;
            return frageInfo_Model;
        }

        public string SaveFrageChanges(int frage_id, string fragetext)
        {

            Frage edit_frage = test_db.Fragen.Single(x => x.Frage_Id == frage_id);
            edit_frage.Fragetext = fragetext;
            edit_frage.Typ = edit_frage.Typ;

            try
            {
                test_db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


            return "ok";
        }

        public string CreateFrage(string fragetext)
        {

            Frage new_frage = new Frage();
            new_frage.Fragetext = fragetext;
            new_frage.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals("F_T"));

            test_db.Fragen.Add(new_frage);
            test_db.SaveChanges();

            return "ok";
        }

        public IActionResult GetFrageDelete(int frage_id)
        {
            var frageDelete_Model = FillFrageInfoModel(frage_id);
            return PartialView("Modal_PartialViews/FrageModals/FrageDelete_Modal", frageDelete_Model);
        }

        public string DeleteFrage(int frage_id)
        {
            Frage frage_delete = test_db.Fragen.Single(x => x.Frage_Id == frage_id);
            if (frage_delete.Aufgaben.Count == 0)
            {
                test_db.Fragen.Remove(frage_delete);
                test_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }

        }

        public IActionResult GetAntwortInfo(int antwort_id)
        {
            var antwortInfo_Model = FillAntwortModel(antwort_id);
            return PartialView("Modal_PartialViews/AntwortInfo_Modal", antwortInfo_Model);
        }

        public IActionResult GetAntwortEdit(int antwort_id)
        {
            var antwortEditMain_Model = new AntwortEditMain_Model();
            var antwortEdit_Model = FillAntwortModel(antwort_id);

            List<Typendefinition> antwortTypen = test_db.Typendefinitionen.Where(x => x.Typ.StartsWith("A_")).ToList<Typendefinition>();
            List<SelectListItem> antwortTypenList = new List<SelectListItem>();
            foreach (Typendefinition t in antwortTypen)
            {
                string typ_string = "";
                switch (t.Typ)
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
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ };
                    antwortTypItem.Selected = true;
                    antwortTypenList.Add(antwortTypItem);
                }
                else
                {
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ };
                    antwortTypenList.Add(antwortTypItem);
                }

            }
            var antwortEditView_Model = new AntwortEditView_Model();
            antwortEditView_Model.AntwortTypen = antwortTypenList;

            antwortEditMain_Model.AntwortInfo_Model = antwortEdit_Model;
            antwortEditMain_Model.AntwortEditView_Model = antwortEditView_Model;

            return PartialView("Modal_PartialViews/AntwortEdit_Modal", antwortEditMain_Model);
        }

        public AntwortInfo_Model FillAntwortModel(int antwort_id)
        {
            Antwort antwort = test_db.Antworten.Single(x => x.Antwort_Id == antwort_id);
            var antwortInfo_Model = new AntwortInfo_Model();
            antwortInfo_Model.Antwort_Id = antwort.Antwort_Id;
            antwortInfo_Model.Antwortname = antwort.Antwort_Name;
            int antwortInhalt_id = antwort.Inhalt_Id;

            switch (antwort.Typ.Typ)
            {
                case "A_T":
                    antwortInfo_Model.Antworttyp = "Text";
                    antwortInfo_Model.Antwort_Text = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_S":
                    antwortInfo_Model.Antworttyp = "Slider";
                    antwortInfo_Model.Antwort_Slider = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_DP":
                    antwortInfo_Model.Antworttyp = "DatePicker";
                    antwortInfo_Model.Antwort_DatePicker = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == antwortInhalt_id);
                    break;
                case "A_CB:T":
                    antwortInfo_Model.Antworttyp = "CheckBox";
                    antwortInfo_Model.Antwort_CheckBoxes = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == antwortInhalt_id).CheckBoxes;
                    break;
                case "A_RB:T":
                    antwortInfo_Model.Antworttyp = "RadioButton";
                    antwortInfo_Model.Antwort_RadioButtons = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == antwortInhalt_id).RadioButtons;
                    break;
            }

            return antwortInfo_Model;
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
            Antwort antwort_delete = test_db.Antworten.Single(x => x.Antwort_Id == antwort_id);
            if (antwort_delete.Aufgaben.Count == 0)
            {
                string a_typ = antwort_delete.Typ.Typ;
                int inhalt_id = antwort_delete.Inhalt_Id;
                switch (a_typ)
                {
                    case "A_T":
                        Antwort_Text antwort_Text_delete = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == inhalt_id);
                        test_db.Antwort_Texte.Remove(antwort_Text_delete);
                        break;
                    case "A_S":
                        Antwort_Slider antwort_Slider_delete = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == inhalt_id);
                        test_db.Antwort_Sliders.Remove(antwort_Slider_delete);
                        break;
                    case "A_DP":
                        Antwort_DatePicker antwort_DP_delete = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == inhalt_id);
                        test_db.Antwort_DatePickerM.Remove(antwort_DP_delete);
                        break;
                    case "A_RB:T":
                        Antwort_RadioButton antwort_RB_delete = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == inhalt_id);
                        int rb_inhalt_id = antwort_RB_delete.Inhalt_Id;
                        RadioButton[] radioButtons_delete = test_db.RadioButtons.Where(x => x.Antwort_RadioButton.Inhalt_Id == rb_inhalt_id).ToArray();
                        foreach (RadioButton rb_delete in radioButtons_delete)
                        {
                            test_db.RadioButtons.Remove(rb_delete);
                        }
                        test_db.Antwort_RadioButtons.Remove(antwort_RB_delete);
                        break;
                    case "A_CB:T":
                        Antwort_CheckBox antwort_CB_delete = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == inhalt_id);
                        int cb_inhalt_id = antwort_CB_delete.Inhalt_Id;
                        CheckBox[] checkBoxes_delete = test_db.CheckBoxes.Where(x => x.Antwort_CheckBox.Inhalt_Id == cb_inhalt_id).ToArray();
                        foreach (CheckBox cb_delete in checkBoxes_delete)
                        {
                            test_db.CheckBoxes.Remove(cb_delete);
                        }
                        test_db.Antwort_CheckBoxes.Remove(antwort_CB_delete);
                        break;
                    default:
                        //nothing
                        break;
                }
                test_db.Antworten.Remove(antwort_delete);
                test_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }
        }

        public IActionResult GetAufgabeDelete(int aufgabe_id)
        {
            var aufgabeDelete_Model = FillAufgabeModel(aufgabe_id);
            return PartialView("Modal_PartialViews/AufgabeModals/AufgabeDelete_Modal", aufgabeDelete_Model);
        }

        public string DeleteAufgabe(int aufgabe_id)
        {
            Aufgabe aufgabe_delete = test_db.Aufgaben.Single(x => x.Aufgabe_Id == aufgabe_id);
            test_db.Aufgaben.Remove(aufgabe_delete);
            test_db.SaveChanges();
            return "ok";
        }

        public IActionResult SetNewAntwortType(string typ_id)
        {
            Console.WriteLine("Selected AntwortType: " + typ_id);

            //string typ_string = test_db.Typendefinitionen.Single(x => x.Typ_Id.ToString().Equals(typ_id)).Typ;

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

        public ZusatzInfo_InfoModel FillInfoModel(int info_id)
        {

            Zusatzinfo zusatzInfo = test_db.Zusatzinfos.Single(x => x.Zusatzinfo_Id == info_id);
            InfoContent[] infoContents = test_db.InfoContentM.Where(x => x.Zusatzinfo.Zusatzinfo_Id == info_id).ToArray();
            List<ZusatzInfoTextblock> infoTextblocks = new List<ZusatzInfoTextblock>();

            ZusatzInfoTextOnly_Model zusatzInfo_Model = new ZusatzInfoTextOnly_Model();
            foreach (InfoContent infoCon in infoContents)
            {
                ZusatzInfoTextblock textBlock = new ZusatzInfoTextblock();
                textBlock.Heading = infoCon.Heading;
                textBlock.Text = infoCon.Info_Content;
                infoTextblocks.Add(textBlock);
            }

            var zusatzInfo_InfoModel = new ZusatzInfo_InfoModel();
            zusatzInfo_InfoModel.Texte = infoTextblocks;
            zusatzInfo_InfoModel.ZusatzInfo_Id = zusatzInfo.Zusatzinfo_Id;
            zusatzInfo_InfoModel.ZusatzInfo_Typ = zusatzInfo.Typ.Typ;
            zusatzInfo_InfoModel.ZusatzInfo_Name = zusatzInfo.Zusatzinfo_Name;

            return zusatzInfo_InfoModel;

        }

        public string DeleteZusatzInfo(int info_id)
        {
            Zusatzinfo info_delete = test_db.Zusatzinfos.Single(x => x.Zusatzinfo_Id == info_id);
            if (info_delete.Aufgaben.Count == 0)
            {
                InfoContent[] infoContentDel = info_delete.InfoContentM.ToArray();
                foreach (InfoContent infC in infoContentDel)
                {
                    test_db.InfoContentM.Remove(infC);
                }
                test_db.Zusatzinfos.Remove(info_delete);
                test_db.SaveChanges();
                return "ok";
            }
            else
            {
                return "not deletable";
            }
        }

        //neue Antworten erstellen
        public string CreateAntwort_Text(string antwortName, string antwortTyp, string antwortText)
        {
            //Console.WriteLine("AntwortTyp: " + antwortTyp);
            //Console.WriteLine("AntwortName: " + antwortName);

            Antwort new_antwort = new Antwort();
            Antwort_Text new_antwortText = new Antwort_Text();

            new_antwortText.Text = antwortText;
            test_db.Antwort_Texte.Add(new_antwortText);
            test_db.SaveChanges();

            new_antwort.Antwort_Name = antwortName;
            new_antwort.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            new_antwort.Inhalt_Id = new_antwortText.Inhalt_Id;
            new_antwort.Aufgaben = new List<Aufgabe>();
            test_db.Antworten.Add(new_antwort);
            test_db.SaveChanges();

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
            Antwort new_antwort = new Antwort();
            Antwort_Slider new_antwortSlider = new Antwort_Slider();

            new_antwortSlider.Min_val = sliderMin;
            new_antwortSlider.Max_val = sliderMax;
            new_antwortSlider.Sprungweite = sliderSprungweite;
            new_antwortSlider.RightVal = sliderRightVal;
            if (sliderTitel == "")
            {
                new_antwortSlider.Slider_text = null;
            }
            else
            {
                new_antwortSlider.Slider_text = sliderTitel;
            }

            test_db.Antwort_Sliders.Add(new_antwortSlider);
            test_db.SaveChanges();

            new_antwort.Antwort_Name = antwortName;
            new_antwort.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            new_antwort.Inhalt_Id = new_antwortSlider.Inhalt_Id;
            new_antwort.Aufgaben = new List<Aufgabe>();
            test_db.Antworten.Add(new_antwort);
            test_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_DP(string antwortName, string antwortTyp, string date)
        {

            Antwort new_antwort = new Antwort();
            Antwort_DatePicker new_antwortDP = new Antwort_DatePicker();

            DateTime date_formated = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            new_antwortDP.Date = date_formated;
            test_db.Antwort_DatePickerM.Add(new_antwortDP);
            test_db.SaveChanges();

            new_antwort.Antwort_Name = antwortName;
            new_antwort.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            new_antwort.Inhalt_Id = new_antwortDP.Inhalt_Id;
            new_antwort.Aufgaben = new List<Aufgabe>();
            test_db.Antworten.Add(new_antwort);
            test_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_CB(string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {
            Antwort new_antwort = new Antwort();
            Antwort_CheckBox new_antwortCB = new Antwort_CheckBox();

            new_antwortCB.Anzahl = rightOptions.Length + wrongOptions.Length;
            new_antwortCB.CheckBoxes = new List<CheckBox>();
            test_db.Antwort_CheckBoxes.Add(new_antwortCB);
            test_db.SaveChanges();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    CheckBox cb_right = new CheckBox();
                    cb_right.Content = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_CheckBox = new_antwortCB;
                    test_db.CheckBoxes.Add(cb_right);
                    test_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    CheckBox cb_wrong = new CheckBox();
                    cb_wrong.Content = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_CheckBox = new_antwortCB;
                    test_db.CheckBoxes.Add(cb_wrong);
                    test_db.SaveChanges();
                }
            }

            new_antwort.Antwort_Name = antwortName;
            new_antwort.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            new_antwort.Inhalt_Id = new_antwortCB.Inhalt_Id;
            new_antwort.Aufgaben = new List<Aufgabe>();
            test_db.Antworten.Add(new_antwort);
            test_db.SaveChanges();

            return "ok";
        }

        public string CreateAntwort_RB(string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            Antwort new_antwort = new Antwort();
            Antwort_RadioButton new_antwortRB = new Antwort_RadioButton();

            new_antwortRB.Anzahl = 1 + wrongOptions.Length;
            new_antwortRB.RadioButtons = new List<RadioButton>();
            test_db.Antwort_RadioButtons.Add(new_antwortRB);
            test_db.SaveChanges();

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    RadioButton rb_wrong = new RadioButton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.IsTrue = false;
                    rb_wrong.Antwort_RadioButton = new_antwortRB;
                    test_db.RadioButtons.Add(rb_wrong);
                    test_db.SaveChanges();
                }
            }

            RadioButton rb_right = new RadioButton();
            rb_right.Content = rightOption;
            rb_right.IsTrue = true;
            rb_right.Antwort_RadioButton = new_antwortRB;
            test_db.RadioButtons.Add(rb_right);
            test_db.SaveChanges();

            new_antwort.Antwort_Name = antwortName;
            new_antwort.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            new_antwort.Inhalt_Id = new_antwortRB.Inhalt_Id;
            new_antwort.Aufgaben = new List<Aufgabe>();
            test_db.Antworten.Add(new_antwort);
            test_db.SaveChanges();

            return "ok";
        }

        public string GetTypIdFromTyp(string typ)
        {
            if (typ.Equals("noItemSelected"))
            {
                return "noItemSelected";
            }
            else
            {
                return test_db.Typendefinitionen.Single(x => x.Typ.Equals(typ)).Typ_Id.ToString();
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        //bestehende Antwort bearbeiten
        //Textfeld + Text auf bestehende Antwort einstellen
        public string EditNewAntwort_Text(int antwortId, string antwortName, string antwortTyp, string antwortText)
        {
            Antwort antwort_editNew = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            string a_typ = antwort_editNew.Typ.Typ;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.Inhalt_Id);

            Antwort_Text new_antwortText = new Antwort_Text();
            new_antwortText.Text = antwortText;
            test_db.Antwort_Texte.Add(new_antwortText);
            test_db.SaveChanges();

            antwort_editNew.Antwort_Name = antwortName;
            antwort_editNew.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            antwort_editNew.Inhalt_Id = new_antwortText.Inhalt_Id;
            test_db.SaveChanges();

            return "ok";
        }

        //Text des Textfeldes auf bestehende Antwort ändern
        public string EditAntwort_Text(int antwortId, string antwortName, string antwortTyp, string antwortText)
        {
            Antwort antwort_edit = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            Antwort_Text antwort_text = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == antwort_edit.Inhalt_Id);
            antwort_text.Text = antwortText;
            antwort_edit.Antwort_Name = antwortName;
            test_db.SaveChanges();

            return "ok";
        }

        //Slider auf bestehende Antwort einstellen
        public string EditNewAntwort_Slider(int antwortId, string antwortName, string antwortTyp, int sliderMin,
                int sliderMax,
                int sliderSprungweite,
                int sliderRightVal,
                string sliderTitel)
        {

            Antwort antwort_editNew = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            string a_typ = antwort_editNew.Typ.Typ;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.Inhalt_Id);

            Antwort_Slider new_antwortSlider = new Antwort_Slider();
            new_antwortSlider.Min_val = sliderMin;
            new_antwortSlider.Max_val = sliderMax;
            new_antwortSlider.Sprungweite = sliderSprungweite;
            new_antwortSlider.RightVal = sliderRightVal;
            if (sliderTitel == "")
            {
                new_antwortSlider.Slider_text = null;
            }
            else
            {
                new_antwortSlider.Slider_text = sliderTitel;
            }
            test_db.Antwort_Sliders.Add(new_antwortSlider);
            test_db.SaveChanges();

            antwort_editNew.Antwort_Name = antwortName;
            antwort_editNew.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            antwort_editNew.Inhalt_Id = new_antwortSlider.Inhalt_Id;
            test_db.SaveChanges();

            return "ok";
        }

        //Sliderwerte auf bestehende Antwort ändern
        public string EditAntwort_Slider(int antwortId, string antwortName, string antwortTyp, int sliderMin,
                int sliderMax,
                int sliderSprungweite,
                int sliderRightVal,
                string sliderTitel)
        {

            Antwort antwort_edit = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            Antwort_Slider antwort_slider = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == antwort_edit.Inhalt_Id);
            antwort_slider.Min_val = sliderMin;
            antwort_slider.Max_val = sliderMax;
            antwort_slider.Sprungweite = sliderSprungweite;
            antwort_slider.RightVal = sliderRightVal;
            if (sliderTitel == "")
            {
                antwort_slider.Slider_text = null;
            }
            else
            {
                antwort_slider.Slider_text = sliderTitel;
            }
            antwort_edit.Antwort_Name = antwortName;
            test_db.SaveChanges();

            return "ok";
        }

        //DP auf bestehende Antwort einstellen
        public string EditNewAntwort_DP(int antwortId, string antwortName, string antwortTyp, string date)
        {
            Antwort antwort_editNew = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            string a_typ = antwort_editNew.Typ.Typ;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.Inhalt_Id);

            Antwort_DatePicker new_antwortDP = new Antwort_DatePicker();
            new_antwortDP.Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            test_db.Antwort_DatePickerM.Add(new_antwortDP);
            test_db.SaveChanges();

            antwort_editNew.Antwort_Name = antwortName;
            antwort_editNew.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            antwort_editNew.Inhalt_Id = new_antwortDP.Inhalt_Id;
            test_db.SaveChanges();

            return "ok";
        }

        //DP-Datum auf bestehende Antwort ändern
        public string EditAntwort_DP(int antwortId, string antwortName, string antwortTyp, string date)
        {
            Antwort antwort_edit = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            Antwort_DatePicker antwort_DP = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == antwort_edit.Inhalt_Id);
            antwort_DP.Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            antwort_edit.Antwort_Name = antwortName;
            test_db.SaveChanges();

            return "ok";
        }

        //CB-Antwortart auf bestehende Antwort einstellen
        public string EditNewAntwort_CB(int antwortId, string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {

            Antwort antwort_editNew = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            string a_typ = antwort_editNew.Typ.Typ;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.Inhalt_Id);

            Antwort_CheckBox new_antwortCB = new Antwort_CheckBox();
            new_antwortCB.Anzahl = rightOptions.Length + wrongOptions.Length;
            new_antwortCB.CheckBoxes = new List<CheckBox>();
            test_db.Antwort_CheckBoxes.Add(new_antwortCB);
            test_db.SaveChanges();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    CheckBox cb_right = new CheckBox();
                    cb_right.Content = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_CheckBox = new_antwortCB;
                    test_db.CheckBoxes.Add(cb_right);
                    test_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    CheckBox cb_wrong = new CheckBox();
                    cb_wrong.Content = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_CheckBox = new_antwortCB;
                    test_db.CheckBoxes.Add(cb_wrong);
                    test_db.SaveChanges();
                }
            }

            antwort_editNew.Antwort_Name = antwortName;
            antwort_editNew.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            antwort_editNew.Inhalt_Id = new_antwortCB.Inhalt_Id;
            test_db.SaveChanges();

            return "ok";
        }

        //CBs auf bestehende Antwort ändern
        public string EditAntwort_CB(int antwortId, string antwortName, string antwortTyp, string[] rightOptions, string[] wrongOptions)
        {
            Antwort antwort_edit = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            Antwort_CheckBox antwort_CB = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == antwort_edit.Inhalt_Id);
            antwort_CB.Anzahl = rightOptions.Length + wrongOptions.Length;

            List<CheckBox> checkBoxesOld = antwort_CB.CheckBoxes;
            for (int i = checkBoxesOld.Count - 1; i >= 0; i--)
            {
                test_db.CheckBoxes.Remove(checkBoxesOld[i]);
            }
            test_db.SaveChanges();

            antwort_CB.CheckBoxes = new List<CheckBox>();

            foreach (string right_option in rightOptions)
            {
                if (right_option != "")
                {
                    CheckBox cb_right = new CheckBox();
                    cb_right.Content = right_option;
                    cb_right.CheckBoxVal = true;
                    cb_right.Antwort_CheckBox = antwort_CB;
                    test_db.CheckBoxes.Add(cb_right);
                    test_db.SaveChanges();
                }
            }

            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    CheckBox cb_wrong = new CheckBox();
                    cb_wrong.Content = wrong_option;
                    cb_wrong.CheckBoxVal = false;
                    cb_wrong.Antwort_CheckBox = antwort_CB;
                    test_db.CheckBoxes.Add(cb_wrong);
                    test_db.SaveChanges();
                }
            }

            antwort_edit.Antwort_Name = antwortName;
            test_db.SaveChanges();

            return "ok";
        }

        //RB-Antwortart auf bestehende Antwort einstellen
        public string EditNewAntwort_RB(int antwortId, string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            Antwort antwort_editNew = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            string a_typ = antwort_editNew.Typ.Typ;

            DeleteOldAntwortDefinition(a_typ, antwort_editNew.Inhalt_Id);

            Antwort_RadioButton new_antwortRB = new Antwort_RadioButton();
            new_antwortRB.Anzahl = 1 + wrongOptions.Length;
            new_antwortRB.RadioButtons = new List<RadioButton>();
            test_db.Antwort_RadioButtons.Add(new_antwortRB);
            test_db.SaveChanges();

            if (rightOption != "")
            {
                RadioButton rb_right = new RadioButton();
                rb_right.Content = rightOption;
                rb_right.IsTrue = true;
                rb_right.Antwort_RadioButton = new_antwortRB;
                test_db.RadioButtons.Add(rb_right);
                test_db.SaveChanges();
            }


            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    RadioButton rb_wrong = new RadioButton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.IsTrue = false;
                    rb_wrong.Antwort_RadioButton = new_antwortRB;
                    test_db.RadioButtons.Add(rb_wrong);
                    test_db.SaveChanges();
                }
            }

            antwort_editNew.Antwort_Name = antwortName;
            antwort_editNew.Typ = test_db.Typendefinitionen.Single(x => x.Typ.Equals(antwortTyp));
            antwort_editNew.Inhalt_Id = new_antwortRB.Inhalt_Id;
            test_db.SaveChanges();

            return "ok";
        }

        //RBs auf bestehende Antwort ändern
        public string EditAntwort_RB(int antwortId, string antwortName, string antwortTyp, string rightOption, string[] wrongOptions)
        {
            Antwort antwort_edit = test_db.Antworten.Single(x => x.Antwort_Id == antwortId);
            Antwort_RadioButton antwort_RB = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == antwort_edit.Inhalt_Id);
            antwort_RB.Anzahl = 1 + wrongOptions.Length;

            List<RadioButton> radioButtonsOld = antwort_RB.RadioButtons;
            for (int i = radioButtonsOld.Count - 1; i >= 0; i--)
            {
                test_db.RadioButtons.Remove(radioButtonsOld[i]);
            }
            test_db.SaveChanges();

            antwort_RB.RadioButtons = new List<RadioButton>();

            if (rightOption != "")
            {
                RadioButton rb_right = new RadioButton();
                rb_right.Content = rightOption;
                rb_right.IsTrue = true;
                rb_right.Antwort_RadioButton = antwort_RB;
                test_db.RadioButtons.Add(rb_right);
                test_db.SaveChanges();
            }
            foreach (string wrong_option in wrongOptions)
            {
                if (wrong_option != "")
                {
                    RadioButton rb_wrong = new RadioButton();
                    rb_wrong.Content = wrong_option;
                    rb_wrong.IsTrue = false;
                    rb_wrong.Antwort_RadioButton = antwort_RB;
                    test_db.RadioButtons.Add(rb_wrong);
                    test_db.SaveChanges();
                }
            }

            antwort_edit.Antwort_Name = antwortName;
            test_db.SaveChanges();

            return "ok";
        }

        //Alte Antwortdefinition löschen
        public void DeleteOldAntwortDefinition(string a_typ, int inhalt_Id)
        {
            switch (a_typ)
            {
                case "A_T":
                    Antwort_Text a_text = test_db.Antwort_Texte.Single(x => x.Inhalt_Id == inhalt_Id);
                    test_db.Antwort_Texte.Remove(a_text);
                    test_db.SaveChanges();
                    break;
                case "A_S":
                    Antwort_Slider a_slider = test_db.Antwort_Sliders.Single(x => x.Inhalt_Id == inhalt_Id);
                    test_db.Antwort_Sliders.Remove(a_slider);
                    test_db.SaveChanges();
                    break;
                case "A_DP":
                    Antwort_DatePicker a_DP = test_db.Antwort_DatePickerM.Single(x => x.Inhalt_Id == inhalt_Id);
                    test_db.Antwort_DatePickerM.Remove(a_DP);
                    test_db.SaveChanges();
                    break;
                case "A_RB:T":
                    Antwort_RadioButton a_RB = test_db.Antwort_RadioButtons.Single(x => x.Inhalt_Id == inhalt_Id);
                    List<RadioButton> radioButtons = a_RB.RadioButtons;
                    foreach (RadioButton rb in radioButtons)
                    {
                        test_db.RadioButtons.Remove(rb);
                        test_db.SaveChanges();
                    }
                    test_db.Antwort_RadioButtons.Remove(a_RB);
                    test_db.SaveChanges();
                    break;
                case "A_CB:T":
                    Antwort_CheckBox a_CB = test_db.Antwort_CheckBoxes.Single(x => x.Inhalt_Id == inhalt_Id);
                    List<CheckBox> checkboxes = a_CB.CheckBoxes;
                    foreach (CheckBox cb in checkboxes)
                    {
                        test_db.CheckBoxes.Remove(cb);
                        test_db.SaveChanges();
                    }
                    test_db.Antwort_CheckBoxes.Remove(a_CB);
                    test_db.SaveChanges();
                    break;
                default:
                    //nothing
                    break;
            }
        }

        public IActionResult GetZusatzInfo_Edit(int info_id) {
            var editZusatzInfo_Model = FillInfoModel(info_id);
            return PartialView("Modal_PartialViews/ZusatzInfoModals/ZusatzInfoEdit_Modal", editZusatzInfo_Model);
        }

        public string CreateNewZusatzInfo(string zusatzInfoName, string[] headings, string[] contents) {

            string type_generated = "I_";

            for (int i = 0; i < contents.Length; i++) {
                type_generated = type_generated + "T";              
            }

            Typendefinition typ = test_db.Typendefinitionen.SingleOrDefault(x => x.Typ.Equals(type_generated));
            if (typ == null) {
                typ = new Typendefinition();
                typ.Typ = type_generated;
                typ.Zusatzinfos = new List<Zusatzinfo>();
                typ.Antworten = new List<Antwort>();
                typ.Fragen = new List<Frage>();
                test_db.Typendefinitionen.Add(typ);
                test_db.SaveChanges();
            }

            Zusatzinfo new_zusatzInfo = new Zusatzinfo();
            new_zusatzInfo.Aufgaben = new List<Aufgabe>();
            new_zusatzInfo.InfoContentM = new List<InfoContent>();
            new_zusatzInfo.Zusatzinfo_Name = zusatzInfoName;
            new_zusatzInfo.Typ = typ;

            test_db.Zusatzinfos.Add(new_zusatzInfo);
            test_db.SaveChanges();

            for (int i = 0; i < contents.Length; i++)
            {
                if (headings[i] == null)
                {
                    InfoContent ic = new InfoContent();
                    ic.Zusatzinfo = new_zusatzInfo;
                    ic.Info_Content = contents[i];
                    test_db.InfoContentM.Add(ic);
                    test_db.SaveChanges();
                }
                else
                {
                    InfoContent ic = new InfoContent();
                    ic.Zusatzinfo = new_zusatzInfo;
                    ic.Heading = headings[i];
                    ic.Info_Content = contents[i];
                    test_db.InfoContentM.Add(ic);
                    test_db.SaveChanges();
                }

            }


            return "ok";
        }

        public string EditZusatzInfo(int zusatzInfoId, string zusatzInfoName, string[] headings, string[] contents) {

            string type_generated = "I_";

            for (int i = 0; i < contents.Length; i++)
            {
                type_generated = type_generated + "T";
            }

            Typendefinition typ = test_db.Typendefinitionen.SingleOrDefault(x => x.Typ.Equals(type_generated));
            if (typ == null)
            {
                typ = new Typendefinition();
                typ.Typ = type_generated;
                typ.Zusatzinfos = new List<Zusatzinfo>();
                typ.Antworten = new List<Antwort>();
                typ.Fragen = new List<Frage>();
                test_db.Typendefinitionen.Add(typ);
                test_db.SaveChanges();
            }

            Zusatzinfo edit_zusatzinfo = test_db.Zusatzinfos.Single(x => x.Zusatzinfo_Id == zusatzInfoId);

            List<InfoContent> old_infoC = edit_zusatzinfo.InfoContentM;
            for (int i = 0; i < old_infoC.Count; i++) {
                test_db.InfoContentM.Remove(old_infoC[i]);
                test_db.SaveChanges();
            }

            edit_zusatzinfo.Zusatzinfo_Name = zusatzInfoName;
            edit_zusatzinfo.Typ = typ;

            test_db.SaveChanges();

            for (int i = 0; i < contents.Length; i++)
            {
                if (headings[i] == null)
                {
                    InfoContent ic = new InfoContent();
                    ic.Zusatzinfo = edit_zusatzinfo;
                    ic.Info_Content = contents[i];
                    test_db.InfoContentM.Add(ic);
                    test_db.SaveChanges();
                }
                else
                {
                    InfoContent ic = new InfoContent();
                    ic.Zusatzinfo = edit_zusatzinfo;
                    ic.Heading = headings[i];
                    ic.Info_Content = contents[i];
                    test_db.InfoContentM.Add(ic);
                    test_db.SaveChanges();
                }

            }

            return "ok";
        }

    }
}
