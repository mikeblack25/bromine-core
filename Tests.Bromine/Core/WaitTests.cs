﻿using System;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Wait class is working as expected.
    /// </summary>
    public class WaitTests : CoreTestsBase
    {
        /// <inheritdoc />
        /// <param name="output"><see cref="ITestOutputHelper"/></param>
        public WaitTests(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// VerifyBase the browser can wait for conditions for the time specified and that Exceptions are logged when the expected conditions are not met.
        /// </summary>
        [Fact]
        public void VerifyWaitForCondition()
        {
            var startTime = DateTime.Now;
            const int timeToWait = 5;

            Browser.Wait.For.Condition(() => false, timeToWait);

            Browser.Verify.InRange(DateTime.Now, startTime.AddSeconds(timeToWait - 1), startTime.AddSeconds(timeToWait + 1));
        }
    }
}
