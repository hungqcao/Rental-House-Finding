using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class CreatePostController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /CreatePost/

        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.ProvinceId = new SelectList(_db.Provinces, "Id", "Name");
            ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name");
            return View();
        }

        //
        // POST: /CreatePost/Create

        [HttpPost]
        public ActionResult Index(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!CommonModel.FilterHasBadContent(model))
                    {
                        Posts postToCreate = CommonModel.ConvertPostViewModelToPost(model, DateTime.Now, DateTime.Now, DateTime.Now);

                        //1 for submitted
                        postToCreate.StatusId = 1;

                        int userId;
                        if (!string.IsNullOrEmpty(User.Identity.Name))
                        {
                            userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                            postToCreate.UserId = userId;
                        }
                        _db.Posts.AddObject(postToCreate);
                        _db.SaveChanges();
                        TempData["MessageSuccessPostNew"] = "Đăng bài thành công, chúng tôi sẽ gửi tin nhắn đến số điện thoại bạn đã cung cấp";
                        TempData["IdSuccessPost"] = postToCreate.Id;
                        return RedirectToAction("Index", "Post");
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
            if (model.ProvinceId != 0)
            {
                var districtList = _db.Districts.Where(c => c.ProvinceId == model.ProvinceId);
                ViewBag.DistrictId = new SelectList(districtList, "Id", "Name", model.ProvinceId);
            }
            else
            {
                ViewBag.DistrictId = new SelectList(_db.Districts, "Id", "Name", model.DistrictId);
            }
            return View(model);
        }
    }
}
