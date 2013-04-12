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
    public class ManageBadwordController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageBadword/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? type)
        {
            ViewBag.TypesList = new SelectList(_db.BadWordTypes, "Id", "Name");
            if (type == null)
            {
                var badwords = _db.BadWords.Include("BadWordType");
                return View(badwords.ToList());
            }
            else
            {
                var badwords = _db.BadWords.Include("BadWordType").Where(w => w.TypeId == type);
                return View(badwords.ToList());
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ViewResult Index(FormCollection form)
        {
            ViewBag.TypesList = new SelectList(_db.BadWordTypes, "Id", "Name");
            if (String.IsNullOrEmpty(form["TypeId"]))
            {
                var badwords = _db.BadWords.Include("BadWordType");
                ViewBag.ErrorMsg = null;
                return View(badwords.ToList());
            }
            else
            {
                try
                {
                    int type = Convert.ToInt32(form["TypeId"]);
                    ViewBag.ErrorMsg = null;
                    var badwords = _db.BadWords.Include("BadWordType").Where(w => w.TypeId == type);
                    return View(badwords.ToList());
                }
                catch(FormatException ex)
                {
                    ViewBag.ErrorMsg = "Mã phân loại không hợp lệ";
                    return View();
                }
            }
        }
        //
        // GET: /ManageBadword/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_db.BadWordTypes, "Id", "Name");
            return View();
        } 

        //
        // POST: /ManageBadword/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(BadWords badwords)
        {
            if (ModelState.IsValid)
            {
                _db.BadWords.AddObject(badwords);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TypeId = new SelectList(_db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }
        
        //
        // GET: /ManageBadword/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BadWords badwords = _db.BadWords.Single(b => b.Id == id);
            ViewBag.TypeId = new SelectList(_db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }

        //
        // POST: /ManageBadword/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(BadWords badwords, FormCollection form, int id)
        {
            if (form["IsDeleted"] != "false")
            {
                var tmp = _db.BadWords.Where(bw => bw.Id == id).FirstOrDefault();
                _db.BadWords.DeleteObject(tmp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _db.BadWords.Attach(badwords);
                _db.ObjectStateManager.ChangeObjectState(badwords, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(_db.BadWordTypes, "Id", "Name", badwords.TypeId);
            return View(badwords);
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}