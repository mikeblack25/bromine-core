using System;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Wait class is working as expected.
    /// </summary>
    public class WaitTests : CoreTestsBase
    {
        /// <summary>
        /// VerifyBase the browser can wait for conditions for the time specified and that Exceptions are logged when the expected conditions are not met.
        /// </summary>
        [Fact]
        public void VerifyWaitForCondition()
        {
            Browser.Verify.Empty(Browser.Exceptions);
            Browser.Verify.False(Browser.Wait.For.Condition(() => false));
            Browser.Verify.NotEmpty(Browser.Exceptions);

            var startTime = DateTime.Now;
            const int timeToWait = 5;

            Browser.Wait.For.Condition(() => false, timeToWait);

            Browser.Verify.InRange(DateTime.Now, startTime.AddSeconds(timeToWait - 1), startTime.AddSeconds(timeToWait + 1));

            var exceptionCount = Browser.Exceptions.Count;

            Browser.Verify.True(Browser.Wait.For.Condition(() => true));
            Browser.Verify.Equal(exceptionCount, Browser.Exceptions.Count);
        }
    }
}
