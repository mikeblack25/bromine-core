using Bromine.Core;

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
        /// <summary>
        /// Verify borders can be added to elements.
        /// </summary>
        [InlineData("gNO89b", "Red", LocatorType.Class)]
        [Theory]
        public void AddBorders(string locator, string color, LocatorType locatorStrategy)
        {
            Browser.Navigate.ToUrl("https://www.google.com");

            DoesNotContain(color.ToLower(), GetStyle(locator, locatorStrategy).ToLower());

            Browser.ElementStyle.AddBorders(locatorStrategy, locator, color);

            Contains(color.ToLower(), GetStyle(locator, locatorStrategy).ToLower());
        }

        /// <summary>
        /// Verify a border can be added t oan element.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="color"></param>
        /// <param name="locatorStrategy"></param>
        [InlineData("gbqfbb", "Black", LocatorType.Id)]
        [Theory]
        public void AddBorder(string locator, string color, LocatorType locatorStrategy)
        {
            Browser.Navigate.ToUrl("https://www.google.com");

            DoesNotContain(color.ToLower(), GetStyle(locator, locatorStrategy).ToLower());

            Browser.ElementStyle.AddBorder(locatorStrategy, locator, color);

            Contains(color.ToLower(), GetStyle(locator, locatorStrategy).ToLower());
        }

        private string GetStyle(string locator, LocatorType locatorStrategy)
        {
            Element element = null;

            if (locatorStrategy == LocatorType.Class)
            {
                element = Browser.Find.ElementByClass(locator);
            }
            else if (locatorStrategy == LocatorType.Id)
            {
                element = Browser.Find.ElementById(locator);
            }

            return Browser.ElementStyle.GetStyleAttribute(element);
        }
    }
}
