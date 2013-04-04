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
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManagePostStatus/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(_db.PostStatuses.ToList());
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
                _db.PostStatuses.AddObject(poststatuses);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(poststatuses);
        }
        
        //
        // GET: /ManagePostStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PostStatuses poststatuses = _db.PostStatuses.Single(p => p.Id == id);
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
                _db.PostStatuses.Attach(poststatuses);
                _db.ObjectStateManager.ChangeObjectState(poststatuses, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poststatuses);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}