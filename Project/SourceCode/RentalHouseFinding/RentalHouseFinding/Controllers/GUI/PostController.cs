using System;
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
            
            //if (TempData["IdSuccessPost"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //int postId = 1;//(int)TempData["IdSuccessPost"];
            var post = (from p in _db.Posts where p.Id == id select p).FirstOrDefault();
            var provinceName = (from d in _db.Districts 
                                where d.Id == post.DistrictId
                                select d.Province.Name).FirstOrDefault();
            ViewBag.Province = provinceName;         
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == post.Id && !i.IsDeleted) select i);
            if (images != null)
            {
                ViewBag.Images = images.ToList();
            }
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

                        return RedirectToAction("Index", "Post", new { id = postToCreate.Id });
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
 
        public ActionResult Edit(int id)
        {
            var postModel = (from p in _db.Posts where (p.Id == id) && (!p.IsDeleted) select p).FirstOrDefault();
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
