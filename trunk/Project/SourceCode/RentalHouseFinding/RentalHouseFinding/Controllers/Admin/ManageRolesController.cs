﻿using System;
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
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.Roles.ToList());
        }

        //
        // GET: /ManageRoles/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageRoles/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Roles roles = db.Roles.Single(r => r.Id == id);
            return View(roles);
        }

        //
        // POST: /ManageRoles/Edit/5
        [Authorize(Roles = "Admin")]
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}