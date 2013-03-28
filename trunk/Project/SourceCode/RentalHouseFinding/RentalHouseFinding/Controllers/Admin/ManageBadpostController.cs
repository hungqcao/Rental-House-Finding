using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web.Helpers;

namespace RentalHouseFinding.Controllers.Admin
{ 
    public class ManageBadpostController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        private const int MAX_RECORD_PER_PAGE = 15;
        //
        // GET: /ManageBadpost/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var postsList = (from p in _db.Posts where (p.StatusId == 2) select p);

            var postViewList = postsList.Select(p => new
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
            }).OrderBy(p => p.ID).Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);

            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid",
            canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: postsList.Count());

            ViewBag.Grid = grid;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}