using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Controllers
{
    public class ValidationController : Controller
    {
        //
        // GET: /Validation/

        public JsonResult IsUserNameAvailable(string userName)
        {
            if (CommonModel.CheckUserName(userName))
            {
                return Json("Tên tài khoản này đã được sử dụng", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmailAvailable(string email)
        {
            if (CommonModel.CheckEmail(email))
            {
                return Json("Địa chỉ Email này đã được sử dụng", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
