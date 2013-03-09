using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Net;
using System.IO;
using RentalHouseFinding.RHF.DAL;

namespace RentalHouseFinding.Controllers
{
    public class ServiceController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDistrictList(string id)
        {
            if (!string.IsNullOrEmpty(id))
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
            else
            {
                return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
            }
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
            if (!string.IsNullOrEmpty(id))
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
            }
            return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetFullTextSuggestion(string categoryId, string provinceId, string districtId, string keyword)
        {
            using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
            {
                var suggList = fullTextHelp.GetFullTextSuggestion(int.Parse(categoryId), int.Parse(provinceId), int.Parse(districtId), keyword);
                if (suggList != null)
                {
                    var myData = suggList.Select(a => new SelectListItem()
                    {
                        Text = a.Title,
                        Value = a.Id.ToString(),
                    });

                    return Json(myData, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
        }
    }
}
