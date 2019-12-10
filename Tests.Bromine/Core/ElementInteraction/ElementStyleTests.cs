using Bromine.Constants;
using Bromine.Core.Element;

using Tests.Bromine.Common;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core.ElementInteraction
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify the ElementStyle class is working as expected.
    /// </summary>
    public class ElementStyleTests : CoreTestsBase
    {
        /// <inheritdoc />
        public ElementStyleTests(ITestOutputHelper output) : base(output)
        {
            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }

        /// <summary>
        /// VerifyBase a border can be added t oan element.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(IdLocatorString, BlackString)]
        [Theory]
        public void AddBorder(string locator, string color)
        {
            Browser.Verify.DoesNotContain(color.ToLower(), GetStyle(locator, Strategy.Id).ToLower());

            Browser.ElementStyle.AddBorder(Strategy.Id, locator, color);

            Browser.Verify.Contains(color.ToLower(), GetStyle(locator, Strategy.Id).ToLower());
        }

        /// <summary>
        /// VerifyBase a border can be added to an element.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(IdLocatorString, BlackString)]
        [Theory]
        public void AddBorderToElement(string locator, string color)
        {
            var element = Browser.Find.Element(locator.Id());

            Browser.Verify.DoesNotContain(color.ToLower(), GetStyle(locator, Strategy.Id).ToLower());

            Browser.ElementStyle.AddBorder(element, color);

            Browser.Verify.Contains(color.ToLower(), GetStyle(locator, Strategy.Id).ToLower());
        }

        /// <summary>
        /// VerifyBase borders can be added to elements.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(ClassLocatorString, RedString)]
        [Theory]
        public void AddBorders(string locator, string color)
        {
            Browser.Verify.DoesNotContain(color.ToLower(), GetStyle(locator, Strategy.Class).ToLower());

            Browser.ElementStyle.AddBorders(Strategy.Class, locator, color);

            Browser.Verify.Contains(color.ToLower(), GetStyle(locator, Strategy.Class).ToLower());
        }

        /// <summary>
        /// VerifyBase borders can be added to elements.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(ClassLocatorString, RedString)]
        [Theory]
        public void AddBordersToElement(string locator, string color)
        {
            var element = Browser.Find.Element(locator.Class());

            Browser.Verify.DoesNotContain(color.ToLower(), GetStyle(locator, Strategy.Class).ToLower());

            Browser.ElementStyle.AddBorders(element, color);

            Browser.Verify.Contains(color.ToLower(), GetStyle(locator, Strategy.Class).ToLower());
        }

        private string GetStyle(string locator, Strategy locatorStrategy)
        {
            Element element = null;

            if (locatorStrategy == Strategy.Class)
            {
                element = Browser.Find.Element(locator.Class());
            }
            else if (locatorStrategy == Strategy.Id)
            {
                element = Browser.Find.Element(locator.Id());
            }

            return Browser.ElementStyle.GetStyleAttribute(element);
        }

        private const string RedString = "Red";
        private const string BlackString = "Black";
        private const string ClassLocatorString = "gNO89b";
        private const string IdLocatorString = "gbqfbb";
    }
}
