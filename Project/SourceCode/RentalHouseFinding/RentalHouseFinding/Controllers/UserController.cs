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
            //Get user ID
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            //Get user profile
            var profile = (from p in _db.Users where (p.Id == userId) select p).FirstOrDefault();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserName = profile.Username;
            userViewModel.PhoneNumber = profile.PhoneNumber;
            userViewModel.Name = profile.Name;
            userViewModel.Email = profile.Email;
            userViewModel.DateOfBirth = profile.DOB.Value;
            userViewModel.Address = profile.Address;
            userViewModel.Avatar = profile.Avatar;

            //Get post status list
            var postStatusList = (from p in _db.PostStatuses select p).ToList();
            ViewBag.StatusList = postStatusList;
            //Get user's posts list
            ViewBag.UserId = userId;
            var postList = (from p in _db.Posts where (p.UserId == userId && !p.IsDeleted) select p);
            ViewBag.PostList = postList.ToList();
            
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
            userViewModel.DateOfBirth = profile.DOB.Value;
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
            if (Membership.ValidateUser(userViewModel.UserName, GetMD5Hash(userViewModel.Password)))
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

        public string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
