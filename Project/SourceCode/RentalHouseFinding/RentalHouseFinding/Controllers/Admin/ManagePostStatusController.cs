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
    public class ManagePostStatusController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManagePostStatus/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.PostStatuses.ToList());
        }

        //
        // GET: /ManagePostStatus/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            PostStatuses poststatuses = db.PostStatuses.Single(p => p.Id == id);
            return View(poststatuses);
        }

        //
        // GET: /ManagePostStatus/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManagePostStatus/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(PostStatuses poststatuses)
        {
            if (ModelState.IsValid)
            {
                db.PostStatuses.AddObject(poststatuses);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(poststatuses);
        }
        
        //
        // GET: /ManagePostStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PostStatuses poststatuses = db.PostStatuses.Single(p => p.Id == id);
            return View(poststatuses);
        }

        //
        // POST: /ManagePostStatus/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(PostStatuses poststatuses)
        {
            if (ModelState.IsValid)
            {
                db.PostStatuses.Attach(poststatuses);
                db.ObjectStateManager.ChangeObjectState(poststatuses, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poststatuses);
        }

        //
        // GET: /ManagePostStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            PostStatuses poststatuses = db.PostStatuses.Single(p => p.Id == id);
            return View(poststatuses);
        }

        //
        // POST: /ManagePostStatus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PostStatuses poststatuses = db.PostStatuses.Single(p => p.Id == id);
            db.PostStatuses.DeleteObject(poststatuses);
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