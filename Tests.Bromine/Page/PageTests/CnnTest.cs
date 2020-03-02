using Bromine.Constants;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Page.PageTests
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
            Browser.Window.Maximize();

            CnnHome.Navigate();
        }

        /// <summary>
        /// Click the Health Link.
        /// </summary>
        [Fact]
        public void ClickHealthLink()
        {
            Browser.Wait.For.DisplayedElement(CnnHome.HealthLink);

            CnnHome.HealthLink.Click();

            Browser.Verify.Contains("https://www.cnn.com/health", Browser.Url);
        }
    }
}
