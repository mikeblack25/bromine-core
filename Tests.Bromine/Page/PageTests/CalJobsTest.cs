using Bromine.Constants;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Page.PageTests
{
    /// <summary>
    /// Check the Los Angeles job listings.
    /// </summary>
    public class CalJobsTest : PageBase
    {
        /// <summary>
        /// Navigate to check the Health page URL from CNN.com.
        /// </summary>
        public CalJobsTest(ITestOutputHelper output)
        {
            InitializePages(BrowserType.Chrome, false, output);
            Browser.Window.Maximize();

            CalJobsHome.Navigate(); 
        }

        /// <summary>
        /// Test the LA job listings.
        /// </summary>
        [Fact]
        public void CheckListings()
        {
            CalJobsHome.GeographicJobSearchButton.Click();

            Browser.Wait.For.DisplayedElement(CalJobsHome.LosAngelesCountyLink);

            CalJobsHome.LosAngelesCountyLink.Click();

            Browser.Verify.Contains("#locid=681", Browser.Url);
        }
    }
}
