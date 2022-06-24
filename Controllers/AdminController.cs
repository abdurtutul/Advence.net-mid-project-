﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        readonly Project_databaseEntities db = new Project_databaseEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add_Driver()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add_Driver(Driver driver)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Driver_List");
            }
            return View(driver);
        }

        [HttpGet]
        public ActionResult Driver_List(int id = 0)
        {
            //var param = this.Request.Params["skip"];
            var d = db.Drivers.ToList().Skip(id * 10).Take(10);
            return View(d);
        }

        [HttpGet]
        public ActionResult Update_Driver()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add_Surgeon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add_Surgeon(Surgeon sg)
        {
            if(ModelState.IsValid)
            {
                db.Surgeons.Add(sg);
                db.SaveChanges();
                return RedirectToAction("Surgeon_List");
            }
            return View(sg);
        }

        [HttpGet]
        public ActionResult Surgeon_List(int id=0)
        {
            //var param = this.Request.Params["skip"];
            var s = db.Surgeons.ToList().Skip(id*0).Take(10);
            return View(s);
        }

        [HttpGet]
        public ActionResult Update_Surgeon(int id)
        {
            //var param = this.Request.Params["skip"];
            var s = db.Surgeons.Find(id);
            return View(s);
        }

        [HttpGet]
        public ActionResult Rules()
        {
            return View(db.Offences.ToList());
        }

        [HttpPost]
        public ActionResult Rules(Offence offence)
        {
            if(ModelState.IsValid)
            {
                db.Offences.Add(offence);
                db.SaveChanges();
                return View(db.Offences.ToList());
            }
            return View(offence);
        }

        [HttpPost]
        public ActionResult Update_Rules(Offence offence,int Id)
        {
            if (ModelState.IsValid)
            {
                var data = (from o in db.Offences where o.Id == Id select o).SingleOrDefault();
                data.Offence_name = offence.Offence_name;
                data.Fine = offence.Fine;
                db.SaveChanges();
            }
            return RedirectToAction("Rules");
        }

        [HttpPost]
        public ActionResult Delete_Rules(int Id)
        {
            var data = (from o in db.Offences where o.Id == Id select o).SingleOrDefault();
            db.Offences.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Rules");
        }
    }
}