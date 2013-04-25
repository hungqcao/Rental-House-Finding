using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using RentalHouseFinding.Caching;
using RentalHouseFinding.Models;
namespace RentalHouseFinding.Tests
{
    public class UnitTestBase
    {
        public Mock<RequestContext> RoutingRequestContext { get; private set; }
        public Mock<HttpContextBase> Http { get; private set; }
        public Mock<HttpServerUtilityBase> Server { get; private set; }
        public Mock<HttpResponseBase> Response { get; private set; }
        public Mock<HttpRequestBase> Request { get; private set; }
        public Mock<HttpSessionStateBase> Session { get; private set; }
        public Mock<ActionExecutingContext> ActionExecuting { get; private set; }
        public HttpCookieCollection Cookies { get; private set; }

        public ICacheRepository SetupCacheRepos()
        {
            RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
            var cacheRepos = new Mock<ICacheRepository>();
            cacheRepos.Setup(x => x.GetAllBadWords()).Returns(_db.BadWords.ToList());
            cacheRepos.Setup(x => x.GetAllConfiguration())
                .Returns(_db.ConfigurationRHFs.ToList());
            return cacheRepos.Object;
        }
        public UnitTestBase CreateMockContext()
        {
            this.RoutingRequestContext = new Mock<RequestContext>(MockBehavior.Loose);
            this.ActionExecuting = new Mock<ActionExecutingContext>(MockBehavior.Loose);
            this.Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            this.Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            this.Response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            this.Request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            this.Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            this.Cookies = new HttpCookieCollection();

            this.RoutingRequestContext.SetupGet(c => c.HttpContext).Returns(this.Http.Object);
            this.ActionExecuting.SetupGet(c => c.HttpContext).Returns(this.Http.Object);
            this.Http.SetupGet(c => c.Request).Returns(this.Request.Object);
            this.Http.SetupGet(c => c.Response).Returns(this.Response.Object);
            this.Http.SetupGet(c => c.Server).Returns(this.Server.Object);
            this.Http.SetupGet(c => c.Session).Returns(this.Session.Object);
            this.Request.Setup(c => c.Cookies).Returns(Cookies);
            return this;
        }

        public UnitTestBase CreateMockContext(string username)
        {
            this.RoutingRequestContext = new Mock<RequestContext>(MockBehavior.Loose);
            this.ActionExecuting = new Mock<ActionExecutingContext>(MockBehavior.Loose);
            this.Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            this.Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            this.Response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            this.Request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            this.Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            this.Cookies = new HttpCookieCollection();

            this.RoutingRequestContext.SetupGet(c => c.HttpContext).Returns(this.Http.Object);
            this.ActionExecuting.SetupGet(c => c.HttpContext).Returns(this.Http.Object);
            this.Http.SetupGet(c => c.Request).Returns(this.Request.Object);
            this.Http.SetupGet(c => c.Response).Returns(this.Response.Object);
            this.Http.SetupGet(c => c.Server).Returns(this.Server.Object);
            this.Http.SetupGet(c => c.Session).Returns(this.Session.Object);
            this.Http.SetupGet(p => p.User.Identity.Name).Returns(username);

            this.Http.SetupGet(p => p.Request.IsAuthenticated).Returns(true);
            this.Request.Setup(c => c.Cookies).Returns(Cookies);
            return this;
        }
    }
}
