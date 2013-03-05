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
    public class ManageRolesController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /ManageRoles/

        public ViewResult Index()
        {
            return View(db.Roles.ToList());
        }

        //
        // GET: /ManageRoles/Details/5

        public ViewResult Details(int id)
        {
            Roles roles = db.Roles.Single(r => r.Id == id);
            return View(roles);
        }

        //
        // GET: /ManageRoles/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageRoles/Create

        [HttpPost]
        public ActionResult Create(Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Roles.AddObject(roles);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(roles);
        }
        
        //
        // GET: /ManageRoles/Edit/5
 
        public ActionResult Edit(int id)
        {
            Roles roles = db.Roles.Single(r => r.Id == id);
            return View(roles);
        }

        //
        // POST: /ManageRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Attach(roles);
                db.ObjectStateManager.ChangeObjectState(roles, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roles);
        }

        //
        // GET: /ManageRoles/Delete/5
 
        public ActionResult Delete(int id)
        {
            Roles roles = db.Roles.Single(r => r.Id == id);
            return View(roles);
        }

        //
        // POST: /ManageRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Roles roles = db.Roles.Single(r => r.Id == id);
            db.Roles.DeleteObject(roles);
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