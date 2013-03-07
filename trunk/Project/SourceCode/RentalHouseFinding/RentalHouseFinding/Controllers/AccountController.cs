﻿using System;
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


namespace RentalHouseFinding.Controllers
{
    public class AccountController : Controller
    {
        //
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
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.BirthDate.WholeBirthDate, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Person.Gender, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.First, true));
                        fetch.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.Last, true));
                        request.AddExtension(fetch);
                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewBag.Message = ex.Message;
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
                                case WellKnownAttributes.BirthDate.WholeBirthDate:
                                    var doBs = fetch.Attributes[WellKnownAttributes.BirthDate.WholeBirthDate].Values;
                                    sDob = doBs.Count > 0 ? doBs[0] : null;
                                    break;
                            }
                        }
                    }
                    var sFriendlyLogin = response.FriendlyIdentifierForDisplay;
                    var lm = new UserDetailsModel
                    {
                        OpenID = response.ClaimedIdentifier,

                        FirstName = sFirstName,
                        LastName = sLastName,

                        Gender = sGender,
                        Email = sEmail
                    };
                    return View("ShowUserDetails", lm);

                case AuthenticationStatus.Canceled:
                    ViewBag.Message = "Canceled at provider";
                    return View("LogOn");
                case AuthenticationStatus.Failed:
                    ViewBag.Message = response.Exception.Message;
                    return View("LogOn");
            }

            return new EmptyResult();
        }
        //
        // GET: /Account/LogOn
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        public ActionResult LogOn()
        {

            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl, bool? status)
        {
            if (model.Password != null)
            {
                if (status == false || status == null)
                {
                    model.Password = GetMD5Hash(model.Password);
                }
            }

            if (ModelState.IsValid)
           
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    int accountType = (from p in _db.Users where p.Username == model.UserName select p.RoleId).FirstOrDefault();
                    if (accountType == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View(model);
        }
        
        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                CustomMembershipProvider customMP = new CustomMembershipProvider();
                customMP.CreateUser(model, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
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
                    if (account == null) changePasswordSucceeded = false;
                    else
                    {
                        account.Password = CommonController.GetMD5Hash(model.NewPassword);
                        _db.SaveChanges();
                        changePasswordSucceeded = true;
                        success = "Thay đổi mật khẩu thành công";
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                    error = "Thay đổi mật khẩu không thành công";
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", @"Mật khẩu cung cấp không phù hợp hoặc mật khẩu mới không đúng");
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
        public string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
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
