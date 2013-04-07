﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Common;
using RentalHouseFinding.Models;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Data.Objects;
using System.Web.Helpers;

namespace RentalHouseFinding.Controllers
{
    public class UserController : Controller
    {
        private const int MAX_RECORD_PER_PAGE = 15;
        private readonly static RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /User/
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            InfoAndUserLogsViewModel model = new InfoAndUserLogsViewModel();
            model.User = _db.Users.Where(u => u.Id == userId).FirstOrDefault();
            model.UserLogsList = _db.UserLogs.Where(u => u.UserId == userId).OrderBy(u => u.CreatedDate).ToList();
            model.IsOpenIdOrFBAcc = CommonModel.IsOpenIdOrFacebookAccount(userId);
            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Info()
        {
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            //Get user profile
            var profile = (from p in _db.Users where (p.Id == userId) select p).FirstOrDefault();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = profile.Username;
            userViewModel.PhoneNumber = profile.PhoneNumber;
            userViewModel.Name = profile.Name;
            userViewModel.Email = profile.Email;

            return View(userViewModel);
        }

        //
        // GET: /User/Edit
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit()
        {
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            //Get user profile
            var profile = (from p in _db.Users where (p.Id == userId) select p).FirstOrDefault();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = profile.Username;
            userViewModel.PhoneNumber = profile.PhoneNumber;
            userViewModel.Name = profile.Name;
            userViewModel.Email = profile.Email;

            return View(userViewModel);
        }
        //
        // POST: /User/Edit
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            
                //Get user ID
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);

                //Get user profile
                var profile = (from p in _db.Users where (p.Id == userId) select p).FirstOrDefault();

                profile.PhoneNumber = userViewModel.PhoneNumber;
                profile.Name = userViewModel.Name;
                profile.Email = userViewModel.Email;

                _db.ObjectStateManager.ChangeObjectState(profile, System.Data.EntityState.Modified);
                _db.SaveChanges();
                TempData["ProfileChanged"] = true;
                TempData["MessageChangeUserInfoSuccess"] = "Thay đổi thông tin thành công";
                return RedirectToAction("Index", "User");
            
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Favorites(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            //Get user's favorite list
            var lstPostId = (from f in _db.Favorites
                             where (f.UserId == userId && !f.IsDeleted)
                             select f.PostId).ToList();
            var favoriteList = (from f in _db.Posts
                                where (!f.IsDeleted && lstPostId.Contains(f.Id)) 
                                select f).ToList();

            var postViewList = favoriteList.Select(p => new
            {
                p.Id,
                p.UserId,
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                PostStatus = p.PostStatus.Name
            }).OrderBy(p => p.Id).Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE); ;

            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", canSort: false);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: favoriteList.Count());
            ViewBag.Grid = grid;
            ViewBag.Index = ((int)page - 1) * MAX_RECORD_PER_PAGE;

            return View();
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Posts(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            //Get post status list
            var postStatusList = (from p in _db.PostStatuses
                                  select p).ToList();
            ViewBag.StatusList = postStatusList;
            //Get user's posts list
            ViewBag.UserId = userId;
            var postList = (from p in _db.Posts
                            where (p.UserId == userId && !p.IsDeleted)
                            select p);

            var postViewList = postList.Select(p => new
            {
                p.Id,
                p.UserId,
                p.Title,
                p.CreatedDate,
                p.EditedDate,
                p.RenewDate,
                p.ExpiredDate,
                PostStatus = (from stt in _db.PostStatuses
                              where (stt.Id == p.StatusId)
                              select stt.Name).FirstOrDefault()
            }).OrderBy(p => p.Id).Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", canSort: false);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: postList.Count());
            ViewBag.Grid = grid;
            ViewBag.Index = ((int)page - 1) * MAX_RECORD_PER_PAGE;
            return View();
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Payments(int? page, ManagePaymentModel model)
        {
            if (page == null)
            {
                ViewBag.Index = 0;
                return View();
            }
            else
            {
                //Get user ID
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);

                var userPostList = (from p in _db.Posts where (p.UserId == userId) select p.Id).ToList();
                IQueryable<Payments> paymentList = (from p in _db.Payments 
                                                    where (userPostList.Contains(p.PostsId)) 
                                                    select p);

                if (model.CreatedDateFrom != null)
                {
                    paymentList = paymentList.Where(p => (EntityFunctions
                                .DiffDays(p.CreatedDate, model.CreatedDateFrom) <= 0));
                }

                if (model.CreatedDateTo != null)
                {
                    paymentList = paymentList.Where(p => (EntityFunctions
                                .DiffDays(p.CreatedDate, model.CreatedDateTo) >= 0));
                }

                IQueryable<Payments> paymentViewList;
                paymentViewList = (from p in paymentList select p)
                    .OrderBy(p => p.Id)
                    .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1))
                    .Take(MAX_RECORD_PER_PAGE);
                var grid = new WebGrid(ajaxUpdateContainerId: "container-grid",
                canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
                grid.Bind(paymentViewList, autoSortAndPage: false, rowCount: paymentList.Count());
                model.Grid = grid;
                ViewBag.Index = ((int)page - 1) * MAX_RECORD_PER_PAGE;
                return View(model);
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public ActionResult Payments(ManagePaymentModel model)
        {
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);

            var userPostList = (from p in _db.Posts where (p.UserId == userId) select p.Id).ToList();
            IQueryable<Payments> paymentList = (from p in _db.Payments
                                                where (userPostList.Contains(p.PostsId))
                                                select p);

            if (model.CreatedDateFrom != null)
            {
                paymentList = paymentList.Where(p => (EntityFunctions
                            .DiffDays(p.CreatedDate, model.CreatedDateFrom) <= 0));
            }

            if (model.CreatedDateTo != null)
            {
                paymentList = paymentList.Where(p => (EntityFunctions
                            .DiffDays(p.CreatedDate, model.CreatedDateTo) >= 0));
            }

            IQueryable<Payments> paymentViewList;
            paymentViewList = (from p in paymentList select p)
                .OrderBy(p => p.Id)
                .Skip(0)
                .Take(MAX_RECORD_PER_PAGE);
            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid",
            canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(paymentViewList, autoSortAndPage: false, rowCount: paymentList.Count());
            model.Grid = grid;
            ViewBag.Index = 0;
            return View(model);
        }
    }
}
