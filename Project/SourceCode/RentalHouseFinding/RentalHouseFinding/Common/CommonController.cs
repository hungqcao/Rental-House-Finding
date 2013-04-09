using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using RentalHouseFinding.Models;
using System.Net;
using System.IO;
using System.Web.Mvc;
using System.Web;

namespace RentalHouseFinding.Common
{
    public static class CommonController
    {        
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

        public static Dictionary<int, string> GetListNearbyLocations(PostViewModel model, HttpRequestBase Request)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
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
                                        location.IsCreatedByUser = true;
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
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<SelectListItem> AddDefaultOption(IEnumerable<SelectListItem> list, string dataTextField, string selectedValue)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = dataTextField, Value = selectedValue });
            items.AddRange(list);
            return items;
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

        public static string GetCenterMap(SearchViewModel model)
        {
            RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
            string centerMap = String.Empty;
            if (model.DistrictId == 0)
            {
                var province = (from p in _db.Provinces where p.Id == model.ProvinceId && !p.IsDeleted select p).FirstOrDefault();
                centerMap = province.Lat + "," + province.Lon + "|11";
            }
            else
            {
                var district = (from p in _db.Districts where p.Id == model.DistrictId && !p.IsDeleted select p).FirstOrDefault();
                centerMap = district.Lat + "," + district.Lon + "|14";
            }
            return centerMap;
        }

        public static bool SendSMS(string phoneNumber, string message)
        {
            try
            {
                //phoneNumber = phoneNumber.Remove(0, 1);            
                //string URLString = "http://api.gateway160.com/client/sendmessage";
                ////string[] param = new string[] { "MyAccount", "key", "16472876789", "US", Url.Encode("Hello world"), "0" };
                //string account = "5house";
                //string apiKey = "3c06b70f-b76d-4def-8ecb-719993faad20";
                //string countryCode = "VN";            
                //string postData = string.Format("accountName={0}&key={1}&phoneNumber={2}&countryCode={3}&message={4}&isUnicode={5}",account,apiKey,phoneNumber,countryCode,message,"0");
                //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLString);
                //request.Method = "post";
                //request.ContentType = "application/x-www-form-urlencoded";
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //if (HttpStatusCode.OK == response.StatusCode)
                //{
                //    Stream receiveStream = response.GetResponseStream();
                //    StreamReader sr = new StreamReader(receiveStream);
                //    string output = sr.ReadLine();
                //    response.Close();

                //    if (output == "1")
                //    {
                //        return true;
                //    }

                //    //error  (check the response code from the chart above)
                //    return false;
                //}
                //else
                //{
                //    return false;
                //}
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static string RandomString(int size)
        {
            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random _rng = new Random();
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public static string CodeRenew()
        {
            RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
            //for create Code Renew.
            while (true)
            {
                string code = CommonController.RandomString(4);
                var postId = (from p in _db.Posts
                              where p.Code.Equals(code) && !p.IsDeleted
                              select p.Id).FirstOrDefault();
                if (postId == 0)
                {
                    return code;
                }
                
            }           
        }
    }
}