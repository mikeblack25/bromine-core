namespace Bromine
{
    /// <summary>
    /// Page Object interface.
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// <see cref="Core.Browser"/>
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Page URL.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Base address for relative URLs.
        /// Use a base address to resolve environment specific URLs.
        /// </summary>
        string BaseAddress { get; }

        /// <summary>
        /// Navigate to the page URL.
        /// </summary>
        void Navigate();
    }
}
