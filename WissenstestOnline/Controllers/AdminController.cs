using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WissenstestOnline.Controllers
{
    public class AdminController : Controller     
    {
        [HttpPost]
        public IActionResult Login(Models.Admin admin)
        {
            
            if (ModelState.IsValid)
            {
               if( admin.IsValid(admin.UserName, admin.Password))
                {
                    return RedirectToAction("AdminOverview", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

          return View(admin);
        }

        public IActionResult AdminOverview() {
            return View();
        }

        public IActionResult CreateNewAdmin() {
            return View();
        }
        
        public IActionResult Logout()
        {
            
            return RedirectToAction("Start", "Main");
        }

    }
}
