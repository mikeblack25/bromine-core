using Bromine.Core;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.VerifiesTests
{
    /// <summary>
    /// Extra tests outside of basic Verify behavior.
    /// </summary>
    public class VerifyExtras : Framework
    {
        /// <summary>
        /// Launch a headless browser.
        /// </summary>
        public VerifyExtras(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Load a webpage.
        /// Force a Browser.Verify.True error.
        /// Ignore forced error via xUnit Assert.Throws.
        /// Try to click a button and ensure an exception is not thrown.
        /// </summary>
        [Fact]
        public void VerifyFailsExecutionStops()
        {
            Browser.Navigate.ToFile(CommonPage.Url);

            Assert.Throws<TrueException>(() => Browser.Verify.True(false));

            var exception = Record.Exception(() => CommonPage.NextButton.Click());

            Browser.Verify.Null(exception);
        }
    }
}
