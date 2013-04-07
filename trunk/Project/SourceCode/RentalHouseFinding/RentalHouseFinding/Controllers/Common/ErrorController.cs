using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers.Common
{
    public class ErrorController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return View("Error", new ErrorModel() { ErrorTitle = "General Error", ExceptionDetail = exception });
        }
 
        public ActionResult Http404()
        {
            return View("Error", new ErrorModel() { ErrorTitle = "Not found" });
        }
       
        public ActionResult Http403()
        {
            return View("Error", new ErrorModel() { ErrorTitle = "Forbidden" });
        }
    }

}
