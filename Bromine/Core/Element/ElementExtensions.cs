using System.Collections.Generic;

namespace Bromine.Core.Element
{
    /// <summary>
    /// Extension methods to provide additional capabilities to Elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Find child elements with the given strategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="strategy">How will the element be found?</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<Element> FindElements(this Element element, Strategy strategy, string locator) => element.FindElements(SeleniumFind.Element(strategy, locator));

        /// <summary>
        /// Find child elements by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<Element> FindElements(this Element element, string locator) => element.FindElements(SeleniumFind.Element(Strategy.Css, locator));

        /// <summary>
        /// Find child element with the given strategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="strategy">How will the element be found.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static Element FindElement(this Element element, Strategy strategy, string locator) => FindElements(element, strategy, locator)[0];

        /// <summary>
        /// Find child element by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static Element FindElement(this Element element, string locator) => FindElements(element, Strategy.Css, locator)[0];
    }
}
