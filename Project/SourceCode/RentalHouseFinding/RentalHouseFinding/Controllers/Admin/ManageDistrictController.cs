﻿using System;
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
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageDistrict/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? provinceId)
        {
            if (provinceId == null)
            {
                ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name");
                var districts = _db.Districts.Include("Province");
                return View(districts.ToList());
            }
            else
            {
                ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", provinceId);
                var districts = _db.Districts.Where(d => d.ProvinceId == provinceId).Include("Province");
                return View(districts.ToList());
            }
        }

        //
        // POST: /ManageDistrict/
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ViewResult Index(FormCollection form)
        {
            if (form["provinceId"] == null)
            {
                ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name");
                var districts = _db.Districts.Where( d => !d.IsDeleted).Include("Province");
                return View(districts.ToList());
            }
            else
            {
                int provinceId = Convert.ToInt32(form["provinceId"]);
                ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", provinceId);
                var districts = _db.Districts.Where(d => (d.ProvinceId == provinceId && !d.IsDeleted)).Include("Province");
                return View(districts.ToList());
            }
        }

        //
        // GET: /ManageDistrict/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            Districts districts = _db.Districts.Single(d => d.Id == id);
            return View(districts);
        }

        //
        // GET: /ManageDistrict/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
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
                _db.Districts.AddObject(districts);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }
        
        //
        // GET: /ManageDistrict/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Districts districts = _db.Districts.Single(d => d.Id == id);
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name", districts.ProvinceId);
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
                _db.Districts.Attach(districts);
                _db.ObjectStateManager.ChangeObjectState(districts, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name", districts.ProvinceId);
            return View(districts);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}