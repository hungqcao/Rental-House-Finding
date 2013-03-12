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

namespace RentalHouseFinding.RHF.Common
{
    public static class CommonModel
    {
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

        public static Posts ConvertPostViewModelToPost(PostViewModel model, DateTime createdDate, DateTime editedDate, DateTime renewDate)
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
                NumberAddress = model.NumberHouse,
                Street = model.Street,
                Area = model.Area,
                CategoryId = model.CategoryId,
                Contacts = new Contacts()
                {
                    Email = model.Email,
                    Phone = model.PhoneContact,
                    Skype = model.Skype,
                    Yahoo = model.Yahoo
                },
                CreatedDate = createdDate,
                Description = model.Description,
                DistrictId = model.DistrictId,
                EditedDate = editedDate,
                Facilities = new Facilities()
                {
                    Direction = model.Direction,
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
                Title = model.Title,
                Views = 0,
                IsDeleted = false
            };
        }

        public static Posts ConvertPostViewModelToPost(Posts post, PostViewModel model, DateTime createdDate, DateTime editedDate, DateTime renewDate)
        {
            post.NumberAddress = model.NumberHouse;
            post.Street = model.Street;
            post.Area = model.Area;
            post.CategoryId = model.CategoryId;

            post.Contacts.Email = model.Email;
            post.Contacts.Phone = model.PhoneContact;
            post.Contacts.Skype = model.Skype;
            post.Contacts.Yahoo = model.Yahoo;

            post.CreatedDate = createdDate;
            post.Description = model.Description;
            post.DistrictId = model.DistrictId;
            post.EditedDate = editedDate;

            post.Facilities.Direction = model.Direction;
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
            post.Title = model.Title;
            post.Views = 0;
            post.IsDeleted = false;

            return post;
        }

        public static PostViewModel ConvertPostToPostViewModel(Posts model)
        {
            return new PostViewModel
            {
                Id = model.Id,
                NumberHouse = model.NumberAddress,
                Street = model.Street,
                Area = model.Area,
                CategoryId = model.CategoryId,
                Email = model.Contacts.Email,
                PhoneContact = model.Contacts.Phone,
                Skype = model.Contacts.Skype,
                Yahoo = model.Contacts.Yahoo,
                Description = model.Description,
                DistrictId = model.DistrictId,
                Direction = model.Facilities.Direction,
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
                Title = model.Title
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
        //get all userName
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

        //send Email.
        public static bool SendEmail(string emailAdd,string bodyMessage, int messageType)
        {
            string email = "findinghousesystem@gmail.com";
            string password = "matkhaulagi";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(emailAdd));
            msg.Subject = "subject test";
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

                match = Regex.Match(propValue.ToString(), regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    return true;
                }
            }	
            return false;
        }

    }
}
