using Bromine.Core;
using Bromine.Extensions;

using Tests.Bromine.Common;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify the ElementStyle class is working as expected.
    /// </summary>
    public class ElementStyleTests : CoreTestsBase
    {
        /// <inheritdoc />
        public ElementStyleTests()
        {
            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }

        /// <summary>
        /// Verify a border can be added t oan element.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(IdLocatorString, BlackString)]
        [Theory]
        public void AddBorder(string locator, string color)
        {
            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorStrategy.Id).ToLower());

            Browser.ElementStyle.AddBorder(LocatorStrategy.Id, locator, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorStrategy.Id).ToLower());
        }

        /// <summary>
        /// Verify a border can be added to an element.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(IdLocatorString, BlackString)]
        [Theory]
        public void AddBorderToElement(string locator, string color)
        {
            var element = Browser.Find.Element(locator.Id());

            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorStrategy.Id).ToLower());

            Browser.ElementStyle.AddBorder(element, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorStrategy.Id).ToLower());
        }

        /// <summary>
        /// Verify borders can be added to elements.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(ClassLocatorString, RedString)]
        [Theory]
        public void AddBorders(string locator, string color)
        {
            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorStrategy.Class).ToLower());

            Browser.ElementStyle.AddBorders(LocatorStrategy.Class, locator, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorStrategy.Class).ToLower());
        }

        /// <summary>
        /// Verify borders can be added to elements.
        /// </summary>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        [InlineData(ClassLocatorString, RedString)]
        [Theory]
        public void AddBordersToElement(string locator, string color)
        {
            var element = Browser.Find.Element(locator.Class());

            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorStrategy.Class).ToLower());

            Browser.ElementStyle.AddBorders(element, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorStrategy.Class).ToLower());
        }

        private string GetStyle(string locator, LocatorStrategy locatorStrategy)
        {
            Element element = null;

            if (locatorStrategy == LocatorStrategy.Class)
            {
                element = Browser.Find.Element(locator.Class());
            }
            else if (locatorStrategy == LocatorStrategy.Id)
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
