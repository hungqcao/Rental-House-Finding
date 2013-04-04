using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers.Admin
{ 
    public class ManageScoreConfigController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageScoreConfig/

        public ViewResult Index()
        {
            return View(_db.ConfigurationRHFs.ToList());
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<ConfigurationRHF> configurationrhf)
        {
            if (ModelState.IsValid)
                {
                    foreach (var config in configurationrhf)
                    {
                        _db.ConfigurationRHFs.Attach(config);
                        _db.ObjectStateManager.ChangeObjectState(config, EntityState.Modified);
                        _db.SaveChanges();
                    }
                return RedirectToAction("Index");
            }
            return View(configurationrhf);
        }

        //
        // GET: /ManageScoreConfig/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageScoreConfig/Create

        [HttpPost]
        public ActionResult Create(ConfigurationRHF configurationrhf)
        {
            if (ModelState.IsValid)
            {
                _db.ConfigurationRHFs.AddObject(configurationrhf);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(configurationrhf);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}