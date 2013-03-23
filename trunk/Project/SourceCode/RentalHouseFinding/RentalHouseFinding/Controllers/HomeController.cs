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
    public class HomeController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public HomeController()
            : this(new CacheRepository())
        {
        }

        public HomeController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
            Session["NumberSkip"] = null;
            Session["NumberResult"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name");
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name");

            if (model != null)
            {
                model.IsAdvancedSearch = false;
                model.IsNormalSearch = true;
                Session["SearchViewModel"] = model;
            }
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLatLonFromResult()
        {
            try
            {
                var myData = Repository.GetAllPosts().Select(a => new SelectListItem()
                {
                    Text = a.Lat + "|" + a.Lon + "|" + a.Area.ToString() + "|" + a.Price.ToString() + "|" + a.NumberAddress.Trim() + " " + a.Street.Trim() + "|" + a.Title,
                    Value = a.Id.ToString(),
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSectionSideListPost(int skipNum, int takeNum)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
            if (!(Session["NumberSkip"] == null))
            {
                skipNum += (int)Session["NumberSkip"];
            }
                
            if (Session["SearchViewModel"] != null)
            {
                SearchViewModel _modelRequest = (SearchViewModel)Session["SearchViewModel"];
                
                if (_modelRequest.IsNormalSearch)
                {
                    using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
                    {
                        IEnumerable<int> list = new List<int>();
                        
                        var suggList = fullTextHelp.FullTextSearchPostWithWeightenScore(_modelRequest.CategoryId,
                                                                                _modelRequest.ProvinceId,
                                                                                _modelRequest.DistrictId,
                                                                                _modelRequest.KeyWord,
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.DESCRIPTION_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.TITLE_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.STREET_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.NEARBY_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.NUMBER_ADDRESS_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                int.Parse(Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantColumnNameScoreNormalSearch.DIRECTION_COLUMN_SCORE_NAME, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString()),
                                                                                skipNum,
                                                                                takeNum);
                        List<Posts> query = new List<Posts>();
                        if (suggList != null)
                        {
                            list = suggList.Select(p => p.Id).ToList();
                            foreach (int i in list)
                            {
                                query.Add((from p in _db.Posts
                                             where p.Id == i
                                             select p).FirstOrDefault());
                            }
                        }
                        var lstPostViewModel = query.Select(p => CommonModel.ConvertPostToPostViewModel(p));
                        Session["ResultPostViewModel"] = lstPostViewModel.ToList();
                        if (Session["NumberResult"] == null)
                        {
                            Session["NumberResult"] = lstPostViewModel.Count();
                        }
                        else
                        {
                            int result = (int)Session["NumberResult"] + lstPostViewModel.Count();
                            Session["NumberResult"] = result;
                        }
                        if (Session["NumberSkip"] == null)
                        {
                            Session["NumberSkip"] = takeNum;
                        }else if (lstPostViewModel.Count() > 0)
                        {
                            Session["NumberSkip"] = (int)Session["NumberSkip"] + takeNum;
                        }
                        return View(lstPostViewModel);
                    }
                }

                if (_modelRequest.IsAdvancedSearch)
                {
                    using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
                    {
                        IEnumerable<int> list = new List<int>();
                        var suggList = fullTextHelp.AdvanceSearch(_modelRequest.CategoryId, 
                                                                  _modelRequest.ProvinceId, 
                                                                  _modelRequest.DistrictId, 
                                                                  _modelRequest.AreaMax,
                                                                  _modelRequest.AreaMin,
                                                                  _modelRequest.PriceMax,
                                                                  _modelRequest.PriceMin,
                                                                  _modelRequest.HasAirConditionerScore,
                                                                  _modelRequest.HasBedScore,
                                                                  _modelRequest.HasGarageScore,
                                                                  _modelRequest.HasInternetScore,
                                                                  _modelRequest.HasMotorParkingScore,
                                                                  _modelRequest.HasSecurityScore,
                                                                  _modelRequest.HasTVCableScore,
                                                                  _modelRequest.HasWaterHeaterScore,
                                                                  _modelRequest.IsAllowCookingScore,
                                                                  _modelRequest.IsStayWithOwnerScore,
                                                                  _modelRequest.HasToiletScore,
                                                                  skipNum, 
                                                                  takeNum);
                        List<Posts> query = new List<Posts>();
                        if (suggList != null)
                        {
                            list = suggList.Select(p => p.Id).ToList();
                            foreach (int i in list)
                            {
                                query.Add((from p in _db.Posts
                                           where p.Id == i
                                           select p).FirstOrDefault());
                            }
                        }
                        var lstPostViewModel = query.Select(p => CommonModel.ConvertPostToPostViewModel(p));
                        Session["ResultPostViewModel"] = lstPostViewModel.ToList();
                        if (Session["NumberResult"] == null)
                        {
                            Session["NumberResult"] = lstPostViewModel.Count();
                        }
                        else
                        {
                            int result = (int)Session["NumberResult"] + lstPostViewModel.Count();
                            Session["NumberResult"] = result;
                        }
                        if (Session["NumberSkip"] == null)
                        {
                            Session["NumberSkip"] = takeNum;
                        }
                        else if (lstPostViewModel.Count() > 0)
                        {
                            Session["NumberSkip"] = (int)Session["NumberSkip"] + takeNum;
                        }
                        return View(lstPostViewModel);
                    }
                }
            }
            return null;
        }
    }
}
