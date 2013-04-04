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
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            //Init/Retain filter values
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", TempData["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", TempData["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", TempData["UserId"]);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", TempData["CategoryId"]);
            ViewBag.Statuses = new SelectList(_db.PostStatuses, "Id", "Name", TempData["StatusId"]);

            //If visit page for the first time
            if (TempData["ProvinceId"] == null && TempData["DistrictId"] == null && TempData["UserId"] == null
                && TempData["CreatedDateFrom"] == null && TempData["CreatedDateTo"] == null
                && TempData["EditedDateFrom"] == null && TempData["EditedDateTo"] == null
                && TempData["RenewedDateFrom"] == null && TempData["RenewedDateTo"] == null 
                && TempData["ExpireDateFrom"] == null && TempData["ExpireDateFrom"] == null)
            {
                return View();
            }

            //Adding filter values
            var filters = new Hashtable();
            filters.Add("DistrictId", TempData["DistrictId"]);
            filters.Add("UserId", TempData["UserId"]);
            filters.Add("ProvinceId", TempData["ProvinceId"]);
            filters.Add("StatusId", TempData["StatusId"]);
            filters.Add("CreatedDateFrom", TempData["CreatedDateFrom"]);
            filters.Add("CreatedDateTo", TempData["CreatedDateTo"]);
            filters.Add("EditedDateFrom", TempData["EditedDateFrom"]);
            filters.Add("EditedDateTo", TempData["EditedDateTo"]);
            filters.Add("RenewedDateFrom", TempData["RenewedDateFrom"]);
            filters.Add("RenewedDateTo", TempData["RenewedDateTo"]);
            filters.Add("ExpireDateFrom", TempData["ExpireDateFrom"]);
            filters.Add("ExpireDateTo", TempData["ExpireDateTo"]);
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Index(int? page, FormCollection form)
        {
            //Retain filter values
            ViewBag.Provinces = new SelectList(_db.Provinces, "Id", "Name", form["ProvinceId"]);
            ViewBag.Districts = new SelectList(_db.Districts, "Id", "Name", form["DistrictId"]);
            ViewBag.Users = new SelectList(_db.Users, "Id", "Username", form["UserId"]);
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", form["CategoryId"]);
            ViewBag.Statuses = new SelectList(_db.PostStatuses, "Id", "Name", form["StatusId"]);

            //Reset temp data for accurate search results
            TempData["DistrictId"] = null;
            TempData["UserId"] = null;
            TempData["ProvinceId"] = null;
            TempData["StatusId"] = null;
            TempData["CreatedDateFrom"] = null;
            TempData["CreatedDateTo"] = null;
            TempData["EditedDateFrom"] = null;
            TempData["EditedDateTo"] = null;
            TempData["RenewedDateFrom"] = null;
            TempData["RenewedDateTo"] = null;
            TempData["ExpireDateFrom"] = null;
            TempData["ExpireDateTo"] = null;

            //Adding filter values
            var filters = new Hashtable();
            filters.Add("DistrictId", form["DistrictId"]);
            filters.Add("UserId", form["UserId"]);
            filters.Add("ProvinceId", form["ProvinceId"]);
            filters.Add("StatusId", form["StatusId"]);
            filters.Add("CreatedDateFrom", form["CreatedDateFrom"]);
            filters.Add("CreatedDateTo", form["CreatedDateTo"]);
            filters.Add("EditedDateFrom", form["EditedDateFrom"]);
            filters.Add("EditedDateTo", form["EditedDateTo"]);
            filters.Add("RenewedDateFrom", form["RenewedDateFrom"]);
            filters.Add("RenewedDateTo", form["RenewedDateTo"]);
            filters.Add("ExpireDateFrom", form["ExpireDateFrom"]);
            filters.Add("ExpireDateTo", form["ExpireDateTo"]);
            ViewBag.Grid = getGrid(filters, 1);
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        public WebGrid getGrid(Hashtable filters, int page)
        {
            IQueryable<Posts> postList = _db.Posts;
            if (filters["DistrictId"] != null)
            {
                if (filters["DistrictId"].ToString() != "")
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
                if (filters["UserId"].ToString() != "")
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
                if (filters["ProvinceId"].ToString() != "")
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

            if (filters["StatusId"] != null)
            {
                if (filters["StatusId"].ToString() != "")
                {
                    int statusId = Convert.ToInt32(filters["StatusId"]);
                    if (statusId > 0)
                    {
                        postList = postList.Where(p => (p.StatusId == statusId));
                    }
                    TempData["StatusId"] = statusId;
                }
            }

            if (filters["CreatedDateFrom"] != null)
            {
                if (filters["CreatedDateFrom"].ToString() != "")
                {
                    DateTime CreatedDateFrom = DateTime.Parse(filters["CreatedDateFrom"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.CreatedDate, CreatedDateFrom) <= 0));
                    TempData["CreatedDateFrom"] = CreatedDateFrom.ToString("yyyy/MM/dd");
                }
            }

            if (filters["CreatedDateTo"] != null)
            {
                if (filters["CreatedDateTo"].ToString() != "")
                {
                    DateTime CreatedDateTo = DateTime.Parse(filters["CreatedDateTo"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.CreatedDate, CreatedDateTo) >= 0));
                    TempData["CreatedDateTo"] = CreatedDateTo.ToString("yyyy/MM/dd");
                }
            }

            if (filters["EditedDateFrom"] != null)
            {
                if (filters["EditedDateFrom"].ToString() != "")
                {
                    DateTime EditedDateFrom = DateTime.Parse(filters["EditedDateFrom"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.EditedDate, EditedDateFrom) <= 0));
                    TempData["EditedDateFrom"] = EditedDateFrom.ToString("yyyy/MM/dd");
                }
            }

            if (filters["EditedDateTo"] != null)
            {
                if (filters["EditedDateTo"].ToString() != "")
                {
                    DateTime EditedDateTo = DateTime.Parse(filters["EditedDateTo"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.EditedDate, EditedDateTo) >= 0));
                    TempData["EditedDateTo"] = EditedDateTo.ToString("yyyy/MM/dd");
                }
            }

            if (filters["RenewedDateFrom"] != null)
            {
                if (filters["RenewedDateFrom"].ToString() != "")
                {
                    DateTime RenewedDateFrom = DateTime.Parse(filters["RenewedDateFrom"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.RenewDate, RenewedDateFrom) <= 0));
                    TempData["RenewedDateFrom"] = RenewedDateFrom.ToString("yyyy/MM/dd");
                }
            }

            if (filters["RenewedDateTo"] != null)
            {
                if (filters["RenewedDateTo"].ToString() != "")
                {
                    DateTime RenewedDateTo = DateTime.Parse(filters["RenewedDateTo"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.RenewDate, RenewedDateTo) >= 0));
                    TempData["RenewedDateTo"] = RenewedDateTo.ToString("yyyy/MM/dd");
                }
            }

            if (filters["ExpireDateFrom"] != null)
            {
                if (filters["ExpireDateFrom"].ToString() != "")
                {
                    DateTime ExpireDateFrom = DateTime.Parse(filters["ExpireDateFrom"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.ExpiredDate, ExpireDateFrom) <= 0));
                    TempData["ExpireDateFrom"] = ExpireDateFrom.ToString("yyyy/MM/dd");
                }
            }

            if (filters["ExpireDateTo"] != null)
            {
                if (filters["ExpireDateTo"].ToString() != "")
                {
                    DateTime ExpireDateTo = DateTime.Parse(filters["ExpireDateTo"].ToString());
                    postList = postList.Where(p => (EntityFunctions
                                .DiffDays(p.ExpiredDate, ExpireDateTo) >= 0));
                    TempData["ExpireDateTo"] = ExpireDateTo.ToString("yyyy/MM/dd");
                }
            }
            var postViewList = postList.Select(p => new
            {
                ID = p.Id,
                User = (from usr in _db.Users
                        where (usr.Id == p.UserId)
                        select usr.Name).FirstOrDefault(),
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                p.ExpiredDate,
                PostStatus = (from stt in _db.PostStatuses
                                where (stt.Id == p.StatusId)
                                select stt.Name).FirstOrDefault(),
                Category = (from cat in _db.Categories 
                            where (cat.Id == p.CategoryId) 
                            select cat.Name).FirstOrDefault()
            }).OrderBy(p => p.ID).Skip(MAX_RECORD_PER_PAGE * (page - 1)).Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", 
                canSort: false, rowsPerPage: 15);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: postList.Count());
            return grid;
        }
    }


}
