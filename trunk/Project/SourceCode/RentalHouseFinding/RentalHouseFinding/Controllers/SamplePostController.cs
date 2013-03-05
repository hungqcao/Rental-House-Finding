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
    public class SamplePostController : Controller
    {
        private RentalHouseFindingEntities db = new RentalHouseFindingEntities();

        //
        // GET: /SamplePost/

        public ViewResult Index()
        {
            var posts = db.Posts.Include("District").Include("Category").Include("PostStatus").Include("User");
            return View(posts.ToList());
        }

        //
        // GET: /SamplePost/Details/5

        public ViewResult Details(int id)
        {
            Posts posts = db.Posts.Single(p => p.Id == id);
            return View(posts);
        }

        //
        // GET: /SamplePost/Create

        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.PostStatuses, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        } 

        //
        // POST: /SamplePost/Create

        [HttpPost]
        public ActionResult Create(Posts posts)
        {
            if (ModelState.IsValid)
            {
                db.Posts.AddObject(posts);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", posts.DistrictId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", posts.CategoryId);
            ViewBag.StatusId = new SelectList(db.PostStatuses, "Id", "Name", posts.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", posts.UserId);
            return View(posts);
        }
        
        //
        // GET: /SamplePost/Edit/5
 
        public ActionResult Edit(int id)
        {
            Posts posts = db.Posts.Single(p => p.Id == id);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", posts.DistrictId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", posts.CategoryId);
            ViewBag.StatusId = new SelectList(db.PostStatuses, "Id", "Name", posts.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", posts.UserId);
            return View(posts);
        }

        //
        // POST: /SamplePost/Edit/5

        [HttpPost]
        public ActionResult Edit(Posts posts)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(posts);
                db.ObjectStateManager.ChangeObjectState(posts, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", posts.DistrictId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", posts.CategoryId);
            ViewBag.StatusId = new SelectList(db.PostStatuses, "Id", "Name", posts.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", posts.UserId);
            return View(posts);
        }

        //
        // GET: /SamplePost/Delete/5
 
        public ActionResult Delete(int id)
        {
            Posts posts = db.Posts.Single(p => p.Id == id);
            return View(posts);
        }

        //
        // POST: /SamplePost/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Posts posts = db.Posts.Single(p => p.Id == id);
            db.Posts.DeleteObject(posts);
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