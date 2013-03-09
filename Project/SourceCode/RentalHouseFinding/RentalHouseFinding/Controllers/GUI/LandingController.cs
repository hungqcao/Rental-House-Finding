using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers
{
    public class LandingController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");

            if(model != null)
            {
                TempData["SearchViewModel"] = model;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
