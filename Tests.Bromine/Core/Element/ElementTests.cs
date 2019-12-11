using System.Drawing;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core.Element
{
    /// <summary>
    /// Tests to verify the behavior of Element.
    /// </summary>
    public class ElementTests : Framework
    {
        /// <summary>
        /// Create a headless Chrome browser for all tests.
        /// Build and navigate to Common.html.
        /// </summary>
        public ElementTests(ITestOutputHelper output) : base(output, true)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Test behavior of Element.
        /// </summary>
        [Fact]
        public void ElementAttributes()
        {
            Browser.SoftVerify.Equal("button", CommonPage.EnableButtonId.TagName);
            Browser.SoftVerify.Equal("Enabled", CommonPage.EnableButtonId.Text);
            Browser.SoftVerify.Equal(true, CommonPage.EnableButtonId.Enabled);

            Browser.SoftVerify.Equal(false, CommonPage.EnableButtonId.Selected);
            CommonPage.EnableButtonId.Click();

            Browser.SoftVerify.Equal(new Point(8, 8), CommonPage.EnableButtonId.Location);
            Browser.SoftVerify.Equal(new Size(65, 21), CommonPage.EnableButtonId.Size);
            Browser.SoftVerify.Equal(true, CommonPage.EnableButtonId.Displayed);

            var expectedText = "Enabled Disabled";
            Browser.SoftVerify.Equal(expectedText, CommonPage.EnableButtonId.ParentElement.Text);

            var stringToEnter = "123";
            Browser.SoftVerify.Equal(string.Empty, CommonPage.ExampleField.Text);
            CommonPage.ExampleField.SendKeys(stringToEnter);
            Browser.SoftVerify.Equal(stringToEnter, CommonPage.ExampleField.GetAttribute("value"));
            CommonPage.ExampleField.Clear();
            Browser.SoftVerify.Equal(string.Empty, CommonPage.ExampleField.Text);
        }
    }
}
