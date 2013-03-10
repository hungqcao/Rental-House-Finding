using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.RHF.DAL;
using RentalHouseFinding.RHF.Common;

namespace RentalHouseFinding.Controllers
{
    public class HomeController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSectionSideListPost(int skipNum, int takeNum)
        {
            if (Session["SearchViewModel"] != null)
            {
                SearchViewModel _modelRequest = (SearchViewModel)Session["SearchViewModel"];
                

                if (_modelRequest.IsNormalSearch)
                {
                    using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
                    {
                        IEnumerable<int> list = new List<int>();
                        var suggList = fullTextHelp.GetFullTextSuggestion(_modelRequest.CategoryId, _modelRequest.ProvinceId, _modelRequest.DistrictId, _modelRequest.KeyWord, skipNum, takeNum);
                        if (suggList != null)
                        {
                            list = suggList.Select(p => p.Id).ToList();
                        }
                        List<Posts> query = (from p in _db.Posts
                                             where list.Contains(p.Id)
                                             select p).ToList();
                        var lstPostViewModel = query.Select(p => CommonModel.ConvertPostToPostViewModel(p));
                        return View(lstPostViewModel);
                    }
                }
            }
            return null;
        }
    }
}
