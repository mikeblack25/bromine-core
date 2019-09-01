using System.Collections.Generic;

using Bromine.Core.ElementLocator;

namespace Bromine.Core.ElementInteraction
{
    /// <summary>
    /// Extension methods to provide additional capabilities to Elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Find child elements with the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="locatorStrategy">How will the element be found?</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<Element> FindElements(this Element element, LocatorStrategy locatorStrategy, string locator) => element.FindElements(SeleniumFind.Element(locatorStrategy, locator));

        /// <summary>
        /// Find child elements by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<Element> FindElements(this Element element, string locator) => element.FindElements(SeleniumFind.Element(LocatorStrategy.Css, locator));

        /// <summary>
        /// Find child element with the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="locatorStrategy">How will the element be found.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static Element FindElement(this Element element, LocatorStrategy locatorStrategy, string locator) => FindElements(element, locatorStrategy, locator)[0];

        /// <summary>
        /// Find child element by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static Element FindElement(this Element element, string locator) => FindElements(element, LocatorStrategy.Css, locator)[0];
    }
}
