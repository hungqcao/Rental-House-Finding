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
    public class ManageBadwordController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageBadword/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            var badwords = db.BadWords.Include("BadWordType");
            return View(badwords.ToList());
        }

        //
        // GET: /ManageBadword/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.BadWordTypes, "Id", "Name");
            return View();
        } 

        //
        // POST: /ManageBadword/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(BadWords badwords)
        {
            if (ModelState.IsValid)
            {
                db.BadWords.AddObject(badwords);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TypeId = new SelectList(db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }
        
        //
        // GET: /ManageBadword/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BadWords badwords = db.BadWords.Single(b => b.Id == id);
            ViewBag.TypeId = new SelectList(db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }

        //
        // POST: /ManageBadword/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(BadWords badwords)
        {
            if (ModelState.IsValid)
            {
                db.BadWords.Attach(badwords);
                db.ObjectStateManager.ChangeObjectState(badwords, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}