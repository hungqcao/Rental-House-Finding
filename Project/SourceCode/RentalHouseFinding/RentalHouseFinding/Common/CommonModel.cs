using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using RentalHouseFinding.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Collections.Generic;

namespace RentalHouseFinding.Common
{
    public static class CommonModel
    {
        public static string Trim(string input)
        {
            while (true)
            {
                bool valid = true;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ' ' && input[i + 1] == ' ')
                    {
                        input.Replace(input[i].ToString(), "");
                        valid = false;
                    }
                }
                if (valid)
                {
                    break;
                }
            }
            return input;
        }
        public static string BuildRegexBadWord()
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities()) 
            {
                StringBuilder sb = new StringBuilder();
                string template = "({0})";
                var regex = (from b in _db.BadWords select b).ToList();
                foreach (var word in regex)
                {
                    sb.Append(string.Format(template, word.Word)).Append("|");
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public static Posts ConvertPostViewModelToPost(PostViewModel model, DateTime createdDate, DateTime? editedDate, DateTime? renewDate, DateTime expiredDate, string noInformation)
        {
            
            string facilityTempId = (model.HasAirConditioner ? "1" : "0") +
                                    (model.HasBed ? "1" : "0") +
                                    (model.HasGarage ? "1" : "0") +
                                    (model.HasInternet ? "1" : "0") +
                                    (model.HasMotorParking ? "1" : "0") +
                                    (model.HasSecurity ? "1" : "0") +
                                    (model.HasToilet ? "1" : "0") +
                                    (model.HasTVCable ? "1" : "0") +
                                    (model.HasWaterHeater ? "1" : "0") +
                                    (model.IsAllowCooking ? "1" : "0") + 
                                    (model.IsStayWithOwner ? "1" : "0");
            int facTemId = Convert.ToInt32(facilityTempId, 2) + 1;            
            return new Posts
            {
                NumberAddress = string.IsNullOrEmpty(model.NumberHouse) ? noInformation : model.NumberHouse,
                Street = model.Street,
                Area = model.Area,
                CategoryId = model.CategoryId,
                Contacts = new Contacts()
                {
                    Email = String.IsNullOrEmpty(model.Email)?null:model.Email.Trim(),
                    Phone = String.IsNullOrEmpty(model.PhoneContact) ? null : model.PhoneContact.Trim(),
                    Skype = String.IsNullOrEmpty(model.Skype) ? null : model.Skype.Trim(),
                    Yahoo = String.IsNullOrEmpty(model.Yahoo) ? null : model.Yahoo.Trim(),
                    NameContact = string.IsNullOrEmpty(model.NameContact) ? noInformation : model.NameContact.Trim(),
                },
                CreatedDate = createdDate,
                Description = string.IsNullOrEmpty(model.Description) ? noInformation : model.Description.Trim(),
                DistrictId = model.DistrictId,
                EditedDate = editedDate,
                ExpiredDate = expiredDate,
                Facilities = new Facilities()
                {
                    ElectricityFee = model.ElectricityFee,
                    HasAirConditioner = model.HasAirConditioner,
                    HasBed = model.HasBed,
                    HasGarage = model.HasGarage,
                    HasInternet = model.HasInternet,
                    HasMotorParkingLot = model.HasMotorParking,
                    HasSecurity = model.HasSecurity,
                    HasToilet = model.HasToilet,
                    HasTVCable = model.HasTVCable,
                    HasWaterHeater = model.HasWaterHeater,
                    IsAllowCooking = model.IsAllowCooking,
                    IsStayWithOwner = model.IsStayWithOwner,
                    RestrictHours = model.RestrictHours,
                    WaterFee = model.WaterFee,
                    FacilityTemplateId = facTemId
                    
                },
                Lat = model.Lat,
                Lon = model.Lon,
                PhoneActive = model.PhoneActive,
                Price = model.Price,
                RenewDate = renewDate,
                Title = model.Title.Trim(),
                IsDeleted = false,
                NearbyPlace = noInformation,
                Code = CommonController.CodeRenew()
            };
        }

        public static Posts ConvertPostViewModelToPost(Posts post, PostViewModel model, DateTime createdDate, DateTime? editedDate, DateTime? renewDate, DateTime exprideDate, string noInformation)
        {
            if (string.IsNullOrEmpty(model.NumberHouse))
            {
                model.NumberHouse = noInformation;
            }
            post.NumberAddress = model.NumberHouse.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.NumberHouse;
            post.Street = model.Street;
            post.Area = model.Area;
            post.CategoryId = model.CategoryId;

            post.Contacts.Email = model.Email;
            post.Contacts.Phone = model.PhoneContact;
            post.Contacts.Skype = model.Skype;
            post.Contacts.Yahoo = model.Yahoo;
            if (string.IsNullOrEmpty(model.NameContact))
            {
                model.NameContact = noInformation;
            }
            post.Contacts.NameContact = model.NameContact.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.NameContact;

            post.CreatedDate = createdDate;
            if (string.IsNullOrEmpty(model.Description))
            {
                model.Description = noInformation;
            }
            post.Description = model.Description.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.Description;
            post.DistrictId = model.DistrictId;
            post.EditedDate = editedDate;

            post.Facilities.ElectricityFee = model.ElectricityFee;
            post.Facilities.HasAirConditioner = model.HasAirConditioner;
            post.Facilities.HasBed = model.HasBed;
            post.Facilities.HasGarage = model.HasGarage;
            post.Facilities.HasInternet = model.HasInternet;
            post.Facilities.HasMotorParkingLot = model.HasMotorParking;
            post.Facilities.HasSecurity = model.HasSecurity;
            post.Facilities.HasToilet = model.HasToilet;
            post.Facilities.HasTVCable = model.HasTVCable;
            post.Facilities.HasWaterHeater = model.HasWaterHeater;
            post.Facilities.IsAllowCooking = model.IsAllowCooking;
            post.Facilities.IsStayWithOwner = model.IsStayWithOwner;
            post.Facilities.RestrictHours = model.RestrictHours;
            post.Facilities.WaterFee = model.WaterFee;

            post.Lat = model.Lat;
            post.Lon = model.Lon;
            post.PhoneActive = model.PhoneActive;
            post.Price = model.Price;
            post.RenewDate = renewDate;
            post.ExpiredDate = exprideDate;
            post.Title = model.Title;
            //post.Views = 0;//?
            post.IsDeleted = false;

            return post;
        }        

        public static Dictionary<int, string> GetDictionaryNearybyPlace(int postId)
        {
            Dictionary<int, string> dictReturn = new System.Collections.Generic.Dictionary<int, string>();
            using(RentalHouseFindingEntities db = new RentalHouseFindingEntities())
            {
                var lstNearyBy = db.PostLocations.Where(p => p.PostId == postId).ToList();
                foreach (var item in lstNearyBy)
                {
                    try
                    {
                        dictReturn.Add(item.LocationId, item.Location.Name);
                    }
                    catch
                    {
                    }
                }
            }
            return dictReturn;
        }

        public static PostViewModel ConvertPostToPostViewModel(Posts model, string noInformation)
        {
            RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
            string createBy = model.UserId == null ? string.Empty : model.User.Username;
            var imagesPath = (from i in _db.PostImages where (i.PostId == model.Id && !i.IsDeleted) select i.Path).FirstOrDefault();
            return new PostViewModel
            {
                Id = model.Id,
                NumberHouse = model.NumberAddress.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.NumberAddress,
                Street = model.Street,
                Address = String.Format("{0} {1}, {2}",model.NumberAddress,model.Street,model.District.Name),
                Area = model.Area,
                CategoryId = model.CategoryId,
                Email = model.Contacts.Email,
                PhoneContact = model.Contacts.Phone,
                Skype = model.Contacts.Skype,
                Yahoo = model.Contacts.Yahoo,
                Description = model.Description.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.Description,
                DistrictId = model.DistrictId,
                ElectricityFee = model.Facilities.ElectricityFee,
                HasAirConditioner = model.Facilities.HasAirConditioner,
                HasBed = model.Facilities.HasBed,
                HasGarage = model.Facilities.HasGarage,
                HasInternet = model.Facilities.HasInternet,
                HasMotorParking = model.Facilities.HasMotorParkingLot,
                HasSecurity = model.Facilities.HasSecurity,
                HasToilet = model.Facilities.HasToilet,
                HasTVCable = model.Facilities.HasTVCable,
                HasWaterHeater = model.Facilities.HasWaterHeater,
                IsAllowCooking = model.Facilities.IsAllowCooking,
                IsStayWithOwner = model.Facilities.IsStayWithOwner,
                RestrictHours = model.Facilities.RestrictHours,
                WaterFee = model.Facilities.WaterFee,
                Lat = model.Lat,
                Lon = model.Lon,
                PhoneActive = model.PhoneActive,
                Price = model.Price,
                Title = model.Title,
                CreatedDate = model.CreatedDate,
                EditedDate = model.EditedDate,
                CreatedBy = createBy,
                Category = model.Category.Name,
                Internet = model.Facilities.HasInternet? "Có":"Không",
                AirConditioner = model.Facilities.HasAirConditioner ? "Có" : "Không",
                Bed = model.Facilities.HasBed ? "Có" : "Không",
                Gara = model.Facilities.HasGarage ? "Có" : "Không",
                MotorParkingLot = model.Facilities.HasMotorParkingLot ? "Có" : "Không",
                Security = model.Facilities.HasSecurity ? "Có" : "Không",
                Toilet = model.Facilities.HasToilet ? "Có" : "Không",
                TVCable = model.Facilities.HasTVCable ? "Có" : "Không",
                WaterHeater = model.Facilities.HasWaterHeater ? "Có" : "Không",
                AllowCooking = model.Facilities.IsAllowCooking ? "Có" : "Không",
                StayWithOwner = model.Facilities.IsStayWithOwner ? "Có" : "Không",
                UserId = model.UserId,
                NameContact = model.Contacts.NameContact.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.Contacts.NameContact,
                NearByPlace = model.NearbyPlace.Equals(noInformation, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : model.NearbyPlace,
                lstNearByPlace = GetDictionaryNearybyPlace(model.Id),
                ImagesPath = imagesPath
            };
        }

        public static int GetUserIdByUsername(string userName)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var user = (from u in _db.Users
                            where u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                            select u).FirstOrDefault();
                if (user != null)
                {
                    return user.Id;
                }
                else
                {
                    //Error
                    return -1;
                }
            }
        }

        public static Users GetUserByUsername(string userName)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var user = (from u in _db.Users
                            where u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                            select u).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    //Error
                    return null;
                }
            }
        }

        //get UserId by OpenId
        public static int GetUserIdByOpenId(string openId)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var user = (from u in _db.Users
                            where u.OpenIdURL.Equals(openId, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                            select u).FirstOrDefault();
                if (user != null)
                {
                    return user.Id;
                }
                else
                {
                    //Error
                    return -1;
                }
            }
        }

        //get UserName by openId
        public static string GetUserNameByOpenId(string openId)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var user = (from u in _db.Users
                            where u.OpenIdURL.Equals(openId, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                            select u.Username).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    //Error
                    return String.Empty;
                }
            }
        }
        //check UserName esixt 
        public static bool CheckUserName(string userName)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var userId = (from u in _db.Users
                              where u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                              select u.Id).FirstOrDefault();
                if (userId != 0)
                {
                    //UserName is existed 
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
        }
        //get email 
        public static string GetEmailByUserId(int userId)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var email = (from u in _db.Users
                              where u.Id == userId && !u.IsDeleted
                              select u.Email).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    //UserName is existed 
                    return email;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        //check Email esixt 
        public static bool CheckEmail(string email)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var userId = (from u in _db.Users
                              where u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                              select u.Id).FirstOrDefault();
                if (userId != 0)
                {
                    //UserName is existed 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //check Email exit in Edit user info page.
        public static bool CheckEmail(string email, string userName)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var userId = (from u in _db.Users
                              where u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted
                              select u.Id).FirstOrDefault();
                var currentUserId = (from u in _db.Users
                              where u.Username.Equals(userName) && !u.IsDeleted
                              select u.Id).FirstOrDefault();
                if (userId != 0 && userId != currentUserId)
                {
                    //UserName is existed 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //send Email.
        public static bool SendEmail(string emailAdd,string bodyMessage, string subject, int messageType)
        {
            string email = "findinghousesystem@gmail.com";
            string password = "matkhaulagi";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(emailAdd));
            msg.Subject = subject;
            msg.Body = bodyMessage;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
            return true;
        }

        /// <summary>
        /// Filter whether it contains bad words
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false for it doesnt have bad word</returns>
        public static bool FilterHasBadContent(Object model)
        {
            string regex = CommonModel.BuildRegexBadWord();

            Type type = model.GetType();

            Match match;
            IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(model, null);

                if (propValue != null)
                {
                    match = Regex.Match(propValue.ToString(), regex, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        return true;
                    }
                }
            }	
            return false;
        }

        public static bool IsOpenIdOrFacebookAccount(int userId)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                {
                    var user = _db.Users.Where(u => u.Id == userId).FirstOrDefault();
                    if (user != null)
                    {
                        return !string.IsNullOrEmpty(user.OpenIdURL);
                    }
                    return false;
                }
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public static int GetUserNotification(string name)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                int userId = CommonModel.GetUserIdByUsername(name);
                var lstPostId = _db.Posts.Where(p => p.UserId == userId && !p.IsDeleted).Select(p => p.Id).ToList();

                int numOfUnreadQuestion = _db.Questions.Where(q => lstPostId.Contains(q.PostId) && !q.IsDeleted && !q.IsRead).ToList().Count;

                var lstSentQuestion = _db.Questions.Where(q => q.SenderId == userId && !q.IsDeleted).ToList();

                int numOfUnreadAnswer = 0;

                foreach (var item in lstSentQuestion)
                {
                    foreach (var answer in item.Answers)
                    {
                        if (!answer.IsRead)
                        {
                            numOfUnreadAnswer++;
                        }
                    }
                }
                return numOfUnreadAnswer + numOfUnreadQuestion;
            }
        }

        public static string GetNameOfUser(string name)
        {
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                Users user = CommonModel.GetUserByUsername(name);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        return user.Name;
                    }
                    if (!string.IsNullOrEmpty(user.Username))
                    {
                        return user.Username;
                    }
                }
                return string.Empty;
            }
        }

    }
}
