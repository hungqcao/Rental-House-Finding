using System;
using System.Web.Security;
using System.Linq;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Sercurity
{
    public class CustomRoleProvider : RoleProvider 
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

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

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                {
                    var accountType = from u in _db.Users
                                      where u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                                      select u.RoleId;
                    var accountRole = from a in _db.Roles
                                      where a.Id == accountType.FirstOrDefault()
                                      select a.Name;


                    //verify exception here, cannot get database from internet
                    if (!accountRole.Any()) return new[] { "" };
                    return accountRole.ToArray();
                }
            }
            catch (Exception)
            {
                return new[] { "" };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                using (RentalHouseFindingEntities _db = new RentalHouseFindingEntities())
                {
                    var accountTypes = from a in _db.Users where a.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) select a.RoleId;

                    var roles = from r in _db.Roles select r.Name;
                    if (roles.First().ToLower().Equals(roleName.ToLower(), StringComparison.CurrentCultureIgnoreCase)) return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}