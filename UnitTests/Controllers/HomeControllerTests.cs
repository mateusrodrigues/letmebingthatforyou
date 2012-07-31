using System.Web.Mvc;
using Lmbtfy.Web.Controllers;
using Lmbtfy.Web.Models;
using Xunit;

namespace UnitTests {
    public class HomeControllerTests {
        [Fact]
        public void Index_WithoutQuestion_ReturnsIndexView() {
            // arrange
            var image = new ImageMetadata("/images/foo.jpg");
            var controller = new HomeController(image);

            // act
            var result = controller.Index(null) as ViewResult;

            // assert
            Assert.Same(image, result.Model);
            Assert.Equal("", result.ViewName);
        }

        [Fact]
        public void Index_WithQuestion_ReturnsBingThisView() {
            // arrange
            var image = new ImageMetadata("/images/foo.jpg");
            var controller = new HomeController(image);

            // act
            var result = controller.Index("test") as ViewResult;

            // assert
            Assert.Same(image, result.Model);
            Assert.Equal("BingThis", result.ViewName);
        }
    }
}
