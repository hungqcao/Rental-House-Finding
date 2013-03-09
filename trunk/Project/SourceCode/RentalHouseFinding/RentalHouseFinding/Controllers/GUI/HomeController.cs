using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers
{
    public class HomeController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
