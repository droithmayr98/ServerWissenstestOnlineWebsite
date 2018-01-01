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
        public IActionResult Login(){
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
            stationsList.Add(new SelectListItem { Text = "keine ausgewählt", Value = "noItemSelected"});
            foreach (Station s in stations)
            {
                SelectListItem stationItem = new SelectListItem { Text = s.Stationsname, Value = s.Station_Id.ToString() };
                stationsList.Add(stationItem);
            }
            adminOverwiew_model.Stationen = stationsList;

            //Admins holen und in Model speichern + Modelübergabe an View
            List<Admintable> alle_admins = test_db.Admins.Select(x => x).OrderBy(x => x.Username).ToList();
            adminOverwiew_model.Admins = alle_admins;

            return View(adminOverwiew_model);

        }

        public IActionResult CreateNewAdmin()
        {
            return View();
        }

        public IActionResult Logout()
        {

            return RedirectToAction("Start", "Main");
        }


        //Ajax calls
        public string CheckAdminInfo(string username, string password) {

            Admintable admin =  test_db.Admins.SingleOrDefault(x => x.Username.Equals(username));
            if (admin == null)
            {
                return "username_fail";
            }
            else if (!admin.Password.Equals(password))
            {
                return "password_fail";
            }
            else {
                return "ok";
            }
        }

        public IActionResult GetAdminInfo(string admin_id) {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminInfo_Modal", adminInfo_model);
        }

        public IActionResult GetAdminEdit(string admin_id) {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminEdit_Modal", adminInfo_model);
        }

        public IActionResult GetAdminDelete(string admin_id) {
            int adminId_int = Convert.ToInt32(admin_id);
            var adminInfo_model = FillAdminModel(adminId_int);
            return PartialView("Modal_PartialViews/AdminDelete_Modal", adminInfo_model);
        }

        public IActionResult GetAufgabeInfo(string aufgabe_id) {
            int aufgabeId_int = Convert.ToInt32(aufgabe_id);
            var aufgabeInfo_model = FillAufgabeModel(aufgabeId_int);
            return PartialView("Modal_PartialViews/AufgabeInfo_Modal", aufgabeInfo_model);
        }

        public AdminInfo_Model FillAdminModel(int adminId_int) {
            
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

        public AufgabeInfo_Model FillAufgabeModel(int aufgabeId_int) {
            Aufgabe aufgabe = test_db.Aufgaben.Single(x => x.Aufgabe_Id == aufgabeId_int);
            var aufgabeInfo_model = new AufgabeInfo_Model();
            aufgabeInfo_model.Aufgabe_Id = aufgabe.Aufgabe_Id;
            aufgabeInfo_model.Frage = aufgabe.Frage.Fragetext;
            return aufgabeInfo_model;
        }



    }
}
