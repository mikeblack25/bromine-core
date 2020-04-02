using System.Collections;
using System.Collections.Generic;

using System.Linq;

using OpenQA.Selenium.Interactions;

namespace Bromine.Element
{
    /// <summary>
    /// 
    /// </summary>
    public static class ElementBehaviorExtensions
    {
        /// <summary>
        /// Find child elements with the given strategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="strategy">How will the element be found?</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<IElement> FindElements(this Element element, Strategy strategy, string locator) => Element.ToList(element.SeleniumElement.FindElements(SeleniumFind.Element(strategy, locator)), element.Browser, locator, strategy);

        /// <summary>
        /// Find child elements by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<IElement> FindElements(this Element element, string locator) => element.FindElements(Strategy.Css, locator);

        /// <summary>
        /// Find child element by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static IElement FindElement(this Element element, string locator) => FindElements(element, Strategy.Css, locator).FirstOrDefault();

        /// <summary>
        /// Get all element attributes.
        /// </summary>
        /// <param name="element">Element to get the attributes of.</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetAttributes(this IElement element)
        {
            var dictionary = new Dictionary<string, object>();

            if (element.Information.IsInitialized)
            {
                var objects = (element as Element)?.Browser.ExecuteJs("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", element);
                var strings = ((IEnumerable) objects).Cast<object>().Select(x => x?.ToString()).ToArray();

                foreach (var attribute in strings)
                {
                    var attributes = attribute.Split(new[] {'[', ']', ','});
                    dictionary.Add(attributes[1], attributes[2]);
                }

                dictionary.Add("text", element.Text);
            }

            return dictionary;
        }
    }
}
