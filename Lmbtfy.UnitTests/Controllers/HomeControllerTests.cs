using Lmbtfy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Lmbtfy.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_WithoutQuestion_ReturnsIndexView()
        {
            // arrange
            var controller = new HomeController();

            // act
            var result = controller.Index(null) as ViewResult;

            // assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Index_WithQuestion_ReturnsBingThisView()
        {
            // arrange
            var question = "question";
            var controller = new HomeController();

            // act
            var result = controller.Index(question) as ViewResult;

            // assert
            Assert.Same(result.ViewData["Question"], question);
            Assert.Equal("BingThis", result.ViewName);
        }
    }
}
