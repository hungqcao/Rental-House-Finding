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
        public static string ConvertUnicodeToAscii(string words)
        {
            if (words == null) words = "";
            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = words.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static DateTime ConvertStringToDateTime(string dateTime)
        {
            IFormatProvider theCultureInfo = new CultureInfo("en-GB", true);
            DateTime theDateTime = DateTime.Parse(dateTime, theCultureInfo, DateTimeStyles.AssumeLocal);
            return theDateTime;
        }

        public static int GetIndexOfColumn(string columnName, IEnumerable<String> listColumn)
        {
            for (int i = 0; i < listColumn.Count(); i++)
            {
                if (listColumn.ElementAt(i).Equals(columnName)) return i;
            }
            return -1;
        }

        #region Account Helper
        public static string CreateAccountName(string firstName, string lastName, string rollNumber)
        {
            string accountName = "";
            accountName += ConvertUnicodeToAscii(firstName);

            var lastNames = ConvertUnicodeToAscii(lastName).Split(' ');

            accountName = lastNames.Where(name => name.Trim() != "").Aggregate(accountName, (current, name) => current + name.Substring(0, 1));
            accountName += rollNumber;
            return accountName.ToLower();
        }
        public static Users CreateDefaultAccount(string accountName)
        {
            return new Users
                       {
                           Username = accountName,
                           RoleId = 2,
                           Password = GetMD5Hash("12345678"),
                           IsDeleted = false
                       };
        }

        #endregion
    }
}