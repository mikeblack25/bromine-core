using Bromine.Constants;

using OpenQA.Selenium;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Page.PageTests
{
    /// <summary>
    /// To test that searching for a particular term in Google leads to the correct page of results.
    /// </summary>
    public class GoogleSearchHomeTest : PageBase
    {
        /// <summary>
        /// Navigate to check the Health page URL from CNN.com.
        /// </summary>
        public GoogleSearchHomeTest(ITestOutputHelper output)
        {
            InitializePages(BrowserType.Chrome, false, output);
            Browser.Window.Maximize();

            GoogleSearchHome.Navigate();
        }

        /// <summary>
        /// Search for a term on Google.
        /// </summary>
        [Fact]
        public void CheckGoogleSearchTerm()
        {
            GoogleSearchHome.GoogleSearchField.SendKeys("Lamps & Fixtures");
            GoogleSearchHome.GoogleSearchField.SendKeys(Keys.Enter);

            var expectedPageTitle = "Light Fixtures - Indoor & Outdoor Lighting | Lamps Plus";
            var actualTitle = GoogleSearchHome.GoogleSearchResults.Text;

            Browser.Verify.Equal(expectedPageTitle, actualTitle);
        }
    }
}
