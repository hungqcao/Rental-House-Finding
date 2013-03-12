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
        //
        // GET: /Post/

        public ActionResult Index(int id)
        {
            
            //if (TempData["IdSuccessPost"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            int postId = 1;//(int)TempData["IdSuccessPost"];
            var post = (from p in _db.Posts where p.Id == id select p).FirstOrDefault();
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
            var images = (from i in _db.PostImages where (i.PostId == post.Id && !i.IsDeleted) select i).ToList();
            ViewBag.Images = images;
            return View(CommonModel.ConvertPostToPostViewModel(post));
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Post/Create

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
        // GET: /Post/Edit/5
 
        public ActionResult Edit(int id)
        {
            var postModel = (from p in _db.Posts where (p.Id == id) && (!p.IsDeleted) select p).FirstOrDefault();
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == postModel.Id && !i.IsDeleted) select i).ToList();
            ViewBag.Images = images;
            return View(CommonModel.ConvertPostToPostViewModel(postModel));
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
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
 
        public ActionResult Delete(int id)
        {
            try
            {
                var post = (from p in _db.Posts where (p.Id == id) select p).FirstOrDefault();
                post.IsDeleted = true;
                _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                _db.SaveChanges();

                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Post/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, PostViewModel postViewModel)
        {
            return View();
        }
    }
}
