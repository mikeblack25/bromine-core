namespace Bromine.Extensions
{
    /// <summary>
    /// Format extension methods for CSS.
    /// </summary>
    public static class CssFormat
    {
        /// <summary>
        /// Format the class locator for CSS.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static string Class(this string locator) => $".{locator}";

        /// <summary>
        /// Format the ID locator for CSS.
        /// </summary>
        /// <param name="locator">Add Id formatting for CSS.</param>
        /// <returns></returns>
        public static string Id(this string locator) => $"#{locator}";
    }
}
