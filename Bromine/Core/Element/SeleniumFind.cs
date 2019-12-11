using System;
using System.Collections.Generic;
using System.Linq;

using Bromine.Logger;

using OpenQA.Selenium;

namespace Bromine.Core.Element
{
    /// <summary>
    /// Find elements using Selenium location strategies.
    /// <see cref="Strategy"/> for supported location strategies.
    /// </summary>
    public class SeleniumFind
    {
        /// <summary>
        /// Construct a Find object to locate elements.
        /// </summary>
        /// <param name="browser"><see cref="Browser"/></param>
        public SeleniumFind(Browser browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// Find Element by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public Element ElementById(string id)
        {
            var elements = ElementsById(id);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: id, locatorType: Strategy.Id);
        }

        /// <summary>
        /// Find Elements by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public List<Element> ElementsById(string id) => Elements(Strategy.Id, id);

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public Element ElementByClass(string className)
        {
            var elements = ElementsByClass(className);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: className, locatorType: Strategy.Class);
        }

        /// <summary>
        /// Find Elements by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByClass(string className) => Elements(Strategy.Class, className);

        /// <summary>
        /// Find Element by CSS selector.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element.</param>
        /// <returns></returns>
        public Element ElementByCssSelector(string cssSelector)
        {
            var elements = ElementsByCssSelector(cssSelector);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: cssSelector, locatorType: Strategy.Css);
        }

        /// <summary>
        /// Find all Elements by CSS selector.
        /// </summary>
        /// <param name="cssSelector">Locate element by CSS selector.</param>
        /// <returns></returns>
        public List<Element> ElementsByCssSelector(string cssSelector) => Elements(Strategy.Css, cssSelector);

        /// <summary>
        /// Find Element by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByText(string text)
        {
            var elements = ElementsByText(text);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: text, locatorType: Strategy.Text);
        }

        /// <summary>
        /// Find Elements by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByText(string text) => Elements(Strategy.Text, text);

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByPartialText(string partialText)
        {
            var elements = ElementsByPartialText(partialText);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: partialText, locatorType: Strategy.PartialText);
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByPartialText(string partialText) => Elements(Strategy.PartialText, partialText);

        /// <summary>
        /// Locate elements by strategy and locator string.
        /// </summary>
        /// <param name="strategy">How will elements be found?</param>
        /// <param name="locator">String to locate elements based on the provided locationStrategy.</param>
        /// <returns></returns>
        public List<Element> Elements(Strategy strategy, string locator)
        {
            var elementsList = new List<Element>();

            try
            {
                var elements = Driver.FindElements(Element(strategy, locator));

                switch (strategy)
                {
                    case Strategy.Class:
                    case Strategy.Css:
                    case Strategy.Id:
                    {
                        foreach (var element in elements)
                        {
                            elementsList.Add(new Element(element, Log, locator, strategy));
                        }

                        break;
                    }
                    case Strategy.Text:
                    case Strategy.PartialText:
                    {
                        var containsList = new List<Element>();
                        elements = Driver.FindElements(Element(Strategy.Css, "*"));

                        foreach (var element in elements)
                        {
                            if (element.Text.Equals(locator))
                            {
                                elementsList.Add(new Element(element, Log, locator, strategy));
                            }
                            else if (element.Text.Contains(locator))
                            {
                                containsList.Add(new Element(element, Log, locator, strategy));
                            }
                        }

                        if (strategy == Strategy.PartialText)
                        {
                            elementsList = containsList;
                        }

                        break;
                    }
                        
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return elementsList;
        }

        /// <summary>
        /// Get By object based on the strategy and locator string.
        /// </summary>
        /// <param name="strategy">How will the element be located?</param>
        /// <param name="locator">String used to find the element.</param>
        /// <returns></returns>
        internal static By Element(Strategy strategy, string locator)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (strategy)
            {
                case Strategy.Class:
                {
                    return By.ClassName(locator);
                }
                case Strategy.Css:
                {
                    return By.CssSelector(locator);
                }
                case Strategy.Id:
                {
                    return By.Id(locator);
                }
                case Strategy.PartialText:
                {
                    return By.PartialLinkText(locator);
                }
                case Strategy.Text:
                {
                    return By.LinkText(locator);
                }
                default:
                {
                    return null;
                }
            }
        }

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
        private Log Log => Browser.Log;
    }
}
