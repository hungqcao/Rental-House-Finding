using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RentalHouseFinding.Common;
using RentalHouseFinding.Models;
using RentalHouseFinding.Sercurity;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Facebook;
using System.Net;
using FBLogin.Models;
using Recaptcha;
using RentalHouseFinding.Caching;
using log4net;
using System.Reflection;



namespace RentalHouseFinding.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ICacheRepository Repository { get; set; }
        public AccountController()
            : this(new CacheRepository())
        {
        }

        public AccountController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult FacebookUserDetail(string token, string returnUrl)
        {
            try
            {
                FacebookClient.SetDefaultHttpWebRequestFactory(uri =>
                {
                    var request = new HttpWebRequestWrapper((HttpWebRequest)WebRequest.Create(uri));
                    return request;
                });
                if (!String.IsNullOrEmpty(token))
                {
                    var client = new FacebookClient(token);//Session["accessToken"].ToString());
                    dynamic fbresult = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,username,timezone,location,picture");

                    UserDetailsModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetailsModel>(fbresult.ToString());
                    return FBookOrOpenIdLogon(facebookUser, 1, returnUrl);
                }
                else
                {
                    //fail
                    return RedirectToAction("LogOn", "Account");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return RedirectToAction("LogOn", "Account");
            }
            
        }
        
        //Logon by Openid
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        public ActionResult Authenticate(string returnUrl)
        {
            var response = openid.GetResponse();

            if (response == null)
            {
                //Let us submit the request to OpenID provider
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        var request = openid.CreateRequest(Request.Form["openid_identifier"]);
                        var fetch = new FetchRequest();
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Email, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.BirthDate.DayOfMonth, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Person.Gender, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.First, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.Last, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Phone.Mobile, true));
                        
                        request.AddExtension(fetch);
                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewBag.Message = ex.Message;
                        log.Error(ex.Message);
                        return View("LogOn");
                    }
                }

                ViewBag.Message = "Invalid identifier";
                return View("LogOn");
            }

            //Let us check the response
            switch (response.Status)
            {

                case AuthenticationStatus.Authenticated:
                    var fetch = response.GetExtension<FetchResponse>();
                    var sFirstName = "";
                    var sEmail = "";
                    var sLastName = "";
                    var sGender = "";
                    var sDob = "";
                    var sPhoneNumber = "";
                    if (fetch != null)
                    {
                        foreach (var vAtrrib in fetch.Attributes)
                        {
                            switch (vAtrrib.TypeUri)
                            {
                                case WellKnownAttributes.Name.First:
                                    var firstNames = fetch.Attributes[WellKnownAttributes.Name.First].Values;
                                    sFirstName = firstNames.Count > 0 ? firstNames[0] : null;
                                    break;
                                case WellKnownAttributes.Contact.Email:
                                    var emailAddresses = fetch.Attributes[WellKnownAttributes.Contact.Email].Values;
                                    sEmail = emailAddresses.Count > 0 ? emailAddresses[0] : null;
                                    break;
                                case WellKnownAttributes.Name.Last:
                                    var lastNames = fetch.Attributes[WellKnownAttributes.Name.Last].Values;
                                    sLastName = lastNames.Count > 0 ? lastNames[0] : null;
                                    break;
                                case WellKnownAttributes.Person.Gender:
                                    var genders = fetch.Attributes[WellKnownAttributes.Person.Gender].Values;
                                    sGender = genders.Count > 0 ? genders[0] : null;
                                    break;
                                case WellKnownAttributes.BirthDate.DayOfMonth:
                                    var doBs = fetch.Attributes[WellKnownAttributes.BirthDate.DayOfMonth].Values;
                                    sDob = doBs.Count > 0 ? doBs[0] : null;
                                    break;
                                case WellKnownAttributes.Contact.Phone.Mobile:
                                    var phoneNumbers = fetch.Attributes[WellKnownAttributes.Contact.Phone.Mobile].Values;
                                    sPhoneNumber = phoneNumbers.Count > 0 ? phoneNumbers[0] : null;
                                    break;                                
                            }
                        }
                    }
                    var sFriendlyLogin = response.FriendlyIdentifierForDisplay;
                    var lm = new UserDetailsModel
                    {
                        id = response.ClaimedIdentifier,
                        first_name = sFirstName,
                        last_name = sLastName,
                        gender = sGender,
                        email = sEmail,
                        phoneMumber = sPhoneNumber,
                        user_birthday = sDob

                    };
                    return FBookOrOpenIdLogon(lm,2, returnUrl);

                case AuthenticationStatus.Canceled:
                    ViewBag.Message = "Canceled at provider";
                    return View("LogOn");
                case AuthenticationStatus.Failed:
                    ViewBag.Message = response.Exception.Message;
                    return View("LogOn");
            }

            return new EmptyResult();
        }
        public ActionResult FBookOrOpenIdLogon(UserDetailsModel userDetail, int type, string returnUrl)
        {
            try
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                CustomMembershipProvider customMP = new CustomMembershipProvider();
                customMP.CreateUserForOpenID(userDetail, type, out createStatus);
                if (createStatus == MembershipCreateStatus.Success || createStatus == MembershipCreateStatus.DuplicateUserName)
                {
                    string userName = CommonModel.GetUserNameByOpenId(userDetail.id);
                    FormsAuthentication.SetAuthCookie(userName, false /* createPersistentCookie */);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Search");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
                // If we got this far, something failed, redisplay form
                return View(userDetail);
            
        }

        // GET: /Account/LogOn
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            try
            {
                var user = _db.Users.Where(u => u.Username.Equals(model.Username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user != null)
                {
                    string newPassword = StringUtil.RandomStr();
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        user.Password = CommonController.GetMD5Hash(newPassword);
                        _db.ObjectStateManager.ChangeObjectState(user, System.Data.EntityState.Modified);
                        _db.SaveChanges();
                        string emailTemplate = Repository.GetAllEmailTemplate()
                                                .Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_FORGOT_PASSWORD,
                                                    StringComparison.CurrentCultureIgnoreCase))
                                                    .Select(m => m.Template).FirstOrDefault();
                        string subject = Repository.GetAllEmailTemplate()
                                                .Where(e => e.Name.Equals(ConstantEmailTemplate.SUBJECT_RECEIVE_FORGOT_PASSWORD, 
                                                    StringComparison.CurrentCultureIgnoreCase))
                                                    .Select(m => m.Template).FirstOrDefault();
                        string message = string.Format(emailTemplate, model.Username, DateTime.Now.ToString(), newPassword);
                        CommonModel.SendEmail(user.Email, message, subject, 0);
                    }
                    if (!string.IsNullOrEmpty(user.PhoneNumber))
                    {
                        CommonController.SendSMS(user.PhoneNumber, string.Format("Mat khau cua ban la: {0}", newPassword));
                    }
                    TempData["MessageForgotPassword"] = "Mật khẩu đã được gửi về email của bạn";
                    return RedirectToAction("LogOn");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không tồn tại");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return View(model);
            }
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {

            if (ModelState.IsValid)           
            {
                try
                {
                    model.Password = CommonController.GetMD5Hash(model.Password);
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        var user = (from p in _db.Users where p.Username == model.UserName select new { p.RoleId }).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        if (user.RoleId == 1)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Search");
                        }
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
            return View(model);
        }
        
        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Search");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        
        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        public ActionResult Register(RegisterModel model, bool captchaValid)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("captcha", "Sai mã bảo vệ");            
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Attempt to register the user
                        MembershipCreateStatus createStatus;
                        CustomMembershipProvider customMP = new CustomMembershipProvider();
                        customMP.CreateUser(model, out createStatus);
                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                            return RedirectToAction("Index", "Search");
                        }
                        else
                        {
                            ModelState.AddModelError("", ErrorCodeToString(createStatus));
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);                        
                    }
                }
            }
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            string success = "";
            string error = "";
            if (ModelState.IsValid)
            {                
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var oldPassword = CommonController.GetMD5Hash(model.OldPassword);
                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    var account = (from a in _db.Users
                                    where
                                        a.Username.Equals(User.Identity.Name) &&
                                        a.Password.Equals(oldPassword)
                                    select a).FirstOrDefault();
                    if (account == null)
                    {
                        changePasswordSucceeded = false;
                        error = "Mật khẩu cung cấp không phù hợp hoặc mật khẩu mới không đúng.";
                    }
                    else
                    {
                        account.Password = CommonController.GetMD5Hash(model.NewPassword);
                        _db.SaveChanges();
                        changePasswordSucceeded = true;
                        success = "Thay đổi mật khẩu thành công";
                        CommonModel.SendEmail(account.Email, "Mật khẩu của tài khoản " + account.Username + " đã được đổi thành " + model.NewPassword, "Đổi mật khẩu thành công!", 0);
                    }
                    
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                    error = "Mật khẩu cũ bạn nhập không đúng. Vui lòng nhập lại";
                }

                if (changePasswordSucceeded)
                {
                    ViewBag.Message = "Thay đổi mật khẩu thành công";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu cung cấp không phù hợp hoặc mật khẩu mới không đúng");
                }
            }

            // If we got this far, something failed, redisplay form
            @ViewBag.success = success;
            @ViewBag.error = error;
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
