using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Caching;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class SearchController : Controller
    {
        public RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public SearchController()
            : this(new CacheRepository())
        {
        }

        public SearchController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name");
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", 2);
            ViewBag.DistrictId = CommonController.AddDefaultOption(new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == 2), "Id", "Name"), "Quận/Huyện", "0");
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if(model != null)
            {
                model.IsAdvancedSearch = false;
                model.IsNormalSearch = true;
                model.CenterMap = CommonController.GetCenterMap(model);                
               
                Session["SearchViewModel"] = model;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
