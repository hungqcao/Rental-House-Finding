using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Net;
using System.IO;
using RentalHouseFinding.Common;
using RentalHouseFinding.Caching;
using log4net;
using System.Reflection;

namespace RentalHouseFinding.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public ServiceController()
            : this(new CacheRepository())
        {
        }

        public ServiceController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDistrictList(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int idPro = Convert.ToInt32(id);
                    var districts = Repository.GetAllDistricts().Where(d => d.ProvinceId == idPro).ToList();
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
            catch (Exception ex)
            {
                log.Error(ex.Message);
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
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int idPost = Convert.ToInt32(id);
                    if (type.Equals("province"))
                    {
                        var provinces = Repository.GetAllProvinces().Where(d => d.Id == idPost).ToList();
                        var myData = provinces.Select(a => new SelectListItem()
                        {
                            Text = a.Name,
                            Value = a.Lat + ";" + a.Lon,
                        });

                        return Json(myData, JsonRequestBehavior.AllowGet);
                    }
                    else if (type.Equals("district"))
                    {
                        var districts = Repository.GetAllDistricts().Where(d => d.Id == idPost).ToList();
                        var myData = districts.Select(a => new SelectListItem()
                        {
                            Text = a.Name,
                            Value = a.Lat + ";" + a.Lon,
                        });

                        return Json(myData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);             
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

            try
            {

                using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
                {
                    int numberOfResult;
                    var suggList = fullTextHelp.FullTextSearchPostWithWeightenScore(catId,
                                                                                    proId,
                                                                                    disId,
                                                                                    keyword,
                                                                                    int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.DESCRIPTION_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                    int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.TITLE_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                    int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.STREET_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                    int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.NEARBY_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                    int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.NUMBER_ADDRESS_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                    skipNum,
                                                                                    takeNum,
                                                                                    out numberOfResult);
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
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);             
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
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return Json(new { message = "Fail" });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLocationWithDistrictId(int id)
        {
            try
            {
                var localtions = _db.Locations.Where(loc => loc.DistrictId == id).ToList();
                var myData = localtions.Select(a => new SelectListItem()
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString(),
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return Json(new { message = "Fail" });
            }
        }

        [Authorize(Roles = "User, Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllUserFavorite()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            try
            {
                var result = _db.Favorites.Where(f => f.UserId == userId && !f.IsDeleted && !f.Post.IsDeleted).ToList();
                return View(result);
            }
            catch
            {
                return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "User, Admin")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ComparePost(string lstId)
        {
            try
            {
                string[] lstStrId = lstId.Split('|');
                int count = 0;
                int id = 0;
                List<int> lstIdParse = new List<int>();
                foreach(string item in lstStrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int.TryParse(item, out id);
                        lstIdParse.Add(id);
                        count++;
                    }
                }
                List<Posts> lstPost = new List<Posts>();
                if (lstIdParse.Count <= 3)
                {
                    foreach (int item in lstIdParse)
                    {
                        if(item > 0)
                        {
                            lstPost.Add(_db.Posts.Where(p => p.Id == item).FirstOrDefault());
                        }
                    }
                }
                if (lstPost.Count < 2)
                {
                    return Content("Bạn cần chọn bài ít nhất 2 bài để so sánh", "text/html");
                }
                return View(lstPost);
            }
            catch
            {
                return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }


        //For Report Post
        
        //[AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "User, Admin")]
        public bool ReportPost(string postId, string resion)
        {
            try
            {
                int userid = CommonModel.GetUserIdByUsername(User.Identity.Name);
                if (userid != -1)
                {
                    ReportedPosts report = new ReportedPosts();
                    report.PostId = Int32.Parse(postId);
                    report.ReportedBy = userid;
                    report.Reason = resion;
                    report.ReportedDate = DateTime.Now;
                    report.IsIgnored = false;
                    _db.AddToReportedPosts(report);
                    _db.SaveChanges();
                    //Send email to Mod
                    //CommonModel.SendEmail("Vietvh01388@fpt.edu.vn", String.Format("PostId = {0} bị {1} report ", postId, User.Identity.Name), 0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }

        }


    }
}
