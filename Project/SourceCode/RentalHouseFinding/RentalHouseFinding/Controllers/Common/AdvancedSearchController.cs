using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;
using RentalHouseFinding.Caching;

namespace RentalHouseFinding.Controllers
{
    public class AdvancedSearchController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /AdvancedSearch/
        public ICacheRepository Repository { get; set; }
        public AdvancedSearchController()
            : this(new CacheRepository())
        {
        }

        public AdvancedSearchController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult Index()
        {
            ViewBag.Score = new SelectList(Repository.GetAllAdvanceSearchScore(), "Score", "Name");
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", CategoryConstant.PHONG_TRO);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", ProvinceConstant.HA_NOI);
            ViewBag.DistrictId = CommonController.AddDefaultOption(new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == ProvinceConstant.HA_NOI), "Id", "Name"), "Quận/Huyện", "0");

            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if (model != null)
            {
                model.IsAdvancedSearch = true;
                model.IsNormalSearch = false;
                model.CenterMap = CommonController.GetCenterMap(model);
                Session["SearchViewModel"] = model;
  
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
