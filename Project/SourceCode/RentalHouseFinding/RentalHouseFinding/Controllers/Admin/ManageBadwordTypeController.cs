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
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageBadwordType/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.BadWordTypes.ToList());
        }

        //
        // GET: /ManageBadwordType/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            BadWordTypes badwordtypes = db.BadWordTypes.Single(b => b.Id == id);
            return View(badwordtypes);
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
                db.BadWordTypes.AddObject(badwordtypes);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(badwordtypes);
        }
        
        //
        // GET: /ManageBadwordType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BadWordTypes badwordtypes = db.BadWordTypes.Single(b => b.Id == id);
            return View(badwordtypes);
        }

        //
        // POST: /ManageBadwordType/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(BadWordTypes badwordtypes)
        {
            if (ModelState.IsValid)
            {
                db.BadWordTypes.Attach(badwordtypes);
                db.ObjectStateManager.ChangeObjectState(badwordtypes, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(badwordtypes);
        }

        //
        // GET: /ManageBadwordType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            BadWordTypes badwordtypes = db.BadWordTypes.Single(b => b.Id == id);
            return View(badwordtypes);
        }

        //
        // POST: /ManageBadwordType/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            BadWordTypes badwordtypes = db.BadWordTypes.Single(b => b.Id == id);
            db.BadWordTypes.DeleteObject(badwordtypes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}