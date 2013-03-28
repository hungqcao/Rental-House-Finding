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
    public class ManageDistrictController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageDistrict/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            var districts = db.Districts.Include("Province");
            return View(districts.ToList());
        }

        //
        // GET: /ManageDistrict/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            return View(districts);
        }

        //
        // GET: /ManageDistrict/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name");
            return View();
        } 

        //
        // POST: /ManageDistrict/Create
        [Authorize(Roles = "Admin")]
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
        // GET: /ManageDistrict/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }

        //
        // POST: /ManageDistrict/Edit/5
        [Authorize(Roles = "Admin")]
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
        // GET: /ManageDistrict/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Districts districts = db.Districts.Single(d => d.Id == id);
            return View(districts);
        }

        //
        // POST: /ManageDistrict/Delete/5
        [Authorize(Roles = "Admin")]
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