using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Lmbtfy.UnitTests.Extensions;
using Lmbtfy.Web.Models;
using Lmbtfy.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace Lmbtfy.UnitTests.Services
{
    public class ImageRepositoryTests
    {
        [Fact]
        public void GetImages_ReturnsAllImages()
        {
            // Arrange
            var hostingEnvironment = new Mock<IHostingEnvironment>();
            hostingEnvironment.SetupGet<string>(h => h.WebRootPath).Returns(string.Empty);

            var reader = new Mock<TextReader>();
            reader.Setup(r => r.ReadLine()).ReturnsInOrder("http://license-url/", "license type", "http://image-url/", "Author", new InvalidOperationException());

            var reader2 = new Mock<TextReader>();
            reader2.Setup(r => r.ReadLine()).ReturnsInOrder("http://license-url2/", "license type2", "http://image-url2/", "Author2", new InvalidOperationException());

            var directoryService = new Mock<IDirectoryService>();
            directoryService.Setup(d => d.EnumerateFiles(@"images\bg", "*.jpg")).Returns(new[] { @"c:\web\bg\img1.jpg", @"c:\web\bg\img2.jpg" });
            directoryService.Setup(d => d.GetReader(@"c:\web\bg\img1.jpg.meta")).Returns(reader.Object);
            directoryService.Setup(d => d.GetReader(@"c:\web\bg\img2.jpg.meta")).Returns(reader2.Object);

            var imageRepository = new ImageRepository(directoryService.Object, hostingEnvironment.Object);


            // Act
            var images = imageRepository.GetImages();

            // Assert
            hostingEnvironment.VerifyAll();
            Assert.Equal(2, images.Count());
            Assert.Equal(@"images\bg\img1.jpg", images.First().ImageUrl);
            Assert.Equal("http://license-url/", images.First().LicenseUrl);
            Assert.Equal(@"images\bg\img2.jpg", images.Last().ImageUrl);
            Assert.Equal("http://license-url2/", images.Last().LicenseUrl);
        }
    }
}
