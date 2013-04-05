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

        public JsonResult AreaMaxGreaterThanAreaMin(float AreaMax, float? AreaMin)
        {
            if (AreaMin == null)
            {
                AreaMin = 0;
            }
            if (AreaMax <= AreaMin)
            {
                return Json("Diện tích lớn nhất phải lớn hơn diện tích nhỏ nhất", JsonRequestBehavior.AllowGet);
            }            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AreaMinLessThanAreaMax(float AreaMin, float? AreaMax)
        {
            if (AreaMax == null)
            {
                AreaMax = float.MaxValue;
            }
            if (AreaMax <= AreaMin)
            {
                return Json("Diện tích nhỏ nhất phải nhỏ hơn diện tích lớn nhất", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PriceMaxGreaterThanPriceMin(float PriceMax, float? PriceMin)
        {
            if (PriceMin == null)
            {
                PriceMin = 0;
            }
            if (PriceMax <= PriceMin)
            {
                return Json("Giá cao nhất phải lớn hơn giá tiền thấp nhất", JsonRequestBehavior.AllowGet);
            }            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PriceMinLessThanPriceMax(float PriceMin, float? PriceMax)
        {
            if (PriceMax == null)
            {
                PriceMax = float.MaxValue;
            }
            if (PriceMax <= PriceMin)
            {
                return Json("Giá thấp nhất phải nhỏ hơn giá cao nhất", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
