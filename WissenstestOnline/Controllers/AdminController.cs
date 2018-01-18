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
                SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ_Id.ToString() };
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
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ_Id.ToString() };
                    antwortTypItem.Selected = true;
                    antwortTypenList.Add(antwortTypItem);
                }
                else
                {
                    SelectListItem antwortTypItem = new SelectListItem { Text = t.Typ, Value = t.Typ_Id.ToString() };
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







    }
}
