using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Reflection;
using RentalHouseFinding.Models;
using RentalHouseFinding.Caching;
using RentalHouseFinding.Common;
using System.Web.Helpers;
using System.Data;
using System.IO;

namespace RentalHouseFinding.Controllers.Admin
{
    public class ManageReportPostController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        private const int MAX_RECORD_PER_PAGE = 15;

        private string _noInfo;

        public ICacheRepository Repository { get; set; }
        public ManageReportPostController()
            : this(new CacheRepository())
        {
        }

        public ManageReportPostController(ICacheRepository repository)
        {
            this.Repository = repository;
            this._noInfo = Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantCommonString.NONE_INFORMATION, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString();
        }

        //
        // GET: /ManageBadpost/
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var postsList = (from p in _db.ReportedPosts where (p.IsIgnored == false) select p);

            var postViewList = postsList.Select(p => new
            {
                ID = p.Id,
                p.PostId,
                p.Post.Title,
                p.Reason,
                p.User.Username,
                p.ReportedDate
            }).OrderBy(p => p.ID).Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);

            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid",
            canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(postViewList, autoSortAndPage: false, rowCount: postsList.Count());
            ViewBag.Index = ((int)page - 1) * MAX_RECORD_PER_PAGE;
            ViewBag.Grid = grid;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, int reportId)
        {
            var postModel = (from p in _db.Posts
                             where (p.Id == id)
                                 && (!p.IsDeleted)
                             select p).FirstOrDefault();
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postModel.District.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postModel.District.ProvinceId), "Id", "Name", postModel.DistrictId);
            TempData["ReportId"] = reportId;
            return View(CommonModel.ConvertPostToPostViewModel(postModel, _noInfo));
        }

        [Authorize(Roles = "Admin")]
        public bool DeleteImage(int id, int postId)
        {
            try
            {
                var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                if (post != null)
                {
                    var postImage = _db.PostImages.Where(p => p.Id == id).FirstOrDefault();
                    postImage.IsDeleted = true;

                    _db.ObjectStateManager.ChangeObjectState(postImage, System.Data.EntityState.Modified);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewImage(int id)
        {
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == id && !i.IsDeleted) select i);
            if (images != null)
            {
                ViewBag.Images = images.ToList();
            }
            return View();
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(PostViewModel postViewModel, IEnumerable<HttpPostedFileBase> images)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var post = (from p in _db.Posts where (p.Id == postViewModel.Id) select p).FirstOrDefault();
                    post = CommonModel.ConvertPostViewModelToPost(post, postViewModel, post.CreatedDate, DateTime.Now, DateTime.Now, _noInfo);

                    Dictionary<int, string> lstNearbyId = CommonController.GetListNearbyLocations(postViewModel, Request);
                    PostLocations postLocation;
                    string nearbyPlace = string.Empty;
                    Locations loc = null;
                    foreach (KeyValuePair<int, string> kvp in lstNearbyId)
                    {
                        loc = _db.Locations.Where(l => l.Id == kvp.Key).FirstOrDefault();
                        if (loc == null)
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
                    }

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
                                string pathToCompare = "/Content/PostImages/"
                                                        + postViewModel.Id + "/" + Path.GetFileName(image.FileName);
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
                                imageToCreate.Path = "/Content/PostImages/" + postViewModel.Id.ToString() + "/" + Path.GetFileName(image.FileName);
                                imageToCreate.IsDeleted = false;

                                _db.PostImages.AddObject(imageToCreate);
                                _db.SaveChanges();
                            }
                        }
                    }
                    TempData["MessageSuccessSaveBadPost"] = "Thay đổi thông tin thành công";
                    var reportPost = _db.ReportedPosts.Where(p => p.Id == (int)TempData["ReportId"]).FirstOrDefault();
                    reportPost.IsIgnored = true;

                    _db.ObjectStateManager.ChangeObjectState(reportPost, System.Data.EntityState.Modified);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postViewModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postViewModel.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postViewModel.ProvinceId), "Id", "Name", postViewModel.DistrictId);
            return View(postViewModel);
        }

        //
        // GET: /Post/Delete/5
        [Authorize(Roles = "Admin")]
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
                TempData["MessageSuccessPostNew"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //Error
                log.Error(ex.ToString());
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}
