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
    public class ManageProvincesController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageProvinces/

        public ViewResult Index()
        {
            return View(db.Provinces.ToList());
        }

        //
        // GET: /ManageProvinces/Details/5

        public ViewResult Details(int id)
        {
            Provinces provinces = db.Provinces.Single(p => p.Id == id);
            return View(provinces);
        }

        //
        // GET: /ManageProvinces/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageProvinces/Create

        [HttpPost]
        public ActionResult Create(Provinces provinces)
        {
            if (ModelState.IsValid)
            {
                db.Provinces.AddObject(provinces);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(provinces);
        }
        
        //
        // GET: /ManageProvinces/Edit/5
 
        public ActionResult Edit(int id)
        {
            Provinces provinces = db.Provinces.Single(p => p.Id == id);
            return View(provinces);
        }

        //
        // POST: /ManageProvinces/Edit/5

        [HttpPost]
        public ActionResult Edit(Provinces provinces)
        {
            if (ModelState.IsValid)
            {
                db.Provinces.Attach(provinces);
                db.ObjectStateManager.ChangeObjectState(provinces, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provinces);
        }

        //
        // GET: /ManageProvinces/Delete/5
 
        public ActionResult Delete(int id)
        {
            Provinces provinces = db.Provinces.Single(p => p.Id == id);
            return View(provinces);
        }

        //
        // POST: /ManageProvinces/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Provinces provinces = db.Provinces.Single(p => p.Id == id);
            db.Provinces.DeleteObject(provinces);
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