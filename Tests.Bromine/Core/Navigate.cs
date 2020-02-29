using System;
using System.Globalization;

using Bromine.Core;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests the behavior of Core.Window.
    /// </summary>
    public class Navigate : Framework
    {
        /// <summary>
        /// Launch a browser in headless mode.
        /// </summary>
        public Navigate(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Can a remote URL be navigated to using Navigate.ToUrl?
        /// </summary>
        [InlineData(GoogleUrl)]
        [InlineData("www.google.com")]
        [Theory]
        public void ToUrl(string url)
        {
            Browser.Navigate.ToUrl(url);

            Browser.Verify.Contains(GoogleUrl, Browser.Url);
        }

        /// <summary>
        /// Can a file be navigated to from a local file using Navigate.ToFile?
        /// </summary>
        /// <param name="explicitFileStringInPath"></param>
        [InlineData(false)]
        [InlineData(true)]
        [Theory]
        public void ToFile(bool explicitFileStringInPath)
        {
            var expectedUrl = !explicitFileStringInPath ? CommonPage.Url : $@"file:\\{CommonPage.Url}";

            Browser.Navigate.ToFile(expectedUrl);

            Browser.Verify.Contains(expectedUrl, Browser.Url);
        }

        /// <summary>
        /// Can a previous page be loaded (if one exists in the navigation chain)  using Navigate.Back?
        /// </summary>
        [Fact]
        public void Back()
        {
            var expectedUrl = CommonPage.Url;

            Browser.Navigate.ToFile(expectedUrl);
            CommonPage.NextButton.Click();

            Browser.ConditionalVerify.Contains(NewPageTitle, Browser.Title, ExpectedTitleNotFoundMessage);

            Browser.Navigate.Back();

            Browser.Verify.Contains(CommonPage.Url, Browser.Url);
        }

        /// <summary>
        /// An error is not thrown if when Navigate.Back() is called without previous navigation.
        /// </summary>
        [Fact]
        public void BackNoPreviousPage()
        {
            var exception = Record.Exception(() => Browser.Navigate.Back());

            Browser.Verify.Null(exception);
        }

        /// <summary>
        /// Can the next page be loaded (if one exists in the navigation chain) using Navigate.Forward?
        /// </summary>
        [Fact]
        public void Forward()
        {
            Back();

            Browser.Navigate.Forward();

            Browser.ConditionalVerify.Contains(NewPageTitle, Browser.Title, ExpectedTitleNotFoundMessage);
        }

        /// <summary>
        /// An error is not thrown if when Navigate.Forward() is called without previous navigation.
        /// </summary>
        [Fact]
        public void ForwardNoPreviousPage()
        {
            var exception = Record.Exception(() => Browser.Navigate.Forward());

            Browser.Verify.Null(exception);
        }

        /// <summary>
        /// Can the browser be refreshed?
        /// </summary>
        [Fact]
        public void Refresh()
        {
            Browser.Navigate.ToFile(CommonPage.Url);

            var firstTime = GetTimeFromString(CommonPage.LastUpdatedLabel.Text);

            Browser.Wait.ForTime(1);
            Browser.Navigate.Refresh();

            var secondTime = GetTimeFromString(CommonPage.LastUpdatedLabel.Text);

            Browser.Verify.InRange(secondTime, firstTime, firstTime.AddSeconds(2));
        }

        private DateTime GetTimeFromString(string time)
        {
            return DateTime.ParseExact(time.Split(' ')[4], "HH:mm:ss", CultureInfo.InvariantCulture);
        }

        private const string GoogleUrl = "https://www.google.com";
        private const string NewPageTitle = "New Page";
        private const string ExpectedTitleNotFoundMessage = "Expected title not found";
    }
}
