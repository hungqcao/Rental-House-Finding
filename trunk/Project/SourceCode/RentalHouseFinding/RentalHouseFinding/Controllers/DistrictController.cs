using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers
{ 
    public class DistrictController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /District/

        public ViewResult Index()
        {
            var districts = db.Districts.Include("Province");
            return View(districts.ToList());
        }

        //
        // GET: /District/Details/5

        public ViewResult Details(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            return View(districts);
        }

        //
        // GET: /District/Create

        public ActionResult Create()
        {
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name");
            return View();
        } 

        //
        // POST: /District/Create

        [HttpPost]
        public ActionResult Create(Districts districts)
        {
            if (ModelState.IsValid)
            {
                db.Districts.AddObject(districts);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }
        
        //
        // GET: /District/Edit/5
 
        public ActionResult Edit(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }

        //
        // POST: /District/Edit/5

        [HttpPost]
        public ActionResult Edit(Districts districts)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Attach(districts);
                db.ObjectStateManager.ChangeObjectState(districts, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }

        //
        // GET: /District/Delete/5
 
        public ActionResult Delete(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            return View(districts);
        }

        //
        // POST: /District/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Districts districts = db.Districts.Single(d => d.Id == id);
            db.Districts.DeleteObject(districts);
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