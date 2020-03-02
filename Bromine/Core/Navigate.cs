using OpenQA.Selenium;

namespace Bromine.Core
{
    /// <summary>
    /// Class to enable browser navigation.
    /// </summary>
    public class Navigate
    {
        /// <summary>
        /// Construct a Navigate object for the given browser type.
        /// </summary>
        /// <param name="browser">Browser used to navigate.</param>
        public Navigate(Browser browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void ToUrl(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = $"https://{url}";
            }

            Nav.GoToUrl(url);
            Log.Debug($"Navigate to {url}");
        }

        /// <summary>
        /// Navigate to the given file.
        /// </summary>
        /// <param name="path"></param>
        public void ToFile(string path)
        {
            var filePathString = @"file:\\\";

            if (!path.StartsWith(filePathString))
            {
                path = $"{filePathString}{path}";
            }

            Nav.GoToUrl(path);
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            Nav.Back();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            Nav.Forward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            Nav.Refresh();
        }

        private Browser Browser { get; }
        private INavigation Nav => Browser.Driver.WebDriver.Navigate();
        private Log Log => Browser.Log;
    }
}
