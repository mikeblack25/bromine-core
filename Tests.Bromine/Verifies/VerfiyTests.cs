using Bromine.Verifies;

using System;
using System.Collections.Generic;

using Xunit;
using Xunit.Sdk;

namespace Tests.Bromine.Verifies
{
    /// <summary>
    /// Tests to verify the Verify methods
    /// </summary>
    public class VerfiyTests
    {
        /// <summary>
        /// 
        /// </summary>
        public VerfiyTests()
        {
            Verify = new Verify(new List<Exception>());
        }

        /// <summary>
        /// Verify property.
        /// </summary>
        public Verify Verify { get; }

        /// <summary>
        /// i8
        /// </summary>
        [Fact]
        public void ContainsTest()
        {
            var expectedSubString = "ipsum";
            var actualString = "Lorem ipsum dolor sit amet";

            Verify.Contains(expectedSubString, actualString);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ContainsFailedTest()
        {
            var expectedSubString = "test";
            var actualString = "Lorem ipsum dolor sit amet";

            Assert.Throws<ContainsException>(() => Assert.Contains(expectedSubString, actualString));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void DoesNotContainTest()
        {
            var expectedSubString = "test";
            var actualString = "Lorem ipsum dolor sit amet";

            Verify.DoesNotContain(expectedSubString, actualString);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void DoesNotContainFailedTest()
        {
            var expectedSubString = "ipsum";
            var actualString = "Lorem ipsum dolor sit amet";

            Assert.Throws<DoesNotContainException>(() => Assert.DoesNotContain(expectedSubString, actualString));
        }
    }
}
