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
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageCategories/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(_db.Categories.ToList());
        }

        //
        // GET: /ManageCategories/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageCategories/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.AddObject(categories);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(categories);
        }
        
        //
        // GET: /ManageCategories/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Categories categories = _db.Categories.Single(c => c.Id == id);
            return View(categories);
        }

        //
        // POST: /ManageCategories/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Attach(categories);
                _db.ObjectStateManager.ChangeObjectState(categories, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}