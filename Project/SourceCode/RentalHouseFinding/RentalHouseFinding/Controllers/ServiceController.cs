using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers
{
    public class ServiceController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDistrictList(string id)
        {
            int idPro = Convert.ToInt32(id);
            var districts = _db.Districts.Where(d => d.ProvinceId == idPro).ToList();
            var myData = districts.Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.Id.ToString(),
            });

            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// type = "province, district
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLatLon(string id, string type)
        {
            int idPost = Convert.ToInt32(id);
            if (type.Equals("province"))
            {
                var provinces = _db.Provinces.Where(d => d.Id == idPost).ToList();
                var myData = provinces.Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Lat + ";" + a.Lon,
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            else if (type.Equals("district"))
            {
                var districts = _db.Districts.Where(d => d.Id == idPost).ToList();
                var myData = districts.Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Lat + ";" + a.Lon,
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Service/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Service/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Service/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Service/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Service/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Service/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Service/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Service/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
