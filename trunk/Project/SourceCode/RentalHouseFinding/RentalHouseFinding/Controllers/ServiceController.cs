using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Net;
using System.IO;
using RentalHouseFinding.RHF.DAL;
using RentalHouseFinding.RHF.Common;

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
        public JsonResult GetFullTextSuggestion(string categoryId, string provinceId, string districtId, string keyword, string skip, string take)
        {
            int catId;
            int proId;
            int disId;
            int skipNum;
            int takeNum;

            int.TryParse(categoryId, out catId);
            int.TryParse(provinceId, out proId);
            int.TryParse(districtId, out disId);
            int.TryParse(skip, out skipNum);
            int.TryParse(take, out takeNum);
            using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
            {
                var suggList = fullTextHelp.GetFullTextSuggestion(catId, proId, disId, keyword, skipNum, takeNum);
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

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLocation()
        {
            try
            {
                var localtions = _db.Locations.ToList();
                var myData = localtions.Select(a => new SelectListItem()
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString(),
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "Fail" });
            }
        }

        //For Report Post
        
        //[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "User, Admin")]
        public bool ReportPost(string postId, string resion)
        {
            RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
            int userid = CommonModel.GetUserIdByUsername(User.Identity.Name);
            if (userid != -1)
            {
                ReportedPosts repost = new ReportedPosts();
                repost.PostId = Int32.Parse(postId);
                repost.ReportedBy = userid;
                repost.Reason = resion;
                repost.ReportedDate = DateTime.Now;
                repost.IsIgnored = false;
                _db.AddToReportedPosts(repost);
                _db.SaveChanges();
                //Send email to Mod
                //CommonModel.SendEmail("Vietvh01388@fpt.edu.vn", String.Format("PostId = {0} bị {1} repost ", postId, User.Identity.Name), 0);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
