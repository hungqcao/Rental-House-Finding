﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Caching;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class LandingController : Controller
    {
        public RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public LandingController()
            : this(new CacheRepository())
        {
        }

        public LandingController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name");
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", 2);
            ViewBag.DistrictId = CommonController.AddDefaultOption(new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == 2), "Id", "Name"), "Quận huyện", "0");
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if(model != null)
            {
                model.IsAdvancedSearch = false;
                model.IsNormalSearch = true;
                model.CenterMap = GetCenterMap(model);                
               
                Session["SearchViewModel"] = model;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }
        public string GetCenterMap(SearchViewModel model)
        {
            string centerMap = String.Empty;
            if (model.DistrictId == 0)
            {
                var province = (from p in _db.Provinces where p.Id == model.ProvinceId && !p.IsDeleted select p).FirstOrDefault();
                centerMap = province.Lat + "," + province.Lon + "|11" ;
            }
            else
            {
                var district = (from p in _db.Districts where p.Id == model.DistrictId && !p.IsDeleted select p).FirstOrDefault();
                centerMap = district.Lat + "," + district.Lon + "|14";
            }
            return centerMap;
        }
    }
}