using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WissenstestOnline.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Test
            return View();
        }

        public IActionResult About()
        {  
            // ABout
            return View();

        }

        public IActionResult Contact()
        {            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
