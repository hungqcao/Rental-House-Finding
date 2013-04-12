using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RentalHouseFinding.Common;
using log4net;
using System.Reflection;
using RentalHouseFinding.Controllers.Common;

namespace RentalHouseFinding
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Tai khoan
            routes.MapRoute(
                "AccountFBUserDetail",
                "tai-khoan/FacebookUserDetail/",
                new { controller = "Account", action = "FacebookUserDetail" }
            );
            routes.MapRoute(
                "AccountLogOn",
                "tai-khoan/dang-nhap",
                new { controller = "Account", action = "LogOn" }
            );
            routes.MapRoute(
                "AccountRegister",
                "tai-khoan/dang-ky",
                new { controller = "Account", action = "Register" }
            );
            routes.MapRoute(
                "AccountForgot",
                "tai-khoan/tim-lai-mat-khau",
                new { controller = "Account", action = "Forgotpassword" }
            );
            //End

            //Trang ca nhan
            routes.MapRoute(
                "UserControlPanel",
                "trang-ca-nhan",
                new { controller = "User", action = "Index"}
            );
            routes.MapRoute(
                "UserControlPanelPos",
                "trang-ca-nhan/quan-ly-bai-dang",
                new { controller = "User", action = "Posts" }
            );
            routes.MapRoute(
                "UserControlPanelPayment",
                "trang-ca-nhan/quan-ly-giao-dich",
                new { controller = "User", action = "Payments" }
            );
            routes.MapRoute(
                "UserControlPanelFavorite",
                "trang-ca-nhan/danh-sach-quan-tam",
                new { controller = "User", action = "Favorites" }
            );
            routes.MapRoute(
                "UserControlPanelEdit",
                "trang-ca-nhan/chinh-sua-thong-tin",
                new { controller = "User", action = "Edit" }
            );
            routes.MapRoute(
                "UserControlPanelQuestion",
                "trang-ca-nhan/tin-nhan",
                new { controller = "Questions", action = "Index" }
            );
            //End trang ca nhan

            //Post
            routes.MapRoute(
                "PostDetailbox",
                "thong-tin-bai-dang/{id}/{name}",
                new { controller = "Post", action = "DetailsBox", name = UrlParameter.Optional },
                new { id = @"^\d+$" }
            );

            routes.MapRoute(
                "PostCreate",
                "tao-bai-dang",
                new { controller = "Post", action = "Create"}
            );

            routes.MapRoute(
                "PostDetails",
                "bai-dang/{id}/{name}",
                new { controller = "Post", action = "Details", name = UrlParameter.Optional },
                new { id = @"^\d+$" }
            );

            routes.MapRoute(
                "PostEdit",
                "chinh-sua-bai-dang/{id}/{name}",
                new { controller = "Post", action = "Edit", name = UrlParameter.Optional },
                new { id = @"^\d+$" }
            );

            routes.MapRoute(
                "PostEditForGuest",
                "sua",
                new { controller = "PostEdit", action = "Index"}
            );

            routes.MapRoute(
                "PostEditForGuestEditPage",
                "sua/{id}/{name}",
                new { controller = "PostEdit", action = "Edit", name = UrlParameter.Optional },
                new { id = @"^\d+$" }
            );

            //End post

            routes.MapRoute(
                "AdvSearchResult",
                "tim-kiem-nang-cao",
                new { controller = "AdvancedSearch", action = "Index" }
            );

            routes.MapRoute(
                "SearchResult",
                "ket-qua-tim-kiem",
                new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Search", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
        }

        // <summary>
        // Handle application error on a global level.
        // Passes handling off to the ErrorController
        // </summary>
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;

            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();

                switch (Response.StatusCode)
                {
                    case 403:
                        routeData.Values["action"] = "Http403";
                        break;

                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                }
            }

            IController errorsController = new ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(rc);
        }

    }
}