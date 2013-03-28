using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web.Helpers;
using System.Web.Routing;
using System.Collections;
using System.Data.Objects;

namespace RentalHouseFinding.Controllers.Admin
{
    public class ManagePostsController : Controller
    {
        private const int MAX_RECORD_PER_PAGE = 15;
        private readonly static RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManagePosts/

        public ActionResult Index(int? page)
        {
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", TempData["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", TempData["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", TempData["UserId"]);

            if (TempData["ProvinceId"] == null && TempData["DistrictId"] == null
                && TempData["UserId"] == null && TempData["ExpireDate"] == null)
            {
                return View();
            }
            var filters = new Hashtable();
            filters.Add("DistrictId", TempData["DistrictId"]);
            filters.Add("UserId", TempData["UserId"]);
            filters.Add("ProvinceId", TempData["ProvinceId"]);
            filters.Add("ExpireDate", TempData["ExpireDate"]);
            WebGrid grid;
            if (page == null)
            {
                grid = getGrid(filters, 1);
            }
            else
            {
                grid = getGrid(filters, (int)page);
            }
            ViewBag.Grid = grid;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int? page, FormCollection form)
        {

            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", form["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", form["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", form["UserId"]);


            var filters = new Hashtable();
            filters.Add("DistrictId", form["DistrictId"]);
            filters.Add("UserId", form["UserId"]);
            filters.Add("ProvinceId", form["ProvinceId"]);
            filters.Add("ExpireDate", form["ExpireDate"]);
            ViewBag.Grid = getGrid(filters, 1);
            
            return View();
        }
        public WebGrid getGrid(Hashtable filters, int page)
        {
            IQueryable<Posts> postList = _db.Posts;
            if (filters["DistrictId"] != null)
            {
                if (filters["DistrictId"] != "")
                {
                    int districtId = Convert.ToInt32(filters["DistrictId"]);
                    if (districtId > 0)
                    {
                        postList = _db.Posts.Where(p => p.DistrictId == districtId);
                    }
                    TempData["DistrictId"] = districtId;
                }
            }
            if (filters["UserId"] != null)
            {
                if (filters["UserId"] != "")
                {
                    int userId = Convert.ToInt32(filters["UserId"]);
                    if (userId > 0)
                    {
                        postList = postList.Where(p => (p.UserId == userId));
                    }
                    TempData["UserId"] = userId;
                }
            }
            if (filters["ProvinceId"] != null && (filters["DistrictId"] == null || filters["DistrictId"].ToString() == "0"))
            {
                if (filters["ProvinceId"] != "")
                {
                    int provinceId = Convert.ToInt32(filters["ProvinceId"]);
                    if (provinceId > 0)
                    {
                        var dictricts = (from d in _db.Districts where (d.ProvinceId == provinceId) select d.Id).ToArray();
                        postList = postList.Where(p => dictricts.Contains(p.DistrictId));
                    }
                    TempData["ProvinceId"] = provinceId;
                }
            }

            if (filters["ExpireDate"] != null)
            {
                if (filters["ExpireDate"].ToString() != "")
                {
                    DateTime expireDate = DateTime.Parse(filters["ExpireDate"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.ExpiredDate, expireDate) == 0));
                    TempData["ExpireDate"] = expireDate.ToString("yyyy/MM/dd");
                }

            }
            var postViewList = postList.Select(p => new
            {
                ID = p.Id,
                p.UserId,
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                p.ExpiredDate,
                PostStatus = (from stt in _db.PostStatuses
                                where (stt.Id == p.StatusId)
                                select stt.Name).FirstOrDefault()
            }).OrderBy(p => p.ID).Skip(MAX_RECORD_PER_PAGE * (page - 1)).Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", 
                canSort: false, rowsPerPage: 15);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: postList.Count());
            return grid;
        }
    }


}
