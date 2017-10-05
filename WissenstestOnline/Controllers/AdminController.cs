using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WissenstestOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AdminOverview() {
            return View();
        }

        public IActionResult CreateNewAdmin() {
            return View();
        }

    }
}
