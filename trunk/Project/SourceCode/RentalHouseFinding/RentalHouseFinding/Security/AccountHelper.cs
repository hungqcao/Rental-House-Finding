using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RentalHouseFinding.Models;
namespace RentalHouseFinding.Sercurity
{
    public static class AccountHelper
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
    }
}