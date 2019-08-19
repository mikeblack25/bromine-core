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
        /// 
        /// </summary>
        public ElementStyleTests()
        {
            Browser.Navigate.ToUrl("https://www.google.com");
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
            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorType.Id).ToLower());

            Browser.ElementStyle.AddBorder(LocatorType.Id, locator, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorType.Id).ToLower());
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
            var element = Browser.Find.ElementById(locator);

            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorType.Id).ToLower());

            Browser.ElementStyle.AddBorder(element, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorType.Id).ToLower());
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
            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorType.Class).ToLower());

            Browser.ElementStyle.AddBorders(LocatorType.Class, locator, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorType.Class).ToLower());
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
            var element = Browser.Find.ElementByClass(locator);

            DoesNotContain(color.ToLower(), GetStyle(locator, LocatorType.Class).ToLower());

            Browser.ElementStyle.AddBorders(element, color);

            Contains(color.ToLower(), GetStyle(locator, LocatorType.Class).ToLower());
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

        private const string RedString = "Red";
        private const string BlackString = "Black";
        private const string ClassLocatorString = "gNO89b";
        private const string IdLocatorString = "gbqfbb";
    }
}
