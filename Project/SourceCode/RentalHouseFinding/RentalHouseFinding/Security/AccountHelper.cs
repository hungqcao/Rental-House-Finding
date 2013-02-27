using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RentalHouseFinding.Models;
namespace RentalHouseFinding.Sercurity
{
    public class AccountHelper
    {
        private readonly RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        #region Membership Helper

        public bool ValidateUserHelper(string userName, string password)
        {
            try
            {

                var user = (from u in _db.Users
                            where
                                u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) &&
                                u.Password.Equals(password)
                            select u).ToList();
                if (user.Any()) return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public bool ChangePasswordHelper(string userName, string oldPassword, string newPassword)
        {
            string iOldPassword = GetMD5Hash(oldPassword);
            string iNewPassword = GetMD5Hash(newPassword);

            var account = from a in _db.Users
                          where a.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) &&
                                a.Password.Equals(iOldPassword)
                          select a;

            if (account.Count() != 0)
            {
                account.FirstOrDefault().Password = iNewPassword;
                _db.Attach(account.FirstOrDefault());

                _db.ObjectStateManager.ChangeObjectState(account.FirstOrDefault(), EntityState.Modified);
                return true;
            }
            return false;
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

        #endregion




        #region Role Helper
        //get user role by User name
        public string[] GetUserRoleByAccountName(string userName)
        {
            try
            {
                var accountType = from u in _db.Users
                                  where u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                                  select u.RoleId;
                var accountRole = from a in _db.Roles
                                  where a.Id == accountType.FirstOrDefault()
                                  select a.Name;


                //verify exception here, cannot get database from internet
                if (!accountRole.Any()) return new[] { "" };
                return accountRole.ToArray();
            }
            catch (Exception)
            {
                return new[] { "" };
            }


        }

        public bool CheckUserInRole(string userName, string roleName)
        {

            var accountTypes = from a in _db.Users where a.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) select a.RoleId;

            var roles = from r in _db.Roles select r.Name;
            if (roles.First().ToLower().Equals(roleName.ToLower(), StringComparison.CurrentCultureIgnoreCase)) return true;
            return false;
        }
        #endregion

    }
}