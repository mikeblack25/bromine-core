using Bromine.Constants;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Page.GridTesting
{
    /// <summary>
    /// Test to check the Health page URL from CNN.com.
    /// </summary>
    public class CnnTest : PageBase
    {
        /// <summary>
        /// Navigate to check the Health page URL from CNN.com.
        /// </summary>
        public CnnTest(ITestOutputHelper output)
        {
            InitializePages(BrowserType.Chrome, false, output);

            Browser.Navigate.ToUrl("https://www.cnn.com");
        }

        /// <summary>
        /// Click the Health Link.
        /// </summary>
        [Fact]
        public void ClickHealthLink()
        {
            Browser.Wait.For.DisplayedElement(GridTesting.HealthLink);

            GridTesting.HealthLink.Click();

            Browser.Verify.Contains("https://www.cnn.com/health", Browser.Url);
        }
    }
}
