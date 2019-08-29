using Tests.Bromine.Common;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Test to verify the Find class is working as expected.
    /// </summary>
    public class FindTests : CoreTestsBase
    {
        /// <inheritdoc />
        /// <summary>
        /// Navigate to the Amazon home page.
        /// </summary>
        public FindTests()
        {
            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }
        
        /// <summary>
        /// Find element with all the following classes.
        /// "gb_ee" "gb_g" "gb_Dg" "gb_ug"
        /// </summary>
        [Fact]
        public void FindElementByClassesTest()
        {
            var element = Browser.Find.ElementByClasses("gb_ee", "gb_g", "gb_Dg", "gb_ug");

            True(element.Displayed);
        }

        /// <summary>
        /// Find the elements with all the following classes.
        /// "gb_f" "gb_g"
        /// </summary>
        [Fact]
        public void FindElementsByClassesTest()
        {
            var elements = Browser.Find.ElementsByClasses("gb_f", "gb_g");

            Equal(2, elements.Count);
        }

        /// <summary>
        /// Find element by nested classes.
        /// class -> gb_ee
        ///   class -> gb_f
        ///     class -> gb_e
        /// </summary>
        [Fact]
        public void FindElementByDescendentClassTest()
        {
            const string gmailString = "Gmail";

            var element = Browser.Find.ElementByDescendentClass("gb_ee", "gb_f", "gb_e");

            Equal(gmailString, element.Text);
        }

        /// <summary>
        /// Find elements by nested classes.
        /// class -> gb_ee
        ///   class -> gb_f
        ///   class -> gb_f
        /// </summary>
        [Fact]
        public void FindElementsByDescendentClassTest()
        {
            var elements = Browser.Find.ElementsByDescendentClass("gb_ee", "gb_f");

            Equal(2, elements.Count);
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
