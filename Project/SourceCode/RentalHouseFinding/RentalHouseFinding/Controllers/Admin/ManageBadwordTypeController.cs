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
    public class ManageBadwordTypeController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageBadwordType/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(_db.BadWordTypes.ToList());
        }

        //
        // GET: /ManageBadwordType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageBadwordType/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(BadWordTypes badwordtypes)
        {
            if (ModelState.IsValid)
            {
                _db.BadWordTypes.AddObject(badwordtypes);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(badwordtypes);
        }
        
        //
        // GET: /ManageBadwordType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BadWordTypes badwordtypes = _db.BadWordTypes.Single(b => b.Id == id);
            return View(badwordtypes);
        }

        //
        // POST: /ManageBadwordType/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(BadWordTypes badwordtypes, FormCollection form, int id)
        {
            if (form["IsDeleted"] != "false")
            {
                var tmp = _db.BadWordTypes.Where(bw => bw.Id == id).FirstOrDefault();
                _db.BadWordTypes.DeleteObject(tmp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _db.BadWordTypes.Attach(badwordtypes);
                _db.ObjectStateManager.ChangeObjectState(badwordtypes, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(badwordtypes);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}