namespace Bromine.Core
{
    /// <summary>
    /// Base page object. This provides access to the Browser.
    /// </summary>
    public abstract class Page
    {
        /// <inheritdoc />
        protected Page(IBrowser browser, bool isHeadless = false)
        {
            Browser = browser;
        }

        /// <summary>
        /// <see cref="Core.Browser"/>
        /// </summary>
        public IBrowser Browser { get; }

        /// <summary>
        /// Page URL.
        /// </summary>
        public abstract string Url { get; }

        /// <summary>
        /// Navigate to the page URL.
        /// </summary>
        public virtual void Navigate()
        {
            Browser.Navigate.ToUrl(Url);
        }
    }
}
