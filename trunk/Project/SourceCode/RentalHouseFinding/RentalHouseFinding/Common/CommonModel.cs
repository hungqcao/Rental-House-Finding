using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Common
{
    public static class CommonModel
    {
        private readonly static RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public static string GetMD5Hash(string value)
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
        public static Guid StringToGUID(string value)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }
        public static string ConvertUnicodeToAscii(string words)
        {
            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = words.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static DateTime ConvertToDateTime(string dateTime)
        {
            try
            {
                IFormatProvider theCultureInfo = new CultureInfo("en-GB", true);

                DateTime theDateTime = DateTime.Parse(dateTime, theCultureInfo, DateTimeStyles.AssumeLocal);

                //var temp = dateTime.Split(' ');
                //var dtfi = new DateTimeFormatInfo {ShortDatePattern = "dd-MM-yyyy HH:mm:ss", DateSeparator = "/"};
                //DateTime objDate = Convert.ToDateTime(temp[0], dtfi);))
                return theDateTime;
            }
            catch (Exception)
            {
                return DateTime.Today;
            }
        }
        public static void ClearSession()
        {
            //HttpContext.Current.Session["Students"] = null;
            
        }

        #region Account Helper
        public static string CreateAccountName(string firstName, string lastName, string rollNumber)
        {
            string accountName = "";
            accountName += ConvertUnicodeToAscii(firstName);

            var lastNames = ConvertUnicodeToAscii(lastName).Split(' ');

            accountName = lastNames.Where(name => name.Trim() != "").Aggregate(accountName, (current, name) => current + name.Substring(0, 1));
            accountName += rollNumber;
            return accountName;
        }
        public static Users CreateDefaultAccount(string accountName)
        {
            return new Users
            {
                Username = accountName,
                RoleId = 3,
                Password = GetMD5Hash("12345678"),
                IsDeleted = false
            };
        }

        public static Posts ConvertPostViewModelToPost(PostViewModel model, DateTime createdDate, DateTime editedDate, DateTime renewDate)
        {
            string newAddress = string.Empty;
            if (!string.IsNullOrEmpty(model.NumberHouse))
            {
                newAddress += model.NumberHouse.Trim();
                newAddress += " ";
            }

            if (!string.IsNullOrEmpty(model.Street))
            {
                newAddress += model.Street.Trim();
            }

            if (!string.IsNullOrEmpty(newAddress))
            {
                newAddress = newAddress.Trim();
            }

            return new Posts
            {
                Address = newAddress,
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
                    WaterFee = model.WaterFee
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

        public static PostViewModel ConvertPostToPostViewModel(Posts model)
        {
            return new PostViewModel
            {
                NumberHouse = model.Address,
                Street = model.Address,
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
                Title = model.Title,
                Views = model.Views,                
                CreatedDate = model.CreatedDate,
                EditedDate = model.EditedDate,
                //CreatedBy = model.User.Username,
                Categori = model.Category.Name
            };
        }

        public static int GetUserIdByUsername(string userName)
        {
            var user = (from u in _db.Users
                        where u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted select u).FirstOrDefault();
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

        /// <summary>
        /// Filter whether it contains bad words
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false for it doesnt have bad word</returns>
        public static bool FilterHasBadContent(PostViewModel model)
        {
            //Filter bad content
            return false;
        }

        #endregion

    }
}
