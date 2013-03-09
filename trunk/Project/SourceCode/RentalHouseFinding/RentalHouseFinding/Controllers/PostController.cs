using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class PostController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /Post/

        public ActionResult Index()
        {
            
            //if (TempData["IdSuccessPost"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            int postId = 1;//(int)TempData["IdSuccessPost"];
            var post = (from p in _db.Posts where p.Id == postId select p).FirstOrDefault();
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
            
            return View(CommonModel.ConvertPostToPostViewModel(post));
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Post/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Post/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
