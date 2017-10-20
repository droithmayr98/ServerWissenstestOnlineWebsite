using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_lib;

namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {
        private readonly TestDB_Context test_db = new TestDB_Context();

        public IActionResult Start(){
            var test = test_db.Bezirke.Count();

            return View();
        }

        public IActionResult SelectStation() {
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



    }
}
