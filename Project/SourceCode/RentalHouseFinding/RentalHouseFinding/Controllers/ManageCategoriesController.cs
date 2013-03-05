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
    public class ManageCategoriesController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageCategories/

        public ViewResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /ManageCategories/Details/5

        public ViewResult Details(int id)
        {
            Categories categories = db.Categories.Single(c => c.Id == id);
            return View(categories);
        }

        //
        // GET: /ManageCategories/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageCategories/Create

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.AddObject(categories);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(categories);
        }
        
        //
        // GET: /ManageCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
            Categories categories = db.Categories.Single(c => c.Id == id);
            return View(categories);
        }

        //
        // POST: /ManageCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(categories);
                db.ObjectStateManager.ChangeObjectState(categories, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        //
        // GET: /ManageCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            Categories categories = db.Categories.Single(c => c.Id == id);
            return View(categories);
        }

        //
        // POST: /ManageCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Categories categories = db.Categories.Single(c => c.Id == id);
            db.Categories.DeleteObject(categories);
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