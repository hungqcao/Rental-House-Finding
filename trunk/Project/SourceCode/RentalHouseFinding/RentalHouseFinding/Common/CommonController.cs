using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using RentalHouseFinding.Models;

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
    }
}