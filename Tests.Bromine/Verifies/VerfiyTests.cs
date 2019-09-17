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
    /// Tests to verify the behavior of Verify methods.
    /// </summary>
    public class VerifyTests
    {
        /// <summary>
        /// Create an instance of Verify for testing its methods.
        /// </summary>
        public VerifyTests(ITestOutputHelper output)
        {
            Verify = new Verify(new Log(output));
        }

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
            const string johnDoe = "John Doe";
            const string janeDoe = "Jane Doe";

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
            Verify.Contains(IpsumString, LoremIpsumDolorSitAmetString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a sub-string is expected to be contained within a given string and fails.
        /// </summary>
        [Fact]
        public void ContainsFailedTest()
        {
            Assert.Throws<ContainsException>(() => Assert.Contains(TestString, LoremIpsumDolorSitAmetString));
        }

        /// <summary>
        /// Verifies that a sub-string is NOT contained within a given string.
        /// </summary>
        [Fact]
        public void DoesNotContainTest()
        {
            Verify.DoesNotContain(TestString, LoremIpsumDolorSitAmetString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a sub-string is NOT expected to be contained within a given string and fails.
        /// </summary>
        [Fact]
        public void DoesNotContainFailedTest()
        {
            Assert.Throws<DoesNotContainException>(() => Assert.DoesNotContain(IpsumString, LoremIpsumDolorSitAmetString));
        }

        /// <summary>
        /// Verifies that a string does NOT match a given RegEx pattern.
        /// </summary>
        [Fact]
        public void DoesNotMatchTest()
        {
            const string actualString = "1234";

            Verify.DoesNotMatch(Pattern, actualString);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a string is not expected to match a given RegEx pattern and fails.
        /// </summary>
        [Fact]
        public void DoesNotMatchFailedTest()
        {
            Assert.Throws<DoesNotMatchException>(() =>Assert.DoesNotMatch(Pattern, TestString));
        }

        /// <summary>
        /// Verifies that a collection is empty.
        /// </summary>
        [Fact]
        public void EmptyTest()
        {
            Verify.Empty(new List<string>());
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a collection is expected to be empty and fails.
        /// </summary>
        [Fact]
        public void EmptyFailedTest()
        {
            var list = new List<string>
            {
                "test1",
                "test2",
                "test3"
            };

            Assert.Throws<EmptyException>(() => Assert.Empty(list));
        }

        /// <summary>
        /// Verifies that a string ends with a given string.
        ///</summary>
        [Fact]
        public void EndsWithTest()
        {
            const string expectedEndString = "test.";

            Verify.EndsWith(expectedEndString, ThisIsATestMessage);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a string is expected to end with the expected string and fails.
        /// </summary>
        [Fact]
        public void EndsWithFailedTest()
        {
            const string expectedEndString = "This";

            Assert.Throws<EndsWithException>(() => Assert.EndsWith(expectedEndString, ThisIsATestMessage));
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
            Verify.Equal(12.3, 12.3);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when two double values are expected to be equal and fails.
        /// </summary>
        [Fact]
        public void EqualDoubleFailedTest()
        {
            Assert.Throws<EqualException>(() => Assert.Equal(12.3, 12.4));
        }

        /// <summary>
        /// Verifies that the condition is false.
        /// </summary>
        [Fact]
        public void FalseTest()
        {
            Verify.False(false);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a condition is expected to be false and fails.
        /// </summary>
        [Fact]
        public void FalseFailedTest()
        {
            Assert.Throws<FalseException>(() => Assert.False(true));
        }

        /// <summary>
        /// Verifies that a value is within a given range.
        /// </summary>
        [Fact]
        public void InRangeDoubleTest()
        {
            Verify.InRange(11.0, 10.0, 12.0);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a value is expected to be within a given range and fails.
        /// </summary>
        [Fact]
        public void InRangeDoubleFailedTest()
        {
            Assert.Throws<InRangeException>(() => Assert.InRange(13.0, 11.0, 12.0));
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
            Verify.NotNull(new Point());
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
            Verify.NotEqual(new Point(), new Size());
        }

        /// <summary>
        /// Verifies the proper exception is thrown when two objects are expected to not be equal and fails.
        /// </summary>
        [Fact]
        public void NotEqualFailedTest()
        {
            Assert.Throws<NotEqualException>(() => Assert.NotEqual(new Point(), new Point()));
        }

        /// <summary>
        /// Verifies that a collection is not empty.
        /// </summary>
        [Fact]
        public void NotEmptyTest()
        {
            var list = new List<string>
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
            Assert.Throws<NotEmptyException>(() => Assert.NotEmpty(new List<string>()));
        }

        /// <summary>
        /// Verifies that a value is not within a given range.
        /// </summary>
        [Fact]
        public void NotInRangeTest()
        {
            Verify.NotInRange(13.0, 10.0, 12.0);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when a value is expected to not be within a given range and fails.
        /// </summary>
        [Fact]
        public void NotInRangeFailedTest()
        {
            Assert.Throws<NotInRangeException>(() => Assert.NotInRange(11.0, 10.0, 12.0));
        }

        /// <summary>
        /// Verifies that an expression is true.
        /// </summary>
        [Fact]
        public void TrueTest()
        {
            Verify.True(true);
        }

        /// <summary>
        /// Verifies the proper exception is thrown when an expression is expected to be true and fails.
        /// </summary>
        [Fact]
        public void TrueFailedTest()
        {
            Assert.Throws<TrueException>(() => Assert.True(false));
        }

        private Verify Verify { get; }

        private static string Pattern => new Regex("(?:[a-z][a-z]+)").ToString();
        private static string LoremIpsumDolorSitAmetString => "Lorem ipsum dolor sit amet";
        private static string IpsumString => "ipsum";
        private static string TestString => "test";
        private static string ThisIsATestMessage => "This is a test.";
    }
}
