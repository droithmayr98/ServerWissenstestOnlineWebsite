﻿using System;
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

namespace WissenstestOnline.Controllers
{
    public class MainController : Controller
    {
        private readonly TestDB_Context test_db;
        private ILogger<MainController> logger;

        public MainController(TestDB_Context db, ILogger<MainController> logger) {
            var migration = new MigrateDatabaseToLatestVersion<TestDB_Context, Configuration>();
            Database.SetInitializer(migration);
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
