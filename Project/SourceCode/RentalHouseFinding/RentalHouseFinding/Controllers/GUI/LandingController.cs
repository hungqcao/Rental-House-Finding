using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Caching;

namespace RentalHouseFinding.Controllers
{
    public class LandingController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

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
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name");
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name");

            if(model != null)
            {
                model.IsAdvancedSearch = false;
                model.IsNormalSearch = true;
                Session["SearchViewModel"] = model;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
