using System;

using Xunit;

using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Wait class is working as expected.
    /// </summary>
    public class WaitTests : CoreTestsBase
    {
        /// <summary>
        /// Verify the browser can wait for conditions for the time specified and that Exceptions are logged when the expected conditions are not met.
        /// </summary>
        [Fact]
        public void VerifyWaitForCondition()
        {
            Empty(Browser.Exceptions);
            False(Browser.Wait.For.Condition(() => false));
            NotEmpty(Browser.Exceptions);

            var startTime = DateTime.Now;
            const int timeToWait = 5;

            Browser.Wait.For.Condition(() => false, timeToWait);

            InRange(DateTime.Now, startTime.AddSeconds(timeToWait - 1), startTime.AddSeconds(timeToWait + 1));

            var exceptionCount = Browser.Exceptions.Count;

            True(Browser.Wait.For.Condition(() => true));
            Equal(exceptionCount, Browser.Exceptions.Count);
        }
    }
}
