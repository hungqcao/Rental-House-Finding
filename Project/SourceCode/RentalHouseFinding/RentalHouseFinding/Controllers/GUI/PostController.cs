﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.RHF.Common;
using System.Data;
using System.IO;

namespace RentalHouseFinding.Controllers
{
    public class PostController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        //
        // GET: /Post/Details/5

        public ActionResult Details(int id)
        {
            var post = (from p in _db.Posts where p.Id == id select p).FirstOrDefault();
            if (post == null)
            {
                return View();
            }
            var districtAndProvinceName = (from d in _db.Districts 
                                where d.Id == post.DistrictId
                                select new { districtName = d.Name , provinceName= d.Province.Name}).FirstOrDefault();            
            ViewBag.Address = districtAndProvinceName.districtName + ", " + districtAndProvinceName.provinceName;
            ViewBag.Internet = post.Facilities.HasInternet ? "Có" : "Không";
            ViewBag.AirConditioner = post.Facilities.HasAirConditioner ? "Có" : "Không";
            ViewBag.Bed = post.Facilities.HasBed ? "Có" : "Không";
            ViewBag.Gara = post.Facilities.HasGarage ? "Có" : "Không";
            ViewBag.MotorParkingLot = post.Facilities.HasMotorParkingLot ? "Có" : "Không";
            ViewBag.Security = post.Facilities.HasSecurity ? "Có" : "Không";
            ViewBag.Toilet = post.Facilities.HasToilet ? "Có" : "Không";
            ViewBag.TVCable = post.Facilities.HasTVCable ? "Có" : "Không";
            ViewBag.WaterHeater = post.Facilities.HasWaterHeater ? "Có" : "Không";
            ViewBag.AllowCooking = post.Facilities.IsAllowCooking ? "Có" : "Không";
            ViewBag.StayWithOwner = post.Facilities.IsStayWithOwner ? "Có" : "Không";
            ViewBag.WaterHeater = post.Facilities.HasWaterHeater ? "Có" : "Không";
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == post.Id && !i.IsDeleted) select i);
            if (images != null)
            {
                ViewBag.Images = images.ToList();
            }
            //Check if the post is in favorite list
            if (User.Identity.IsAuthenticated)
            {
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                var favorite = (from f in _db.Favorites where 
                                    (f.PostId == post.Id && f.UserId == userId && !f.IsDeleted) select f).ToList();
                if (favorite.Count > 0)
                {
                    ViewBag.RemoveFavorite = true;
                }
                else
                {
                    ViewBag.RemoveFavorite = false;
                }
            }
            TempData["PostID"] = post.Id;
            return View(CommonModel.ConvertPostToPostViewModel(post));
        }

        //
        // GET: /Post/Create
		
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
            return View();
        }

        //
        // POST: /Post/Create

        [HttpPost]
        public ActionResult Create(PostViewModel model, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!CommonModel.FilterHasBadContent(model))
                    {
                        Posts postToCreate = CommonModel.ConvertPostViewModelToPost(model, DateTime.Now, DateTime.Now, DateTime.Now);

                        if (CommonModel.FilterHasBadContent(model))
                        {
                            //2 for pending
                            postToCreate.StatusId = 2;
                            TempData["MessageSuccessPostNew"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                        }
                        else
                        {
                            //1 for submitted
                            postToCreate.StatusId = 1;
                            TempData["MessageSuccessPostNew"] = "Đăng bài thành công, chúng tôi sẽ gửi tin nhắn đến số điện thoại bạn đã cung cấp";
                        }

                        int userId;
                        if (!string.IsNullOrEmpty(User.Identity.Name))
                        {
                            userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                            postToCreate.UserId = userId;
                        }
                        _db.Posts.AddObject(postToCreate);
                        _db.SaveChanges();
                        
                        //Nearby places
                        List<int> lstNearbyId = GetListNearbyLocations(model);
                        PostLocations postLocation;
                        foreach (int i in lstNearbyId)
                        {
                            postLocation = new PostLocations();
                            postLocation.PostId = postToCreate.Id;
                            postLocation.LocationId = i;
                            _db.PostLocations.AddObject(postLocation);
                            _db.SaveChanges();
                        }

                        //Images post
                        PostImages imageToCreate = null;

                        if (!(images.Count() == 0 || images == null))
                        {
                            foreach (HttpPostedFileBase image in images)
                            {
                                if (image != null && image.ContentLength > 0)
                                {
                                    var path = Path.Combine(HttpContext.Server.MapPath("/Content/PostImages/"), postToCreate.Id.ToString());
                                    Directory.CreateDirectory(path);
                                    string filePath = Path.Combine(path, Path.GetFileName(image.FileName));
                                    image.SaveAs(filePath);
                                    imageToCreate = new PostImages();
                                    imageToCreate.PostId = postToCreate.Id;
                                    imageToCreate.Path = "/Content/PostImages/" + postToCreate.Id.ToString() + "/" + Path.GetFileName(image.FileName);
                                    imageToCreate.IsDeleted = false;

                                    _db.PostImages.AddObject(imageToCreate);
                                    _db.SaveChanges();
                                }
                            }
                        }

                        return RedirectToAction("Details", "Post", new { id = postToCreate.Id });
                    }
                    else
                    {
                        //If model contain bad word
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", model.CategoryId);
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name", model.ProvinceId);
            return View(model);
        }
        
        //
        // GET: /Post/Edit/5
		[Authorize(Roles = "User")]
        public ActionResult Edit(int id)
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            var postModel = (from p in _db.Posts where (p.Id == id) 
                                 && (!p.IsDeleted)  && p.UserId == userId select p).FirstOrDefault();
            //Check if the post belongs to current user
            if (postModel == null)
            {
                return View();
            }
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == postModel.Id && !i.IsDeleted) select i);
            if (images != null)
            {
                ViewBag.Images = images.ToList();
            }
            return View(CommonModel.ConvertPostToPostViewModel(postModel));
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
		[Authorize(Roles = "User")]
        public ActionResult Edit(int id, PostViewModel postViewModel, IEnumerable<HttpPostedFileBase> images)
        {
            var post = (from p in _db.Posts where (p.Id == id) select p).FirstOrDefault();
            post = CommonModel.ConvertPostViewModelToPost(post, postViewModel, post.CreatedDate, DateTime.Now, DateTime.Now);
            _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
            _db.SaveChanges();

            PostImages imageToCreate = null;
            if (!(images.Count() == 0 || images == null))
            {
                foreach (HttpPostedFileBase image in images)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var path = Path.Combine(HttpContext.Server.MapPath("/Content/PostImages/"), id.ToString());
                        //Check if image already exists
                        var imagesPath = (from i in _db.PostImages 
                                          where (i.Path == "/Content/PostImages/" 
                                                + id.ToString() + "/" + Path.GetFileName(image.FileName)) 
                                          select i.Path).ToArray();
                        if (imagesPath.Count() > 0)
                        {
                            break;
                        }
                        Directory.CreateDirectory(path);
                        string filePath = Path.Combine(path, Path.GetFileName(image.FileName));
                        image.SaveAs(filePath);
                        imageToCreate = new PostImages();
                        imageToCreate.PostId = id;
                        imageToCreate.Path = "/Content/PostImages/" + id.ToString() + "/" + Path.GetFileName(image.FileName);
                        imageToCreate.IsDeleted = false;

                        _db.PostImages.AddObject(imageToCreate);
                        _db.SaveChanges();
                    }
                }
            }
            TempData["MessageSuccessEdit"] = "Success";
            return RedirectToAction("Index/" + id);
        }

        //
        // GET: /Post/Delete/5
		[Authorize(Roles = "User")]
        public ActionResult Delete(int id)
        {
            try
            {
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                var post = (from p in _db.Posts where (p.Id == id) select p).FirstOrDefault();
                //Check if the post belongs to current user
                if (post.UserId == userId)
                {
                    post.IsDeleted = true;
                    _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View();
            }
        }

        private List<int> GetListNearbyLocations(PostViewModel model)
        {
            try
            {
                List<int> lstReturn = new List<int>();
                int id;
                Locations location;
                for (int i = 0; i < Request.Form.Keys.Count; i++)
                {
                    if (Request.Form.Keys[i].Contains("idNearby"))
                    {
                        int.TryParse(Request.Form.Keys[i].Split(':')[1], out id);
                        if (id == -1)
                        {
                            if(!string.IsNullOrEmpty(Request.Form.Keys[i].Split(':')[2].Trim()))
                            {
                                location = new Locations();
                                location.DistrictId = model.DistrictId;
                                //1 for Create by User
                                location.LocationTypeId = 1;
                                location.Name = Request.Form.Keys[i].Split(':')[2];
                                _db.Locations.AddObject(location);
                                _db.SaveChanges();
                                lstReturn.Add(location.Id);
                            }
                        }
                        else
                        {
                            lstReturn.Add(id);
                        }
                    }
                }
                return lstReturn;
            }
            catch
            {
                return null;
            }
        }
		
		[Authorize(Roles = "User")]
        public JsonResult AddFavorite(PostViewModel postViewModel)
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            int postId = Convert.ToInt32(TempData["PostID"]);

            //Check if user has added this post to favorite, but deleted
            var favorite = (from f in _db.Favorites
                            where
                                (f.PostId == postId && f.UserId == userId && f.IsDeleted)
                            select f).FirstOrDefault();

            //If user never added this post to favorite then add new entry
            if (favorite == null)
            {
                favorite = new Favorites();
                favorite.UserId = userId;
                favorite.PostId = postId;
                favorite.AddedDate = DateTime.Now;
                favorite.IsDeleted = false;
                _db.Favorites.Attach(favorite);
                _db.ObjectStateManager.ChangeObjectState(favorite, EntityState.Added);
            }
            //If user has added this post to favorite before, set deleted to false
            else 
            {
                favorite.IsDeleted = false;
                _db.ObjectStateManager.ChangeObjectState(favorite, EntityState.Modified);
            }
            var success = false;
            try
            {
                //Temp data is reset so we have to set its value again
                TempData["PostID"] = postId;
                _db.SaveChanges();
                success = true;
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            
        }

        [Authorize(Roles = "User")]
        public JsonResult RemoveFavorite()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            int postId = Convert.ToInt32(TempData["PostID"]);
            var success = false;

            //Get favorite from db
            var favorite = (from f in _db.Favorites where
                                (f.PostId == postId && f.UserId == userId && !f.IsDeleted)
                            select f).FirstOrDefault();

            if (favorite == null)
            {
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            favorite.IsDeleted = true;
            
            try
            {
                //Temp data is reset so we have to set its value again
                TempData["PostID"] = postId;
                _db.ObjectStateManager.ChangeObjectState(favorite, EntityState.Modified);
                _db.SaveChanges();
                success = true;
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(success, JsonRequestBehavior.AllowGet);
            }
        }
    }
}