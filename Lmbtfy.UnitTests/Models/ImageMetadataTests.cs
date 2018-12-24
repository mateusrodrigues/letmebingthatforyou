using System;
using System.IO;
using Lmbtfy.UnitTests.Extensions;
using Lmbtfy.Web.Models;
using Moq;
using Xunit;

namespace Lmbtfy.UnitTests.Models
{
    public class ImageMetadataTests
    {
        [Fact]
        public void ImageMetadataPopulatesSelfFromReader()
        {
            // arrange
            var reader = new Mock<TextReader>();
            reader.Setup(r => r.ReadLine()).ReturnsInOrder("http://license-url/", "license type", "http://image-url/", "Author", new InvalidOperationException());

            // act
            var image = new ImageMetadata("/images/foo.jpg", reader.Object);

            // assert
            Assert.Equal("http://license-url/", image.LicenseUrl);
            Assert.Equal("license type", image.LicenseType);
            Assert.Equal("http://image-url/", image.SourceUrl);
            Assert.Equal("Author", image.Author);
        }
    }
}
