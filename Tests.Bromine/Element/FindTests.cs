using System.Collections.Generic;

using Bromine;
using Bromine.Element;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Element
{
    /// <summary>
    /// Tests to verify the behavior of <see cref="Find"/> and <see cref="SeleniumFind"/>.
    /// </summary>
    public class FindTests : Framework
    {
        /// <summary>
        /// Create a headless Chrome browser for all tests.
        /// Build and navigate to Common.html.
        /// </summary>
        public FindTests(ITestOutputHelper output) : base(output, true)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Call <see cref="Find.Element"/> with all supported <see cref="Strategy"/>.
        /// Is <see cref="Strategy"/> as expected?
        /// Is the element count as expected?
        /// </summary>
        [Fact]
        public void FindByStrategyTest()
        {
            var locator = CommonPage.EnableButtonId;
            VerifyStrategy(locator, Strategy.Id);
            VerifyElementCount(locator.Information.LocatorString, 1);

            locator = CommonPage.EnableButtonClass;
            VerifyStrategy(locator, Strategy.Class);
            VerifyElementCount(locator.Information.LocatorString, 1);

            locator = CommonPage.EnableButtonCss;
            VerifyStrategy(locator, Strategy.Css);
            VerifyElementCount(locator.Information.LocatorString, 1);

            locator = CommonPage.EnableButtonText;
            VerifyStrategy(locator, Strategy.Text);
            VerifyElementCount(locator.Information.LocatorString, 1);

            locator = CommonPage.EnableButtonPartialText;
            VerifyStrategy(locator, Strategy.PartialText);
            VerifyElementCount(locator.Information.LocatorString, 4);
        }

        /// <summary>
        /// Call the following with an invalid locator string.
        /// <see cref="SeleniumFind.ElementById"/>
        /// <see cref="SeleniumFind.ElementByClass"/>
        /// <see cref="SeleniumFind.ElementByCssSelector"/>
        /// <see cref="SeleniumFind.ElementByText"/>
        /// <see cref="SeleniumFind.ElementByPartialText"/>
        /// Does <see cref="SeleniumFind.Element"/> returns null when an undefined <see cref="Strategy"/> is provided?
        /// </summary>
        [Fact]
        public void SeleniumFindInvalidStrategy()
        {
            VerifyInvalidElement(CommonPage.EnableButtonInvalidSeleniumId);
            VerifyInvalidElement(CommonPage.EnableButtonInvalidSeleniumClass);
            VerifyInvalidElement(CommonPage.EnableButtonInvalidSeleniumCss);
            VerifyInvalidElement(CommonPage.Browser.SeleniumFind.ElementByText(CommonPage.InvalidString));
            VerifyInvalidElement(CommonPage.Browser.SeleniumFind.ElementByText(null));
            VerifyInvalidElement(CommonPage.EnableButtonInvalidSeleniumPartialText);

            Browser.Verify.Null(SeleniumFind.Element(Strategy.Undefined, string.Empty));
    }

        /// <summary>
        /// <see cref="Find.ElementByClasses"/>
        /// Is the expected element displayed?
        /// Is the expected element count found?
        /// </summary>
        [Fact]
        public void ElementByClassesTest()
        {
            var element = CommonPage.EnabledButtonElementClasses;

            Log.Message($"Find.ElementByClasses by {element.Information.LocatorString} and {element.Information.Strategy}");
            Browser.SoftVerify.True(element.Displayed);
            Browser.SoftVerify.Equal(1, CommonPage.EnabledButtonElementsClasses.Count);
        }

        /// <summary>
        /// Find the child element of an element. Both the parent and child elements are located by CSS selector.
        /// </summary>
        [Fact]
        public void FindChildElementTest()
        {
            var element = CommonPage.EnabledButtonChildElement;

            Log.Message($"Find.ChildElement by {element.Information.LocatorString} and {element.Information.Strategy}");
            Browser.SoftVerify.True(element.Displayed);
            Browser.SoftVerify.True(CommonPage.EnabledButtonChildElementParentElement.Displayed);
            Browser.SoftVerify.True(CommonPage.EnabledButtonDescendentCssElement.Displayed);
            Browser.SoftVerify.Equal(1, CommonPage.EnabledButtonChildElements.Count);
            Browser.SoftVerify.Equal(2, CommonPage.EnabledButtonDescendentCssElements.Count);
        }

        private void VerifyStrategy(IElement element, Strategy expectedStrategy)
        {
            Log.Message($"Find.Element by {element.Information.LocatorString} and {element.Information.Strategy}");
            Browser.SoftVerify.Equal(expectedStrategy, element.Information.Strategy);
        }

        private void VerifyInvalidElement(IElement element)
        {
            Browser.SoftVerify.False(element.IsInitialized);
            Browser.SoftVerify.Null(element.SeleniumElement);
        }

        private void VerifyElementCount(string locator, int expectedCount)
        {
            Elements = Browser.Find.Elements(locator);

            Browser.SoftVerify.Equal(expectedCount, Elements.Count);
        }

        private List<IElement> Elements { get; set; }
    }
}
