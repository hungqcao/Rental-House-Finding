using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.RHF.Common;
using System.IO;

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
            return View();
        }

        //
        // POST: /CreatePost/Create

        [HttpPost]
        public ActionResult Index(PostViewModel model, IEnumerable<HttpPostedFileBase> images)
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
                        

                        PostImages imageToCreate = null;
                        
                        if (!(images.Count() == 0 || images == null))
                        {
                            foreach (HttpPostedFileBase image in images)
                            {
                                if (image != null && image.ContentLength > 0)
                                {
                                    var path = Path.Combine(HttpContext.Server.MapPath("/App_Data/Images/"), postToCreate.Id.ToString());
                                    Directory.CreateDirectory(path);
                                    string filePath = Path.Combine(path, Path.GetFileName(image.FileName));
                                    image.SaveAs(filePath);
                                    imageToCreate = new PostImages();
                                    imageToCreate.PostId = postToCreate.Id;
                                    imageToCreate.Path = filePath;
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
    }
}
