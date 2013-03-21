using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web.Helpers;
using System.Web.Routing;

namespace RentalHouseFinding.Controllers.Admin
{
    public class ManagePostsController : Controller
    {
        private readonly static RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManagePosts/

        public ActionResult Index(int? page)
        {
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", TempData["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", TempData["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", TempData["UserId"]);
            
            IQueryable<Posts> postList = _db.Posts;
            if (TempData["DistrictId"] != null)
            {
                int districtId = Convert.ToInt32(TempData["DistrictId"]);
                if (districtId > 0)
                {
                    postList = _db.Posts.Where(p => p.DistrictId == districtId);
                    TempData["DistrictId"] = districtId;
                }
            }
            if (TempData["User"]  != null)
            {
                int userId = Convert.ToInt32(TempData["User"]);
                if (userId > 0)
                {
                    postList = postList.Where(p => (p.UserId == userId));
                    TempData["User"] = userId;
                }
            }
            if (TempData["ProvinceId"] != null)
            {
                int provinceId = Convert.ToInt32(TempData["ProvinceId"]);
                if (provinceId > 0)
                {
                    var provinces = _db.Provinces.Where(p => p.Id == provinceId)
                                        .FirstOrDefault().Districts.Select(d => d.Id).ToArray();
                    postList = postList.Where(p => provinces.Contains(p.DistrictId));
                }
                TempData["ProvinceId"] = Convert.ToInt32(TempData["ProvinceId"]);
            }

            var postViewList = postList.Select(p => new
            {
                p.Id,
                p.UserId,
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                PostStatus = (from stt in _db.PostStatuses
                              where (stt.Id == p.StatusId)
                              select stt.Name).FirstOrDefault()
            });

            var grid = new WebGrid(postViewList, ajaxUpdateContainerId: "container-grid", canSort: false);

            ViewBag.Grid = grid.GetHtml(tableStyle: "webGrid", mode: WebGridPagerModes.All,
                            headerStyle: "header",
                            alternatingRowStyle: "alt",
                            htmlAttributes: new { id = "grid" });
            return View();
        }
        [HttpPost]
        public ActionResult Index(int? page, FormCollection form)
        {
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", TempData["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", TempData["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", TempData["UserId"]);

            IQueryable<Posts> postList = _db.Posts;
            if (!String.IsNullOrEmpty(form["DistrictId"]))
            {
                int districtId = Convert.ToInt32(form["DistrictId"]);
                if (districtId > 0)
                {
                    postList = _db.Posts.Where(p => p.DistrictId == districtId);
                    TempData["DistrictId"] = districtId;
                }
            }
            if (!String.IsNullOrEmpty(form["User"]))
            {
                int userId = Convert.ToInt32(form["User"]);
                if (userId > 0)
                {
                    postList = postList.Where(p => (p.UserId == userId));
                    TempData["User"] = userId;
                }
            }
            if (!String.IsNullOrEmpty(form["ProvinceId"]))
            {
                int provinceId = Convert.ToInt32(form["ProvinceId"]);
                if (provinceId > 0)
                {
                    var provinces = _db.Provinces.Where(p => p.Id == provinceId)
                                        .FirstOrDefault().Districts.Select(d => d.Id).ToArray();
                    postList = postList.Where(p => provinces.Contains(p.DistrictId));
                }
                TempData["ProvinceId"] = Convert.ToInt32(form["ProvinceId"]);
            }

            var postViewList = postList.Select(p => new
            {
                p.Id,
                p.UserId,
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                PostStatus = (from stt in _db.PostStatuses 
                 where (stt.Id == p.StatusId) select stt.Name).FirstOrDefault()
            });

            var grid = new WebGrid(postViewList, ajaxUpdateContainerId: "container-grid", canSort: false);

            ViewBag.Grid = grid.GetHtml(tableStyle: "webGrid", mode: WebGridPagerModes.All,
                            headerStyle: "header",
                            alternatingRowStyle: "alt",
                            htmlAttributes: new { id = "grid" });
            return View();
        }

    }
}
