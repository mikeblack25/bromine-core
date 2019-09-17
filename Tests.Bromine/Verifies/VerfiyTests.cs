using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

using Bromine.Logger;
using Bromine.Verifies;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.Verifies
{
    /// <summary>
    /// Tests to verify the Verify methods.
    /// </summary>
    public class VerifyTests
    {
        /// <summary>
        /// 
        /// </summary>
        public VerifyTests(ITestOutputHelper output)
        {
            Verify = new Verify(new Log(output));
        }

        /// <summary>
        /// Verify property.
        /// </summary>
        public Verify Verify { get; }

        /// <summary>
        /// Verifies that all items in the collection pass when executed against.
        /// </summary>
        [Fact]
        public void AllTest()
        {

        }

        /// <summary>
        /// Verifies the proper exception is thrown when all items in the collection fail when executed against action.
        /// </summary>
        [Fact]
        public void AllFailedTest()
        {

        }

        /// <summary>
        /// Verifies that a collection contains exactly a given number of elements, which meet
        /// the criteria provided by the element inspectors.
        /// </summary>
        [Fact]
        public void CollectionTest()
        {

        }

        /// <summary>
        /// Verifies the proper exception is thrown when a collection contains exactly a given number of elements, which does NOT meet
        /// the criteria provided by the element inspectors.
        /// </summary>
        [Fact]
        public void CollectionFailedTest()
        {
            var johnDoe = "John Doe";
            var janeDoe = "Jane Doe";

            var list = new List<object>
            {
                johnDoe,
                janeDoe
            };

            Verify.Collection(list, item => Assert.Equal(johnDoe, item),
                                                item => Assert.Equal(janeDoe, item));
        }

        /// <summary>
        /// Verifies that a sub-string is contained within a given string.
        /// </summary>
        [Fact]
        public void ContainsTest()
        {
            var expectedSubString = "ipsum";
            var actualString = "Lorem ipsum dolor sit amet";

            Verify.Contains(expectedSubString, actualString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a sub-string is expected to be contained within a given string and fails.
        /// </summary>
        [Fact]
        public void ContainsFailedTest()
        {
            var expectedSubString = "test";
            var actualString = "Lorem ipsum dolor sit amet";

            Assert.Throws<ContainsException>(() => Assert.Contains(expectedSubString, actualString));
        }

        /// <summary>
        /// Verifies that a sub-string is NOT contained within a given string.
        /// </summary>
        [Fact]
        public void DoesNotContainTest()
        {
            var expectedSubString = "test";
            var actualString = "Lorem ipsum dolor sit amet";

            Verify.DoesNotContain(expectedSubString, actualString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a sub-string is NOT expected to be contained within a given string and fails.
        /// </summary>
        [Fact]
        public void DoesNotContainFailedTest()
        {
            var expectedSubString = "ipsum";
            var actualString = "Lorem ipsum dolor sit amet";

            Assert.Throws<DoesNotContainException>(() => Assert.DoesNotContain(expectedSubString, actualString));
        }

        /// <summary>
        /// Verifies that a string does NOT match a given RegEx pattern.
        /// </summary>
        [Fact]
        public void DoesNotMatchTest()
        {
            var expectedRegexPattern = new Regex("(?:[a-z][a-z]+)").ToString();
            var actualString = "1234";

            Verify.DoesNotMatch(expectedRegexPattern, actualString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a string is not expected to match a given RegEx pattern and fails.
        /// </summary>
        [Fact]
        public void DoesNotMatchFailedTest()
        {
            var expectedRegexPattern = new Regex("(?:[a-z][a-z]+)").ToString();
            var actualString = "test";

            Assert.Throws<DoesNotMatchException>(() =>Assert.DoesNotMatch(expectedRegexPattern, actualString));
        }

        /// <summary>
        /// Verifies that a collection is empty.
        /// </summary>
        [Fact]
        public void EmptyTest()
        {
            List<string> emptyList = new List<string> { };
            
            Verify.Empty(emptyList);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a collection is expected to be empty and fails.
        /// </summary>
        [Fact]
        public void EmptyFailedTest()
        {
            List<string> emptyList = new List<string>
            {
                "test1",
                "test2",
                "test3"
            };

            Assert.Throws<EmptyException>(() => Assert.Empty(emptyList));
        }

        /// <summary>
        /// Verifies that a string ends with a given string.
        ///</summary>
        [Fact]
        public void EndsWithTest()
        {
            var expectedEndString = "test.";
            var actualString = "This is a test.";

            Verify.EndsWith(expectedEndString, actualString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a string is expected to end with the expected string and fails.
        /// </summary>
        [Fact]
        public void EndsWithFailedTest()
        {
            var expectedEndString = "This";
            var actualString = "This is a test.";

            Assert.Throws<EndsWithException>(() => Assert.EndsWith(expectedEndString, actualString));
        }

        /// <summary>
        /// Verifies that two objects are equal.
        /// </summary>
        [Fact]
        public void EqualObjectsTest()
        {
            var expected = new Point();
            var actual = new Point();

            Verify.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when two objects are expected to be equal and fails.
        /// </summary>
        [Fact]
        public void EqualObjectsFailedTest()
        {

        }

        /// <summary>
        /// Verifies that two double values are equal.
        /// </summary>
        [Fact]
        public void EqualDoubleTest()
        {
            var expected = 12.3;
            var actual = 12.3;

            Verify.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when two double values are expected to be equal and fails.
        /// </summary>
        [Fact]
        public void EqualDoubleFailedTest()
        {
            var expected = 12.3;
            var actual = 12.4;

            Assert.Throws<EqualException>(() => Assert.Equal(expected, actual));
        }

        /// <summary>
        /// Verifies that the condition is false.
        /// </summary>
        [Fact]
        public void FalseTest()
        {
            Verify.False(1 == 2);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a condition is expected to be false and fails.
        /// </summary>
        [Fact]
        public void FalseFailedTest()
        {
            Assert.Throws<FalseException>(() => Assert.False(1 == 1));
        }

        /// <summary>
        /// Verifies that a value is within a given range.
        /// </summary>
        [Fact]
        public void InRangeDoubleTest()
        {
            var low = 10.0;
            var high = 12.0;
            var actual = 11.0;

            Verify.InRange(actual, low, high);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a value is expected to be within a given range and fails.
        /// </summary>
        [Fact]
        public void InRangeDoubleFailedTest()
        {
            var low = 10.0;
            var high = 12.0;
            var actual = 13.0;

            Assert.Throws<InRangeException>(() => Assert.InRange(actual, low, high));
        }

        /// <summary>
        /// Verifies that a date is within a given range.
        /// </summary>
        [Fact]
        public void InRangeDateTest()
        {
            var low = new DateTime(2019, 1, 1, 0, 0, 0);
            var high = new DateTime(2019, 3, 1, 0, 0, 0);
            var actual = new DateTime(2019, 2, 1, 0, 0, 0);

            Verify.InRange(actual, low, high);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a date is expected to be within a given range and fails.
        /// </summary>
        [Fact]
        public void InRangeDateFailedTest()
        {
            var low = new DateTime(2019, 1, 1, 0, 0, 0);
            var high = new DateTime(2019, 3, 1, 0, 0, 0);
            var actual = new DateTime(2019, 4, 1, 0, 0, 0);

            Assert.Throws<InRangeException>(() => Assert.InRange(actual, low, high));
        }

        /// <summary>
        /// Verifies that an object reference is not null.
        /// </summary>
        [Fact]
        public void NotNullTest()
        {
            var test = new Point();

            Verify.NotNull(test);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when an object reference is expected to be not null and fails.
        /// </summary>
        [Fact]
        public void NotNullFailedTest()
        {
            Assert.Throws<NotNullException>(() => Assert.NotNull(null));
        }

        /// <summary>
        /// Verifies that an object reference is null.
        /// </summary>
        [Fact]
        public void NullTest()
        {
            Verify.Null(null);
        }

        /// <summary>
        ///Verifies the proper exception is thrown when an object reference is expected to be null and fails.
        /// </summary>
        [Fact]
        public void NullFailedTest()
        {

        }

        /// <summary>
        /// Verifies that two objects are not equal.
        /// </summary>
        [Fact]
        public void NotEqualTest()
        {
            var expected = new Point();
            var actual = new Size();

            Verify.NotEqual(expected, actual);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when two objects are expected to not be equal and fails.
        /// </summary>
        [Fact]
        public void NotEqualFailedTest()
        {
            var expected = new Point();
            var actual = new Point();

            Assert.Throws<NotEqualException>(() => Assert.NotEqual(expected, actual));
        }

        /// <summary>
        /// Verifies that a collection is not empty.
        /// </summary>
        [Fact]
        public void NotEmptyTest()
        {
            List<string> list = new List<string>
            {
                "test1",
                "test2",
                "test3"
            };

            Verify.NotEmpty(list);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a collection is expected to not be empty and fails.
        /// </summary>
        [Fact]
        public void NotEmptyFailedTest()
        {
            List<string> List = new List<string> { };

            Assert.Throws<NotEmptyException>(() => Assert.NotEmpty(List));
        }

        /// <summary>
        /// Verifies that a value is not within a given range.
        /// </summary>
        [Fact]
        public void NotInRangeTest()
        {
            var low = 10.0;
            var high = 12.0;
            var actual = 13.0;

            Verify.NotInRange(actual, low, high);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a value is expected to not be within a given range and fails.
        /// </summary>
        [Fact]
        public void NotInRangeFailedTest()
        {
            var low = 10.0;
            var high = 12.0;
            var actual = 11.0;

            Assert.Throws<NotInRangeException>(() => Assert.NotInRange(actual, low, high));
        }

        /// <summary>
        /// Verifies that an expression is true.
        /// </summary>
        [Fact]
        public void TrueTest()
        {
            Verify.True(1 == 1);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when an expression is expected to be true and fails.
        /// </summary>
        [Fact]
        public void TrueFailedTest()
        {
            Assert.Throws<TrueException>(() => Assert.True(1 == 2));
        }
    }
}
