using System;

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
            try
            {
                Nav.GoToUrl(url);
                Log.Debug($"Navigate to {url}");

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Navigate to the given file.
        /// </summary>
        /// <param name="path"></param>
        public void ToFile(string path)
        {
            try
            {
                Nav.GoToUrl($"file://{path}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            try
            {
                Nav.Back();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            try
            {
                Nav.Forward();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            try
            {
                Nav.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        private Browser Browser { get; }
        private INavigation Nav => Browser.Driver.WebDriver.Navigate();
        private Log Log => Browser.Log;
    }
}
