using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_lib;
using Microsoft.Extensions.Logging;
using DB_lib.Tables;
using WissenstestOnlineWebseite.Models;

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


        public IActionResult Login()
        {
            logger.LogInformation("Testlog Admincontroller");

            return View();
        }

        public IActionResult AdminOverview()
        {

            List<Aufgabe> alle_aufgaben = test_db.Aufgaben.Select(x => x).ToList();
            var adminOverwiew_model = new AdminOverview_Model();
            adminOverwiew_model.Aufgaben = alle_aufgaben;

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





    }
}
