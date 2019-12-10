using System.Collections.Generic;

using Bromine.Core.ElementInteraction;
using Bromine.Core.ElementLocator;

using Tests.Bromine.Common;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core.ElementInteraction
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify <see cref="ElementExtensions"/> in the <see cref="Element"/> class is working as expected.
    /// </summary>
    public class ElementExtensionTests : CoreTestsBase
    {
        /// <inheritdoc />
        public ElementExtensionTests(ITestOutputHelper output) : base(output)
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);
            _element = Browser.Find.Element(".nav-fill");
        }

        /// <summary>
        /// Locate div elements of the element found in the test setup.
        /// </summary>
        [Fact]
        public void VerifyFindElementsOfElement()
        {
            _elementsOfElement = _element.FindElements(LocatorStrategy.Css, "div");
        }

        /// <summary>
        /// Locate a div element of the element found in the test setup.
        /// </summary>
        [Fact]
        public void VerifyFindElementOfElement()
        {
            _elementsOfElement = new List<Element> {_element.FindElement(LocatorStrategy.Css, "div")};
        }

        /// <summary>
        /// VerifyBase one or more child elements are found from the element found in the test setup.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                Browser.Verify.InRange(_elementsOfElement.Count, 1, int.MaxValue);
            }
            finally
            {
                base.Dispose();
            }
        }

        private readonly Element _element;
        private List<Element> _elementsOfElement;
    }
}
