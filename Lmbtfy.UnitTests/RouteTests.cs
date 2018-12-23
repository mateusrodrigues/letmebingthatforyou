using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lmbtfy.Web;
using Moq;
using Xunit;

namespace UnitTests {
    public class RouteTests {
        [Fact]
        public void RouteUrl_ByName_ReturnsSearchUrl() {
            // arrange
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request.ApplicationPath).Returns("/");
            httpContext.Setup(c => c.Response.ApplyAppPathModifier(It.IsAny<String>())).Returns<string>(s => s);
            httpContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/foo");
            var requestContext = new RequestContext(httpContext.Object, new RouteData());
            var urlHelper = new UrlHelper(requestContext, routes);

            // act
            var url = urlHelper.RouteUrl("Search", new { q = "searchterm" });

            // assert
            Assert.Equal("/?q=searchterm", url);
        }
    }
}
