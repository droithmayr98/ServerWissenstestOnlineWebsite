using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Start(){
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
