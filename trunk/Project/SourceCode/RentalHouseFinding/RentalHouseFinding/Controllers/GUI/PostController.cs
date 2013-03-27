using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;
using System.Data;
using System.IO;
using RentalHouseFinding.Caching;

namespace RentalHouseFinding.Controllers
{
    public class PostController : Controller
    {
        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public PostController()
            : this(new CacheRepository())
        {
        }

        public PostController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

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
            var districtAndProvinceName = Repository.GetAllDistricts().Where(d => d.Id == post.DistrictId).Select(d => new { districtName = d.Name, provinceName = d.Province.Name }).FirstOrDefault();
                   
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
            TempData["CreatedUserId"] = post.UserId;

            Session["PostID"] = post.Id;
            Session["CreatedUserId"] = post.UserId;

            return View(CommonModel.ConvertPostToPostViewModel(post));
        }

        public ActionResult DetailsBox(int id)
        {
            var post = (from p in _db.Posts where p.Id == id select p).FirstOrDefault();
            if (post == null)
            {
                return View();
            }
            var districtAndProvinceName = Repository.GetAllDistricts().Where(d => d.Id == post.DistrictId).Select(d => new { districtName = d.Name, provinceName = d.Province.Name }).FirstOrDefault();
                 
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
                var favorite = (from f in _db.Favorites
                                where
                                    (f.PostId == post.Id && f.UserId == userId && !f.IsDeleted)
                                select f).ToList();
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
            TempData["CreatedUserId"] = post.UserId;

            Session["PostID"] = post.Id;
            Session["CreatedUserId"] = post.UserId;

            return View(CommonModel.ConvertPostToPostViewModel(post));
        }

        //
        // GET: /Post/Create
		
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name");
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name");
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts(), "Id", "Name");
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
                    string strExpiredDate = Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantCommonString.EXPIRED_DATE, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString();
                    int numberExpiredDate = 0;
                    int.TryParse(strExpiredDate, out numberExpiredDate);
                    Posts postToCreate = CommonModel.ConvertPostViewModelToPost(model, DateTime.Now, DateTime.Now, DateTime.Now, 
                                DateTime.Now.AddDays(numberExpiredDate),
                                Repository.GetAllConfiguration().Where( c => c.Name.Equals(ConstantCommonString.NONE_INFORMATION, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString());

                    if (CommonModel.FilterHasBadContent(model))
                    {
                        //2 for pending
                        postToCreate.StatusId = 2;
                        TempData["MessagePendingPostNew"] = "Bài đăng có chứa những từ không cho phép, chúng tôi sẽ duyệt trước khi đăng lên hệ thống";
                        TempData["Pending"] = true;
                    }
                    else
                    {
                        //1 for submitted
                        postToCreate.StatusId = 1;
                        TempData["MessageSuccessPostNew"] = "Đăng bài thành công, chúng tôi sẽ gửi tin nhắn đến số điện thoại bạn đã cung cấp";
                        TempData["Success"] = true;
                    }

                    int userId;
                    if (!string.IsNullOrEmpty(User.Identity.Name))
                    {
                        userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
                        postToCreate.UserId = userId;
                    }
                    _db.Posts.AddObject(postToCreate);
                    _db.SaveChanges();

                    if (string.IsNullOrEmpty(User.Identity.Name))
                    {
                        //Visitor post 
                        PostEdit postEdit = new PostEdit();
                        postEdit.PostId = postToCreate.Id;
                        postEdit.Password = StringUtil.RandomStr();
                        _db.PostEdits.AddObject(postEdit);
                        _db.SaveChanges();
                    }
                    //Nearby places
                    Dictionary<int, string> lstNearbyId = GetListNearbyLocations(model);
                    PostLocations postLocation;
                    string nearbyPlace = string.Empty;
                    foreach (KeyValuePair<int,string> kvp in lstNearbyId)
                    {
                        postLocation = new PostLocations();
                        postLocation.PostId = postToCreate.Id;
                        postLocation.LocationId = kvp.Key;
                        _db.PostLocations.AddObject(postLocation);
                        _db.SaveChanges();
                        nearbyPlace += kvp.Value + ",";
                    }
                    if (!string.IsNullOrEmpty(nearbyPlace))
                    {
                        nearbyPlace = nearbyPlace.Remove(nearbyPlace.Length - 1);
                        postToCreate.NearbyPlace = nearbyPlace;
                        _db.ObjectStateManager.ChangeObjectState(postToCreate, System.Data.EntityState.Modified);
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException);
                }
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", model.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", model.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d=>d.ProvinceId == model.ProvinceId), "Id", "Name", model.DistrictId);
            return View(model);
        }

        //
        // GET: /Post/Edit/5
		[Authorize(Roles = "User, Admin")]
        public ActionResult Edit(int id)
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            var postModel = (from p in _db.Posts where (p.Id == id) 
                                 && (!p.IsDeleted)  && p.UserId == userId select p).FirstOrDefault();
            //Check if the post belongs to current user
            if (postModel == null)
            {
                return RedirectToAction("Index", "Landing");
            }
            //Get images
            var images = (from i in _db.PostImages where (i.PostId == postModel.Id && !i.IsDeleted) select i);
            if (images != null)
            {
                ViewBag.Images = images.ToList();
            }
            ViewBag.CategoryId = new SelectList(Repository.GetAllCategories(), "Id", "Name", postModel.CategoryId);
            ViewBag.ProvinceId = new SelectList(Repository.GetAllProvinces(), "Id", "Name", postModel.District.ProvinceId);
            ViewBag.DistrictId = new SelectList(Repository.GetAllDistricts().Where(d=>d.ProvinceId == postModel.District.ProvinceId), "Id", "Name", postModel.DistrictId);
            return View(CommonModel.ConvertPostToPostViewModel(postModel));
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(PostViewModel postViewModel, IEnumerable<HttpPostedFileBase> images)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    int status = 0;
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

        //
        // GET: /Post/Delete/5
        [Authorize(Roles = "User, Admin")]
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

        private Dictionary<int, string> GetListNearbyLocations(PostViewModel model)
        {
            try
            {
                Dictionary<int, string> lstReturn = new Dictionary<int, string>();
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
                                lstReturn.Add(location.Id, location.Name);
                            }
                        }
                        else
                        {
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

        [Authorize(Roles = "User, Admin")]
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

        [Authorize(Roles = "User, Admin")]
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

        [HttpPost]
        public ActionResult SendQuestion(QuestionViewModel model)
        {
            try
            {
                int postId = Convert.ToInt32(Session["PostID"]);
                Users user = null;
                
                var post = _db.Posts.Where( p => p.Id == postId).FirstOrDefault();
                if (post.UserId != null)
                {
                    user = _db.Users.Where(u => u.Id == post.UserId).FirstOrDefault();
                }

                Questions questionToCreate = new Questions();
                questionToCreate.Content = model.ContentQuestion.Trim();
                questionToCreate.Title = model.TitleQuestion.Trim();
                questionToCreate.CreatedDate = DateTime.Now;
                questionToCreate.IsDeleted = false;
                questionToCreate.IsRead = false;
                questionToCreate.PostId = postId;
                if (model.UserId != 0)
                {
                    questionToCreate.SenderId = model.UserId;
                }
                else
                {
                    questionToCreate.SenderId = null;
                }
                questionToCreate.SenderEmail = model.Email;

                _db.Questions.AddObject(questionToCreate);
                _db.SaveChanges();

                string message = string.Empty;
                if (!(user == null))
                {
                    string emailTemplate = Repository.GetAllEmailTemplate().Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_QUESTION, StringComparison.CurrentCultureIgnoreCase)).Select(m => m.Template).FirstOrDefault();
                    message = string.Format(emailTemplate, model.Email, model.TitleQuestion, model.ContentQuestion, post.Title);
                    CommonModel.SendEmail(user.Email, message, "Bạn nhận được 1 câu hỏi", 0);
                }

                if (user != null)
                {
                    UserLogs log = new UserLogs();
                    log.UserId = user.Id;
                    log.Message = message;
                    log.IsRead = false;
                    log.CreatedDate = DateTime.Now;
                    _db.UserLogs.AddObject(log);
                    _db.SaveChanges();
                }
                return Content("Thông tin đã được gửi đi", "text/html");
            }
            catch
            {
                return Content("Có lỗi xảy ra!", "text/html");
            }
            
        }
    }
}
