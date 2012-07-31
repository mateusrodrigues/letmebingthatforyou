using System;
using System.Collections.Generic;
using Lmbtfy.Web.Extensions;
using Xunit;

namespace UnitTests.Extensions {
    public class EnumerableExtensionsTests {
        [Fact]
        public void GetItemOfTheDay_WithNullEnumerable_ThrowsArgumentNullException() {
            // arrange
            var items = (IEnumerable<object>)null;

            // act, assert
            var exception = Assert.Throws<ArgumentNullException>(() => items.GetElementOfTheDay(DateTime.Now));
            Assert.Equal("items", exception.ParamName);
        }

        [Fact]
        public void GetItemOfTheDay_WithOneItem_ReturnsSameItem() {
            // arrange
            var items = new[] { 42 };

            // act

            var item = items.GetElementOfTheDay(DateTime.Now);

            // assert
            Assert.Equal(42, item);
            Assert.Equal(42, items.GetElementOfTheDay(DateTime.Now.AddDays(1)));
        }

        [Fact]
        public void GetItemOfTheDay_WithMoreThanOneItem_ReturnsSameItemForSameDay() {
            // arrange
            var items = new[] { 42, 8, 32 };
            var date = DateTime.MinValue;

            // act
            var item = items.GetElementOfTheDay(date);
            var item2 = items.GetElementOfTheDay(date.AddHours(12));
            var item3 = items.GetElementOfTheDay(date.AddHours(23));

            // assert
            Assert.Equal(42, item);
            Assert.Equal(42, item2);
            Assert.Equal(42, item3);
        }

        [Fact]
        public void GetItemOfTheDay_WithMoreThanOneItem_ReturnsDifferentItemForDifferentDay() {
            // arrange
            var items = new[] { 42, 8, 32 };
            var date = DateTime.MinValue;

            // act
            var item = items.GetElementOfTheDay(date);
            var item2 = items.GetElementOfTheDay(date.AddDays(1));
            var item3 = items.GetElementOfTheDay(date.AddDays(2));

            // assert
            Assert.Equal(42, item);
            Assert.Equal(8, item2);
            Assert.Equal(32, item3);
        }

        [Fact]
        public void GetItemOfTheDay_CalledWithMoreDaysThanItems_GoesBackToFirstItem() {
            // arrange
            var items = new[] { 42, 8, 32 };
            var date = DateTime.MinValue;

            // act
            var item = items.GetElementOfTheDay(date.AddDays(3));

            // assert
            Assert.Equal(42, item);
        }
    }
}
