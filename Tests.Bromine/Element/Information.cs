using System;

using Bromine.Core;
using Bromine.Element;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Element
{
    /// <summary>
    /// Tests to verify the usage of Element.Information.
    /// </summary>
    public class Information : Framework
    {
        /// <summary>
        /// Create a headless Chrome browser for all tests.
        /// Build and navigate to Common.html.
        /// </summary>
        public Information(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
        }

        /// <summary>
        /// Get information from a POM element.
        /// Request CommonPage.EnableButtonId from a POM.
        /// Check the timestamp is within the next minute.
        /// Check the locator string, strategy, and name of calling entity from the POM.
        /// </summary>
        [Fact]
        public void ElementInformation()
        {
            HeadlessInit();

            var element = CommonPage.EnableButtonId;

            Browser.SoftVerify.InRange(element.Information.CalledTimestamp, DateTime.Now, DateTime.Now.AddMinutes(1));

            Browser.SoftVerify.Equal("enabled_button", element.Information.LocatorString);
            Browser.SoftVerify.Equal(Strategy.Id, element.Information.Strategy);
            Browser.SoftVerify.Equal("EnableButtonId", element.Information.Name);
        }

        /// <summary>
        /// Create an invalid element via Browser.Find.Element.
        /// Check the Information properties are expected for an invalid Element.
        /// </summary>
        [Fact]
        public void UnInitializedElementInformation()
        {
            HeadlessInit(stopOnError: false);

            var element = Browser.Find.Element(string.Empty);

            Browser.SoftVerify.Equal(string.Empty, element.Information.LocatorString);
            Browser.SoftVerify.Equal(Strategy.Undefined, element.Information.Strategy);
            Browser.SoftVerify.Equal("UnInitializedElementInformation", element.Information.Name);
            Browser.SoftVerify.Equal(false, element.Information.IsInitialized);
        }
    }
}
