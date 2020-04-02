using Bromine.Core;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Element
{
    /// <summary>
    /// Tests to verify the behavior of ElementStyle.
    /// </summary>
    public class ElementStyle : Framework
    {
        /// <summary>
        /// Create a headless Chrome browser for all tests.
        /// Build and navigate to Common.html.
        /// </summary>
        public ElementStyle(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Add borders and check the style attribute is updated correctly.
        /// </summary>
        [Fact]
        public void ElementBorderStyleTests()
        {
            var expectedColor = "red";
            var expectedStyle = "border-color: red;";

            Browser.ElementStyleExtensions.AddBorder(CommonPage.DisabledButton, expectedColor);

            var styleAttribute = Browser.ElementStyleExtensions.GetStyleAttribute(CommonPage.DisabledButton);
            Browser.SoftVerify.Equal(expectedStyle, styleAttribute);

            var borderElement = Browser.Find.Element(".button");
            Browser.ElementStyleExtensions.AddBorders(borderElement, expectedColor);
            // TODO: Verify all ".button" elements have a border.
        }
    }
}
