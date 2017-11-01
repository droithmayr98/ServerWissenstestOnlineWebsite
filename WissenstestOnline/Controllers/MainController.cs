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

namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {

        /*private string global_bezirk = "";
        private string global_ort = "";
        private string global_stufe = "";
        private string global_mode = "";
        private List<string> global_stations = new List<string>();*/

        private readonly TestDB_Context test_db;
        private ILogger<MainController> logger;

        public MainController(TestDB_Context db, ILogger<MainController> logger) {
            //var migration = new MigrateDatabaseToLatestVersion<TestDB_Context, Configuration>();
            //Database.SetInitializer(migration);
            this.test_db = db;
            this.logger = logger;

        }

        public IActionResult Start(){

            

            //DB_ConnectionTest
            var test = test_db.Bezirke.Count();
            ViewBag.test = test;
            //LoggerTest
            logger.LogInformation("Test Log");

            return View();
        }

        public IActionResult SelectStation() {
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

        public IActionResult AufgabeUmgebungLearn() {
            return View();
        }

        public IActionResult AufgabeUmgebungPractise(){
            return View();
        }

        public IActionResult ErgebnisOverview() {
            return View();
        }

        public IActionResult ZusatzInfo() {
            return View();
        }


        //AllgemeinInfo
        public IActionResult Info() {
            return View();
        }

        public IActionResult Kontakt() {
            return View();
        }


        //Ajax Calls

        public string CheckUserInfo(string bezirk, string ort, string stufe) {

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

        public string GlobalStationData(List<string> stations,string mode) {

            string stationsString = string.Join(",", stations.ToArray());

            UserData.Mode = mode;
            UserData.Stations = stationsString;
            return "success";
        }


        public IActionResult GetGlobalData() {
            var data = new Dictionary<string, string>()
            {
                ["bezirk"] = UserData.Bezirk,
                ["ort"] = UserData.Ort,
                ["stufe"] = UserData.Stufe,
                ["mode"] = UserData.Mode,
                ["stations"] = UserData.Stations
            };
            return Json(data);
        }

    }
}
