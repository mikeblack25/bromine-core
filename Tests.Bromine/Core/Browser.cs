using System;

using Bromine.Core;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests the behavior of Core.Window.
    /// </summary>
    public class Browser : Framework
    {
        /// <summary>
        /// Launch a browser in headless mode.
        /// </summary>
        public Browser(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
        }

        /// <summary>
        /// Can a remote URL be navigated to using Navigate.ToUrl?
        /// </summary>
        [Fact]
        public void StopOnError()
        {
            HeadlessInit(stopOnError: true);

            Assert.Throws<InvalidSelectorException>(() => Browser.Find.Element(string.Empty));
        }

        /// <summary>
        /// Can a remote URL be navigated to using Navigate.ToUrl?
        /// </summary>
        [Fact]
        public void DontStopOnError()
        {
            HeadlessInit(stopOnError: false);

            Browser.Find.Element(string.Empty);

            Browser.SoftVerify.True(CommonPage.EnableButtonId.Information.IsInitialized);
            Browser.SoftVerify.Equal(1, Browser.Log.ErrorCount);
        }
    }
}
