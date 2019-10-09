using Bromine.Core.ElementInteraction;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core.ElementLocator
{
    /// <summary>
    /// Test to verify the Find class is working as expected.
    /// </summary>
    public class SeleniumFindTests : CoreTestsBase
    {
        /// <inheritdoc />
        public SeleniumFindTests(ITestOutputHelper output) : base(output)
        {
            Browser.Navigate.ToFile($@"{BasePath}\{AmazonHome}");
        }

        /// <summary>
        /// Find element by id.
        /// </summary>
        [Fact]
        public void FindElementsByIdTest()
        {
            _element = Browser.SeleniumFind.ElementById(IdString);
        }

        /// <summary>
        /// Find element by class.
        /// </summary>
        [Fact]
        public void FindElementsByClassTest()
        {
            _element = Browser.SeleniumFind.ElementByClass(ClassString);
        }

        /// <summary>
        /// Find element by CSS.
        /// </summary>
        [Fact]
        public void FindElementsByCssTest()
        {
            _element = Browser.SeleniumFind.ElementByCssSelector(CssSelectorString);
        }

        /// <summary>
        /// Find element by tag.
        /// </summary>
        [Fact]
        public void FindElementsByTagTest()
        {
            _element = Browser.SeleniumFind.ElementByTag(TagString);
        }

        /// <summary>
        /// Find element by text.
        /// </summary>
        [Fact]
        public void FindElementsByTextTest()
        {
            _element = Browser.SeleniumFind.ElementByText(TextString);
        }

        /// <summary>
        /// Find element by partial text.
        /// </summary>
        [Fact]
        public void FindElementsByPartialTextTest()
        {
            _element = Browser.SeleniumFind.ElementByPartialText(TextString.Substring(2));
        }

        /// <summary>
        /// Dispose of the browser when the test is done.
        /// All tests will ensure there are no errors finding elements.
        /// </summary>
        public override void Dispose()
        {
            Browser.Verify.True(_element.IsInitialized);

            Browser?.Dispose();
        }

        private Element _element;

        private static string IdString => "s-suggestion";
        private static string ClassString => "a-link-normal";
        private static string CssSelectorString => $"#{IdString}";
        private static string TagString => "div";
        private static string TextString => "Careers";
    }
}
