namespace Bromine.Core
{
    /// <summary>
    /// Base page object. This provides access to the Browser.
    /// </summary>
    public class Page : IPage
    {
        /// <inheritdoc />
        protected Page(IBrowser browser, string url = "", string baseAddress = "")
        {
            Browser = browser;

            BaseAddress = baseAddress;
            Url = !string.IsNullOrWhiteSpace(BaseAddress) ? $"{BaseAddress}{url}" : url;
        }

        /// <inheritdoc />
        public IBrowser Browser { get; }

        /// <inheritdoc />>
        public string Url { get; }

        /// <inheritdoc />
        public string BaseAddress { get; }

        /// <inheritdoc />
        public virtual void Navigate()
        {
            Browser.Navigate.ToUrl(Url);
        }
    }
}
