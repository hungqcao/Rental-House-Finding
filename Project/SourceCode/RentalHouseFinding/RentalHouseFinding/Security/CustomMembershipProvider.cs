using System;
using System.Web.Security;
using System.Linq;
using RentalHouseFinding.Models;
using System.Data;
using RentalHouseFinding.RHF.Common;

namespace RentalHouseFinding.Sercurity
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            string oldPasswordHashed = AccountHelper.GetMD5Hash(oldPassword);
            string newPasswordHashed = AccountHelper.GetMD5Hash(newPassword);
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                var account = from a in _db.Users
                              where a.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) &&
                                    a.Password.Equals(oldPasswordHashed)
                              select a;

                if (account.Count() != 0)
                {
                    account.FirstOrDefault().Password = newPasswordHashed;
                    _db.Attach(account.FirstOrDefault());

                    _db.ObjectStateManager.ChangeObjectState(account.FirstOrDefault(), EntityState.Modified);
                    return true;
                }
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(RegisterModel model, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(model.UserName, model.Password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            //if ((RequiresUniqueEmail && !(string.IsNullOrEmpty(GetUserNameByEmail(model.Email)))))
            //{
            //    status = MembershipCreateStatus.DuplicateEmail;
            //    return null;
            //}
            int userID = CommonModel.GetUserIdByUsername(model.UserName);

            if (userID != -1)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            MembershipUser membershipUser = GetUser(model.UserName, false);

            if (membershipUser == null)
            {
                try
                {
                    using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                    {
                        Users user = new Users();
                        user.IsDeleted = false;
                        user.Name = model.Name;
                        user.Username = model.UserName;
                        user.Password = AccountHelper.GetMD5Hash(model.Password);
                        user.Email = model.Email.ToLower();
                        user.CreatedDate = DateTime.Now;
                        user.LastUpdate = DateTime.Now;
                        user.Address = model.Address;
                        user.DOB = model.DateOfBirth;
                        user.Avatar = model.Avatar;
                        user.PhoneNumber = model.PhoneNumber;
                        user.RoleId = 3;
                        user.Sex = model.Sex;
                        //user.KeyActive = Guid.NewGuid(); // curently, not use.

                        _db.AddToUsers(user);

                        _db.SaveChanges();

                        status = MembershipCreateStatus.Success;
                        //send mail welcome!
                        CommonModel.SendEmail(model.UserName, String.Format("Chào mừng bạn đến với HouseFinding!</br>Thông tin tài khoản:</br>-Email:{0}</br>-Mật khẩu:{1}</br> ",model.UserName,model.Password), 0);

                        return GetUser(model.UserName, false);
                    }

                }
                catch(Exception ex)
                {
                    status = MembershipCreateStatus.ProviderError;
                }
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public MembershipUser CreateUserForOpenID(UserDetailsModel model, out MembershipCreateStatus status)
        {
            //Check username da tontai chua, sau do check openid xem co ton tai chua
            int userID = CommonModel.GetUserIdByUsername(model.email);

            if (userID != -1)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return GetUser(model.email, false);
            }
            else
                //create user
            {
                try
                {
                    using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                    {
                        //add user info
                        Users user = new Users();
                        user.OpenIdURL = model.id;
                        user.Name = model.first_name + " " + model.last_name;                        
                        user.IsDeleted = false;
                        user.Username = model.email;
                        if(!String.IsNullOrEmpty(model.user_birthday))
                        {
                            user.DOB =  DateTime.Parse(model.user_birthday);
                        }                        
                        user.CreatedDate = DateTime.Now;
                        user.LastUpdate = DateTime.Now;
                        user.RoleId = 3;                        
                        user.Sex = model.gender;

                        _db.AddToUsers(user);

                        _db.SaveChanges();

                        status = MembershipCreateStatus.Success;

                        return GetUser(model.email, false);
                    }

                }
                catch (Exception ex)
                {
                    status = MembershipCreateStatus.ProviderError;
                }
            }

            return null;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser membershipUser = null;
            using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
            {
                try
                {
                    var user = (from u in _db.Users 
                                where u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) && !u.IsDeleted && u.IsActive == true
                                 select u)
                                 .FirstOrDefault();
 
                    if (user != null)
                    {
                        membershipUser = new MembershipUser(this.Name,
                            user.Username,
                            null,
                            user.Email,
                            "",
                            "",
                            true,
                            false,
                            user.CreatedDate,
                            DateTime.Now,
                            DateTime.Now,
                            default(DateTime),
                            default(DateTime));
                    }
                }
                catch
                {
                }
            }
 
            return membershipUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }        

        public override string GetUserNameByEmail(string email)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                {
                    var user = (from u in _db.Users
                                where
                                    u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) &&
                                    !u.IsDeleted
                                select u).FirstOrDefault();
                    return user.Username;
                }

            }
            catch
            {
                return null;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                {
                    var user = (from u in _db.Users
                                where
                                    u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) &&
                                    u.Password.Equals(password)
                                select u).ToList();
                    if (user.Any()) return true;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}