using Bromine.Logger;
using Bromine.Verifies;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.Verifies
{
    /// <summary>
    /// Tests to verify the Verify methods
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
        /// Verifies that a sub-string is NOT contained within a given string and throws the proper exception.
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
        /// Verifies that a sub-string is contained within a given string and throws the proper exception.
        /// </summary>
        [Fact]
        public void DoesNotContainFailedTest()
        {
            var expectedSubString = "ipsum";
            var actualString = "Lorem ipsum dolor sit amet";

            Assert.Throws<DoesNotContainException>(() => Assert.DoesNotContain(expectedSubString, actualString));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void DoesNotMatchTest()
        {
            var expectedRegexPattern = "";
            var actualString = "";

            Verify.DoesNotMatch(expectedRegexPattern, actualString);
        }
    }
}
