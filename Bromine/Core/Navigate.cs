namespace Bromine.Core
{
    /// <summary>
    /// Class to enable with browser navigation.
    /// </summary>
    public class Navigate
    {
        /// <summary>
        /// Construct a Navigate object for the given driver type.
        /// </summary>
        /// <param name="driver"></param>
        public Navigate(Driver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void ToUrl(string url)
        {
            _driver.NavigateToUrl(url);
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            _driver.NavigateBack();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            _driver.NavigateForward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            _driver.Refresh();
        }

        private Driver _driver { get; }
    }
}
