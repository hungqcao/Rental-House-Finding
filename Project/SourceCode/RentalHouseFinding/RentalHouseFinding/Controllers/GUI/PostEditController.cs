using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;
using System.IO;
using System.Data;
using RentalHouseFinding.Caching;

namespace RentalHouseFinding.Controllers.GUI
{
    public class PostEditController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
     
        public ICacheRepository Repository { get; set; }
        public PostEditController()
            : this(new CacheRepository())
        {
        }

        public PostEditController(ICacheRepository repository)
        {
            this.Repository = repository;
        }
        //
        // GET: /PostEdit/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PostEditModel model)
        {
            if (ModelState.IsValid)
            {
                var postEdit = _db.PostEdits.Where(p => p.PostId == model.PostId && p.Password.Equals(model.Password, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (postEdit != null)
                {
                    TempData["PostIdToEdit"] = postEdit.PostId;
                    return RedirectToAction("Edit", "PostEdit");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công, vui lòng kiểm tra lại thông tin");
                }
            }
            return View(model);
        }

        public ActionResult Edit()
        {
            if (TempData["PostIdToEdit"] == null)
            {
                return RedirectToAction("Index", "PostEdit");
            }
            else
            {
                int postId = (int)TempData["PostIdToEdit"];
                var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", post.CategoryId);
                ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", post.District.ProvinceId);
                ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d=>d.ProvinceId == post.District.ProvinceId), "Id", "Name", post.DistrictId);
                return View(CommonModel.ConvertPostToPostViewModel(post));
            }
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel postViewModel, IEnumerable<HttpPostedFileBase> images)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var post = (from p in _db.Posts where (p.Id == postViewModel.Id) select p).FirstOrDefault();
                    if (CommonModel.FilterHasBadContent(postViewModel))
                    {
                        //2 for pending
                        post.StatusId = 2;
                        TempData["MessageSuccessEdit"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                    }
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
                                var path = Path.Combine(HttpContext.Server.MapPath("/Content/PostImages/"), postViewModel.Id.ToString());
                                //Check if image already exists
                                var imagesPath = (from i in _db.PostImages
                                                  where (i.Path == "/Content/PostImages/"
                                                        + postViewModel.Id.ToString() + "/" + Path.GetFileName(image.FileName))
                                                  select i.Path).ToArray();
                                if (imagesPath.Count() > 0)
                                {
                                    break;
                                }
                                Directory.CreateDirectory(path);
                                string filePath = Path.Combine(path, Path.GetFileName(image.FileName));
                                image.SaveAs(filePath);
                                imageToCreate = new PostImages();
                                imageToCreate.PostId = postViewModel.Id;
                                imageToCreate.Path = "/Content/PostImages/" + postViewModel.Id.ToString() + "/" + Path.GetFileName(image.FileName);
                                imageToCreate.IsDeleted = false;

                                _db.PostImages.AddObject(imageToCreate);
                                _db.SaveChanges();
                            }
                        }
                    }
                    TempData["MessageSuccessEdit"] = "Thay đổi thông tin thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postViewModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postViewModel.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postViewModel.ProvinceId), "Id", "Name", postViewModel.DistrictId);
            return View(postViewModel);
        }

    }
}
