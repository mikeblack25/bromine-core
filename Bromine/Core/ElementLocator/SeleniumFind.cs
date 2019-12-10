using System;
using System.Collections.Generic;
using System.Linq;

using Bromine.Core.ElementInteraction;
using Bromine.Logger;

using OpenQA.Selenium;

namespace Bromine.Core.ElementLocator
{
    /// <summary>
    /// Find elements using Selenium location strategies.
    /// <see cref="LocatorStrategy"/> for supported location strategies.
    /// </summary>
    public class SeleniumFind
    {
        /// <summary>
        /// Construct a Find object to locate elements.
        /// </summary>
        /// <param name="driver">Driver used to navigate.</param>
        /// <param name="log"><see cref="Log"/></param>
        public SeleniumFind(Driver driver, Log log)
        {
            Driver = driver;
            Log = log;
        }

        /// <summary>
        /// Find Element by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public Element ElementById(string id)
        {
            var elements = ElementsById(id);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: id, locatorType: LocatorStrategy.Id);
        }

        /// <summary>
        /// Find Elements by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public List<Element> ElementsById(string id) => Elements(LocatorStrategy.Id, id);

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public Element ElementByClass(string className)
        {
            var elements = ElementsByClass(className);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: className, locatorType: LocatorStrategy.Class);
        }

        /// <summary>
        /// Find Elements by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByClass(string className) => Elements(LocatorStrategy.Class, className);

        /// <summary>
        /// Find Element by CSS selector.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element.</param>
        /// <returns></returns>
        public Element ElementByCssSelector(string cssSelector)
        {
            var elements = ElementsByCssSelector(cssSelector);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: cssSelector, locatorType: LocatorStrategy.Css);
        }

        /// <summary>
        /// Find all Elements by CSS selector.
        /// </summary>
        /// <param name="cssSelector">Locate element by CSS selector.</param>
        /// <returns></returns>
        public List<Element> ElementsByCssSelector(string cssSelector) => Elements(LocatorStrategy.Css, cssSelector);

        /// <summary>
        /// Find Element by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByText(string text)
        {
            var elements = ElementsByText(text);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: text, locatorType: LocatorStrategy.Text);
        }

        /// <summary>
        /// Find Elements by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByText(string text) => Elements(LocatorStrategy.Text, text);

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByPartialText(string partialText)
        {
            var elements = ElementsByPartialText(partialText);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: partialText, locatorType: LocatorStrategy.PartialText);
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByPartialText(string partialText) => Elements(LocatorStrategy.PartialText, partialText);

        /// <summary>
        /// Locate elements by locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will elements be found?</param>
        /// <param name="locator">String to locate elements based on the provided locationStrategy.</param>
        /// <returns></returns>
        public List<Element> Elements(LocatorStrategy locatorStrategy, string locator)
        {
            var elementsList = new List<Element>();

            try
            {
                var elements = Driver.WebDriver.FindElements(Element(locatorStrategy, locator));

                switch (locatorStrategy)
                {
                    case LocatorStrategy.Class:
                    case LocatorStrategy.Css:
                    case LocatorStrategy.Id:
                    {
                        foreach (var element in elements)
                        {
                            elementsList.Add(new Element(element, Log, locator, locatorStrategy));
                        }

                        break;
                    }
                    case LocatorStrategy.Text:
                    case LocatorStrategy.PartialText:
                    {
                        var containsList = new List<Element>();
                        elements = Driver.WebDriver.FindElements(Element(LocatorStrategy.Css, "*"));

                        foreach (var element in elements)
                        {
                            if (element.Text.Equals(locator))
                            {
                                elementsList.Add(new Element(element, Log, locator, locatorStrategy));
                            }
                            else if (element.Text.Contains(locator))
                            {
                                containsList.Add(new Element(element, Log, locator, locatorStrategy));
                            }
                        }

                        if (locatorStrategy == LocatorStrategy.PartialText)
                        {
                            elementsList = containsList;
                        }

                        break;
                    }
                        
                }
            }
            catch (Exception e)
            {
                Driver.Log.Error(e.Message);
            }

            return elementsList;
        }

        /// <summary>
        /// Get By object based on the locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will the element be located?</param>
        /// <param name="locator">String used to find the element.</param>
        /// <returns></returns>
        internal static By Element(LocatorStrategy locatorStrategy, string locator)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (locatorStrategy)
            {
                case LocatorStrategy.Class:
                {
                    return By.ClassName(locator);
                }
                case LocatorStrategy.Css:
                {
                    return By.CssSelector(locator);
                }
                case LocatorStrategy.Id:
                {
                    return By.Id(locator);
                }
                case LocatorStrategy.PartialText:
                {
                    return By.PartialLinkText(locator);
                }
                case LocatorStrategy.Text:
                {
                    return By.LinkText(locator);
                }
                default:
                {
                    return null;
                }
            }
        }

        private Driver Driver { get; }
        private Log Log { get; }
    }
}
