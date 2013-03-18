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
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLatLonFromResult()
        {
            if (Session["ResultPostViewModel"] != null)
            {
                var lstResult = (IEnumerable<PostViewModel>)Session["ResultPostViewModel"];
                var myData = lstResult.Select(a => new SelectListItem()
                {
                    Text = a.Lat + ";" + a.Lon,
                    Value = a.Id.ToString(),
                });

                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSectionSideListPost(int skipNum, int takeNum)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
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
                        if (suggList != null)
                        {
                            list = suggList.Select(p => p.Id).ToList();
                        }
                        List<Posts> query = (from p in _db.Posts
                                             where list.Contains(p.Id)
                                             select p).ToList();
                        var lstPostViewModel = query.Select(p => CommonModel.ConvertPostToPostViewModel(p));
                        Session["ResultPostViewModel"] = lstPostViewModel.ToList();
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
                        if (suggList != null)
                        {
                            list = suggList.Select(p => p.Id).ToList();
                        }
                        List<Posts> query = (from p in _db.Posts
                                             where list.Contains(p.Id)
                                             select p).ToList();
                        var lstPostViewModel = query.Select(p => CommonModel.ConvertPostToPostViewModel(p));
                        Session["ResultPostViewModel"] = lstPostViewModel.ToList();
                        return View(lstPostViewModel);
                    }
                }
            }
            return null;
        }
    }
}
