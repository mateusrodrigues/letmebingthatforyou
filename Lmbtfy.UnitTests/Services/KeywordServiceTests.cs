using Lmbtfy.Web.Services;
using Xunit;

namespace Lmbtfy.UnitTests.Services
{
    public class KeywordServiceTests
    {
        [Fact]
        public void GetDailyKeyword_ShouldReturnKeyword()
        {
            // Arrange
            var keywordService = new KeywordService();

            // Act
            var result = keywordService.GetDailyKeyword();

            // Assert
            Assert.NotNull(result);
        }
    }
}
