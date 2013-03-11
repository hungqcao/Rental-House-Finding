using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.RHF.Common;

namespace RentalHouseFinding.Controllers
{
    public class ValidationController : Controller
    {
        //
        // GET: /Validation/

        public JsonResult IsUserNameAvailable(string userName)
        {
            //Go to the DB and retrievw the get User
            List<string> userNames = CommonModel.GetAllUserName();
            //Check if the list contains the userName
            if (userNames.Contains(userName.ToLower()))
            {
                return Json("Email đã được sử dụng", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
