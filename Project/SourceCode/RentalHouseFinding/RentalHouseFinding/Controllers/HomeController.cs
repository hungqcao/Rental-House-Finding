﻿using System;
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
                        return View(lstPostViewModel);
                    }
                }
            }
            return null;
        }
    }
}