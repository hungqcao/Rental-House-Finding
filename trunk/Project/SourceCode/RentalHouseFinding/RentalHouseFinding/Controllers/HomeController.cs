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
            if (TempData["SearchViewModel"] != null)
            {
                SearchViewModel model = (SearchViewModel)TempData["SearchViewModel"];
                
                using (FullTextSearchHelper fullTextHelp = new FullTextSearchHelper())
                {
                    IEnumerable<int> list = new List<int>();
                    var suggList = fullTextHelp.GetFullTextSuggestion(model.CategoryId, model.ProvinceId, model.DistrictId, model.KeyWord, skipNum, takeNum);
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
            return View("Index");
        }
        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

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
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

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
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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
