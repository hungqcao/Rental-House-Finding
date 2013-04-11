using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web.Helpers;

namespace RentalHouseFinding.Controllers.Admin
{ 
    public class ManageLocationController : Controller
    {
        private const int MAX_RECORD_PER_PAGE = 15;
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManageLocation/

        public ViewResult Index(int? districtId, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", ViewBag.ProvinceId);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", ViewBag.DistrictId);
            IQueryable<Locations> locations = _db.Locations;
            if (districtId != null)
            {
                locations = locations.Where(l => l.DistrictId == (int)districtId);
            }
            else
            {
                if (Session["ProvinceId"] != null
                    && Session["ProvinceId"].ToString() != "")
                {
                    int provinceId = Convert.ToInt32(Session["ProvinceId"]);
                    if (provinceId > 0)
                    {
                        var districtList = (from d in _db.Districts
                                            where (d.ProvinceId == provinceId)
                                            select d.Id).ToList();
                        locations = locations
                            .Where(l => districtList.Contains(l.DistrictId));
                    }
                }

                if (Session["DistrictId"] != null 
                    && Session["DistrictId"].ToString() != "")
                {
                    int sessionDistrictId = Convert.ToInt32(Session["DistrictId"]);
                    if (sessionDistrictId > 0)
                    {
                        locations = locations
                            .Where(l => l.DistrictId == sessionDistrictId);
                    }
                }
                
            }
            var locationView = locations.Select(l => new
            { 
                l.Id,
                l.Name,
                District = (from d in _db.Districts 
                            where (d.Id == l.DistrictId) 
                            select d.Name).FirstOrDefault(),
                l.Lat,
                l.Lon
            }).OrderBy(l => l.Id)
                .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid"
                , canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(locationView, autoSortAndPage: false, rowCount: locations.Count());
            ViewBag.Grid = grid;
            return View();
        }
        [HttpPost]
        public ViewResult Index(int? page, FormCollection form)
        {
            if (page == null)
            {
                page = 1;
            }
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", form["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", form["DistrictId"]);
            
            IQueryable<Locations> locations = _db.Locations;

            if (!String.IsNullOrEmpty(form["ProvinceId"]))
            {
                int provinceId = Convert.ToInt32(form["ProvinceId"]);
                if (provinceId > 0)
                {
                    Session["ProvinceId"] = provinceId;
                    var districtList = (from d in _db.Districts
                                        where (d.ProvinceId == provinceId)
                                        select d.Id).ToList();
                    locations = locations.Where(l => districtList.Contains(l.DistrictId));
                }
            }

            if (!String.IsNullOrEmpty(form["DistrictId"]))
            {
                int districtId = Convert.ToInt32(form["DistrictId"]);
                if (districtId > 0)
                {
                    Session["DistrictId"] = districtId;
                    locations = locations.Where(l => l.DistrictId == districtId);
                }
            }
            var locationView = locations.Select(l => new
            {
                l.Id,
                l.Name,
                District = (from d in _db.Districts
                            where (d.Id == l.DistrictId)
                            select d.Name).FirstOrDefault(),
                l.Lat,
                l.Lon
            }).OrderBy(l => l.Id)
                .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", 
                canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(locationView, autoSortAndPage: false, rowCount: locations.Count());
            ViewBag.Grid = grid;
            return View();
        }

        //
        // GET: /ManageLocation/Create

        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name");
            return View();
        } 

        //
        // POST: /ManageLocation/Create

        [HttpPost]
        public ActionResult Create(Locations locations)
        {
            if (ModelState.IsValid)
            {
                _db.Locations.AddObject(locations);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name", locations.DistrictId);
            return View(locations);
        }
        
        //
        // GET: /ManageLocation/Edit/5
 
        public ActionResult Edit(int id)
        {
            Locations locations = _db.Locations.Single(l => l.Id == id);
            ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name", locations.DistrictId);
            return View(locations);
        }

        //
        // POST: /ManageLocation/Edit/5

        [HttpPost]
        public ActionResult Edit(Locations locations)
        {
            if (ModelState.IsValid)
            {
                _db.Locations.Attach(locations);
                _db.ObjectStateManager.ChangeObjectState(locations, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name", locations.DistrictId);
            return View(locations);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}