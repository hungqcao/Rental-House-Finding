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
using log4net;
using System.Reflection;

namespace RentalHouseFinding.Controllers.GUI
{
    public class PostEditController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        private string _noInfo;
        public ICacheRepository Repository { get; set; }
        public PostEditController()
            : this(new CacheRepository())
        {
        }

        public PostEditController(ICacheRepository repository)
        {
            this.Repository = repository;
            this._noInfo = Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantCommonString.NONE_INFORMATION, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString();
        }
        //
        // GET: /PostEdit/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postEdit = _db.Posts.Where(p => p.Code.Equals(code)).FirstOrDefault();
                    if (postEdit != null)
                    {
                        Session["PostIdToEdit"] = postEdit.Id;
                        return RedirectToAction("Edit", "PostEdit");
                    }
                    else
                    {
                        ModelState.AddModelError("", "");
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
            return View();
        }

        public ActionResult Edit()
        {
            try
            {
                if (Session["PostIdToEdit"] == null)
                {
                    return RedirectToAction("Index", "PostEdit");
                }
                else
                {
                    int postId = (int)Session["PostIdToEdit"];
                    var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                    ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", post.CategoryId);
                    ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", post.District.ProvinceId);
                    ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == post.District.ProvinceId), "Id", "Name", post.DistrictId);
                    return View(CommonModel.ConvertPostToPostViewModel(post, _noInfo));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return View();
            }
        }

        public bool DeleteImage(int id)
        {
            try
            {
                if (Session["PostIdToEdit"] == null)
                {
                    return false;
                }
                int postId = (int)Session["PostIdToEdit"];
                var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                var postImage = _db.PostImages.Where(p => p.Id == id).FirstOrDefault();

                //Delete in Db
                postImage.IsDeleted = true;
                _db.ObjectStateManager.ChangeObjectState(postImage, EntityState.Modified);
                _db.SaveChanges();

                //Delete File
                if (System.IO.File.Exists(HttpContext.Server.MapPath(postImage.Path)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath(postImage.Path));
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
        }

        public ActionResult ViewImage()
        {
            try
            {
                if (Session["PostIdToEdit"] == null)
                {
                    TempData["MessagePendingPostNew"] = "Phiên làm việc của bạn đã hết, mời bạn đăng nhập lại";
                    TempData["Pending"] = true;
                    TempData["Success"] = false;
                    return RedirectToAction("Index");
                }
                //Get images
                int postId = (int)Session["PostIdToEdit"];
                var images = (from i in _db.PostImages where (i.PostId == postId && !i.IsDeleted) select i);
                if (images != null)
                {
                    ViewBag.Images = images.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel postViewModel, IEnumerable<HttpPostedFileBase> images)
        {
            if (images.Count() > 10)
            {
                ModelState.AddModelError("", "Số lượng ảnh vượt quá 10");
                ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postViewModel.CategoryId);
                ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postViewModel.ProvinceId);
                ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postViewModel.ProvinceId), "Id", "Name", postViewModel.DistrictId);
                return View(postViewModel);
            }
            if (ModelState.IsValid)
            {
                postViewModel = (PostViewModel)CommonModel.TrimObjectProperties(postViewModel);
                try
                {
                    if (Session["PostIdToEdit"] == null)
                    {
                        TempData["MessagePendingPostNew"] = "Phiên làm việc của bạn đã hết, mời bạn đăng nhập lại";
                        TempData["Pending"] = true;
                        TempData["Success"] = false;
                        return RedirectToAction("Index");
                    }
                    var post = (from p in _db.Posts where (p.Id == postViewModel.Id) select p).FirstOrDefault();
                    bool isPending = false;
                    int currentPostStatusID = post.StatusId;
                    TimeSpan keepPendingDay;
                    DateTime expiredDate = DateTime.Now;

                    if (currentPostStatusID == StatusConstant.PENDING)
                    {
                        if (post.EditedDate == null)
                        {
                            keepPendingDay = DateTime.Now - post.CreatedDate;
                        }
                        else
                        {
                            keepPendingDay = DateTime.Now - (DateTime)post.EditedDate;
                        }
                        expiredDate = post.ExpiredDate.AddDays(keepPendingDay.Days);
                    }
                    else
                    {
                        expiredDate = post.ExpiredDate;
                    }
                    if (CommonModel.FilterHasBadContent(postViewModel))
                    {
                        post.StatusId = StatusConstant.PENDING;
                        TempData["MessagePendingPostNew"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                        TempData["Pending"] = true;
                        TempData["Success"] = false;
                        isPending = true;
                    }                    
                    if (isPending)
                    {
                        post.StatusId = StatusConstant.PENDING;
                    }
                    else
                    {
                        if (currentPostStatusID == StatusConstant.PENDING)
                        {
                            post.StatusId = StatusConstant.ACTIVATED;                            
                        }
                        else
                        {
                            post.StatusId = currentPostStatusID;

                        }
                    }
                    post = CommonModel.ConvertPostViewModelToPost(post, postViewModel, post.CreatedDate, DateTime.Now, post.RenewDate, expiredDate, _noInfo);
                    

                    Dictionary<int, string> lstNearbyId = CommonController.GetListNearbyLocations(postViewModel, Request);
                    PostLocations postLocation;
                    string nearbyPlace = string.Empty;
                    Locations loc = null;
                    foreach (KeyValuePair<int, string> kvp in lstNearbyId)
                    {
                        loc = _db.Locations.Where(l => l.Id == kvp.Key).FirstOrDefault();
                        if (loc != null)
                        {
                            postLocation = new PostLocations();
                            postLocation.PostId = post.Id;
                            postLocation.LocationId = kvp.Key;
                            _db.PostLocations.AddObject(postLocation);
                            _db.SaveChanges();
                        }
                        nearbyPlace += kvp.Value + ", ";
                    }
                    if (!string.IsNullOrEmpty(nearbyPlace))
                    {
                        nearbyPlace = nearbyPlace.Remove(nearbyPlace.Length - 2);
                        post.NearbyPlace = nearbyPlace;
                        if (CommonModel.FilterHasBadContent(nearbyPlace))
                        {
                            post.StatusId = StatusConstant.PENDING;
                            TempData["MessagePendingPostNew"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                            TempData["Pending"] = true;
                            TempData["Success"] = false;
                        }
                    }

                    _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                    _db.SaveChanges();

                    PostImages imageToCreate = null;
                    if (!(images == null || images.Count() == 0))
                    {
                        foreach (HttpPostedFileBase image in images)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                var path = Path.Combine(HttpContext.Server.MapPath(DefaultValue.PATH_IMAGES), postViewModel.Id.ToString());
                                //Check if image already exists
                                string pathToCompare = DefaultValue.PATH_IMAGES 
                                                        + postViewModel.Id + "/" + Path.GetFileName(image.FileName) ;
                                var imagesPath = (from i in _db.PostImages 
                                                  where (i.Path.Equals(pathToCompare, StringComparison.CurrentCultureIgnoreCase))
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
                                imageToCreate.Path = DefaultValue.PATH_IMAGES + postViewModel.Id.ToString() + "/" + Path.GetFileName(image.FileName);
                                imageToCreate.IsDeleted = false;

                                _db.PostImages.AddObject(imageToCreate);
                                _db.SaveChanges();
                            }
                        }
                    }
                   if (!isPending)
                    {
                        TempData["MessageSuccessEdit"] = "Thay đổi thông tin thành công.";
                        TempData["Success"] = true;
                    }

                    Session["PostIdToEdit"] = null;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postViewModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postViewModel.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postViewModel.ProvinceId) , "Id", "Name", postViewModel.DistrictId);
            return View(postViewModel);
        }

        public ActionResult LogOut()
        {
            Session["PostIdToEdit"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            try
            {
                if (Session["PostIdToEdit"] == null)
                {
                    TempData["MessagePendingPostNew"] = "Phiên làm việc của bạn đã hết, mời bạn đăng nhập lại";
                    TempData["Pending"] = true;
                    TempData["Success"] = false;
                    return RedirectToAction("Index");
                }
                int postId = (int)Session["PostIdToEdit"];
                var post = (from p in _db.Posts where (p.Id == postId) select p).FirstOrDefault();
                post.IsDeleted = true;
                _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                _db.SaveChanges();
                TempData["MessageDeletePost"] = "Xóa thành công!";

                Session["PostIdToEdit"] = null;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return View();
            }
        }
    }

}
