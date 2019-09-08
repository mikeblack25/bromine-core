using Bromine.Core.ElementLocator;

using Xunit;

namespace Tests.Bromine.Core.ElementLocator
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
            var element = Browser.SeleniumFind.ElementById(IdString);

            Browser.Verify.Equal(LocatorStrategy.Id, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Find element by class.
        /// </summary>
        [Fact]
        public void FindElementsByClassTest()
        {
            var element = Browser.SeleniumFind.ElementByClass(ClassString);

            Browser.Verify.Equal(LocatorStrategy.Class, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Find element by CSS.
        /// </summary>
        [Fact]
        public void FindElementsByCssTest()
        {
            var element = Browser.SeleniumFind.ElementByCssSelector(CssSelectorString);

            Browser.Verify.Equal(LocatorStrategy.Css, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Find element by tag.
        /// </summary>
        [Fact]
        public void FindElementsByTagTest()
        {
            var element = Browser.SeleniumFind.ElementByTag(TagString);

            Browser.Verify.Equal(LocatorStrategy.Tag, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Find element by text.
        /// </summary>
        [Fact]
        public void FindElementsByTextTest()
        {
            var element = Browser.SeleniumFind.ElementByText(TextString);

            Browser.Verify.Equal(LocatorStrategy.Text, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Find element by partial text.
        /// </summary>
        [Fact]
        public void FindElementsByPartialTextTest()
        {
            var element = Browser.SeleniumFind.ElementByPartialText(TextString.Substring(2));

            Browser.Verify.Equal(LocatorStrategy.PartialText, element.Information.LocatorStrategy);
        }

        /// <summary>
        /// Dispose of the browser when the test is done.
        /// All tests will ensure there are no errors finding elements.
        /// </summary>
        public override void Dispose()
        {
            Browser?.Dispose();

            Assert.True(ErrorList.Count == 0, string.Join(",", ErrorList));
        }

        private static string IdString => "s-suggestion";
        private static string ClassString => "a-link-normal";
        private static string CssSelectorString => $"#{IdString}";
        private static string TagString => "div";
        private static string TextString => "Careers";
    }
}
