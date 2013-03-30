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
                return View(CommonModel.ConvertPostToPostViewModel(post, _noInfo));
            }
        }

        public bool DeleteImage(int id, int postId)
        {
            try
            {
                var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                if (post.UserId == userId)
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
                return false;
            }
        }

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
                        TempData["MessagePendingPostNew"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                        TempData["Pending"] = true;
                    }
                    post = CommonModel.ConvertPostViewModelToPost(post, postViewModel, post.CreatedDate, DateTime.Now, DateTime.Now, _noInfo);
                    

                    Dictionary<int, string> lstNearbyId = GetListNearbyLocations(postViewModel);
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
                    TempData["MessageSuccessPostNew"] = "Thay đổi thông tin thành công";
                    TempData["Success"] = true;
                    return RedirectToAction("Details", new { Id = post.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postViewModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postViewModel.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d => d.ProvinceId == postViewModel.ProvinceId) , "Id", "Name", postViewModel.DistrictId);
            return View(postViewModel);
        }
        private Dictionary<int, string> GetListNearbyLocations(PostViewModel model)
        {
            try
            {
                Dictionary<int, string> lstReturn = new Dictionary<int, string>();
                int id;
                Locations location;
                for (int i = 0; i < Request.Form.Keys.Count; i++)
                {
                    if (Request.Form.Keys[i].Contains("tag["))
                    {
                        if (Request.Form.Keys[i].Equals("tag[]", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] lstValue = Request.Form[i].Trim().Split(',');
                            foreach (string item in lstValue)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    location = new Locations();
                                    location.DistrictId = model.DistrictId;
                                    //1 for Create by User
                                    location.LocationTypeId = 1;
                                    location.Name = item;
                                    _db.Locations.AddObject(location);
                                    _db.SaveChanges();
                                    lstReturn.Add(location.Id, location.Name);
                                }
                            }
                        }
                        else
                        {
                            string idValue = Request.Form.Keys[i].Split('-')[0].Split('[')[1];
                            int.TryParse(idValue, out id);
                            lstReturn.Add(id, Request.Form[i].Trim());
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

    }

}
