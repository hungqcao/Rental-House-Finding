using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Common;
using RentalHouseFinding.Models;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;

namespace RentalHouseFinding.Controllers
{
    public class UserController : Controller
    {
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
            DateTime? dt = null;
            userViewModel.DateOfBirth = profile.DOB.HasValue ? profile.DOB.Value : dt;
            userViewModel.Address = profile.Address;
            userViewModel.Avatar = profile.Avatar;
            userViewModel.Sex = profile.Sex;

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
            DateTime? a = null;
            userViewModel.DateOfBirth = profile.DOB.HasValue ? profile.DOB.Value : a; 
            userViewModel.Address = profile.Address;
            userViewModel.Avatar = profile.Avatar;

            return View(userViewModel);
        }
        //
        // POST: /User/Edit
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (Membership.ValidateUser(userViewModel.UserName, CommonController.GetMD5Hash(userViewModel.Password)))
            {
                //Get user ID
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                //Get user profile
                var profile = (from p in _db.Users where (p.Id == userId) select p).FirstOrDefault();

                profile.Username = userViewModel.UserName;
                profile.PhoneNumber = userViewModel.PhoneNumber;
                profile.Name = userViewModel.Name;
                profile.Email = userViewModel.Email;
                profile.DOB = userViewModel.DateOfBirth;
                profile.Address = userViewModel.Address;
                profile.Avatar = userViewModel.Avatar;

                _db.ObjectStateManager.ChangeObjectState(profile, System.Data.EntityState.Modified);
                _db.SaveChanges();
                TempData["ProfileChanged"] = true;
            }
            return RedirectToAction("Index", "User");
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Favorites()
        {
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
            });

            ViewBag.FavoriteList = postViewList;
            return View();
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Posts()
        {
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
            });
            ViewBag.PostList = postViewList.ToArray();
            
            return View();
        }

    }
}
