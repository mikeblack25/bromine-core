using Xunit;

using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Test to verify the Find class is working as expected.
    /// </summary>
    public class SeleniumFindTests : CoreTestsBase
    {
        /// <inheritdoc />
        /// <summary>
        /// Navigate to the Amazon home page.
        /// </summary>
        public SeleniumFindTests()
        {
            Browser.Navigate.ToFile($@"{BasePath}\{AmazonHome}");
        }

        /// <summary>
        /// Find element by id.
        /// </summary>
        [Fact]
        public void FindElementsByIdTest()
        {
            Browser.SeleniumFind.ElementById(IdString);
        }

        /// <summary>
        /// Find element by class.
        /// </summary>
        [Fact]
        public void FindElementsByClassTest()
        {
            Browser.Find.ElementByClass(ClassString);
        }

        /// <summary>
        /// Find element by CSS.
        /// </summary>
        [Fact]
        public void FindElementsByCssTest()
        {
            Browser.SeleniumFind.ElementByCssSelector(CssSelectorString);
        }

        /// <summary>
        /// Find element by tag.
        /// </summary>
        [Fact]
        public void FindElementsByTagTest()
        {
            Browser.SeleniumFind.ElementByTag(TagString);
        }

        /// <summary>
        /// Find element by text.
        /// </summary>
        [Fact]
        public void FindElementsByTextTest()
        {
            Browser.SeleniumFind.ElementByText(TextString);
        }

        /// <summary>
        /// Find element by partial text.
        /// </summary>
        [Fact]
        public void FindElementsByPartialTextTest()
        {
            Browser.SeleniumFind.ElementByPartialText(TextString.Substring(2));
        }

        /// <summary>
        /// Dispose of the browser when the test is done.
        /// All tests will ensure there are no errors finding elements.
        /// </summary>
        public override void Dispose()
        {
            Browser?.Dispose();

            True(ErrorList.Count == 0, string.Join(",", ErrorList));
        }

        private static string IdString => "s-suggestion";
        private static string ClassString => "a-link-normal";
        private static string CssSelectorString => $"#{IdString}";
        private static string TagString => "div";
        private static string TextString => "Careers";
    }
}
