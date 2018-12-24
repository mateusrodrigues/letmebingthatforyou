using Lmbtfy.Web.Controllers;
using Lmbtfy.Web.Extensions;
using Lmbtfy.Web.Models;
using Lmbtfy.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lmbtfy.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_WithoutQuestion_ReturnsIndexView()
        {
            // mock
            var images = new List<ImageMetadata>
            {
                new ImageMetadata("/images/foo.jpg")
            };
            var imageRepository = new Mock<IImageRepository>();
            imageRepository.Setup(m => m.GetImages()).Returns(images);

            // arrange
            var controller = new HomeController(imageRepository.Object);

            // act
            var result = controller.Index(null) as ViewResult;

            // assert
            Assert.Same(images.First(), result.Model);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Index_WithQuestion_ReturnsBingThisView()
        {
            // mock
            var images = new List<ImageMetadata>
            {
                new ImageMetadata("/images/foo.jpg")
            };
            var imageRepository = new Mock<IImageRepository>();
            imageRepository.Setup(m => m.GetImages()).Returns(images);

            // arrange
            var controller = new HomeController(imageRepository.Object);

            // act
            var result = controller.Index("test") as ViewResult;

            // assert
            Assert.Same(images.First(), result.Model);
            Assert.Equal("BingThis", result.ViewName);
        }
    }
}
