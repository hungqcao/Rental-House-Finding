using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class AdvancedSearchController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /AdvancedSearch/

        public ActionResult Index()
        {
            ViewBag.Score = new SelectList(_db.AdvanceSearchScores, "Score", "Name");
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            ViewBag.Score = new SelectList(_db.AdvanceSearchScores, "Score", "Name");
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");

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
