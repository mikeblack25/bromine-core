using System.Collections.Generic;
using System.Linq;

using Bromine.Core;

using OpenQA.Selenium;

namespace Bromine.Element
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
        public IElement ElementById(string id)
        {
            if (!string.IsNullOrWhiteSpace(id)) { id = id[0] == '.' ? id : $".{id}"; }
            var elements = ElementsById(id);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: id, locatorType: Strategy.Id);
        }

        /// <summary>
        /// Find Elements by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public List<IElement> ElementsById(string id) => ToList(Driver.FindElements(Element(Strategy.Id, id)), id, Strategy.Id);

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public IElement ElementByClass(string className)
        {
            if (!string.IsNullOrWhiteSpace(className)) { className = className[0] == '.' ? className : $".{className}"; }
            var elements = ElementsByClass(className);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: className, locatorType: Strategy.Class);
        }

        /// <summary>
        /// Find Elements by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate elements.</param>
        /// <returns></returns>
        public List<IElement> ElementsByClass(string className) => ToList(Driver.FindElements(Element(Strategy.Class, className)), className, Strategy.Class);

        /// <summary>
        /// Find Element by CSS selector.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element.</param>
        /// <returns></returns>
        public IElement ElementByCssSelector(string cssSelector)
        {
            var elements = ElementsByCssSelector(cssSelector);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: cssSelector, locatorType: Strategy.Css);
        }

        /// <summary>
        /// Find all Elements by CSS selector.
        /// </summary>
        /// <param name="cssSelector">Locate element by CSS selector.</param>
        /// <returns></returns>
        public List<IElement> ElementsByCssSelector(string cssSelector)
        {
            var elements = new List<IElement>();

            try
            {
                elements = ToList(Driver.FindElements(Element(Strategy.Css, cssSelector)), cssSelector, Strategy.Css);
            }
            catch
            {
                Log.Framework($"Unable to locate element by {Strategy.Css} '{cssSelector}'");
            }

            return elements;
        }

        /// <summary>
        /// Find Element by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public IElement ElementByText(string text)
        {
            var elements = ElementsByText(text);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: text, locatorType: Strategy.Text);
        }

        /// <summary>
        /// Find Elements by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public List<IElement> ElementsByText(string text)
        {
            var list = new List<IElement>();

            try
            {
                list = ToList(Driver.FindElements(Element(Strategy.Text, text)), text, Strategy.Text);

                if (list.Count == 0)
                {
                     var tempList = ToList(Driver.FindElements(Element(Strategy.Css, "*")), text, Strategy.Text);

                    foreach (var element in tempList)
                    {
                        if (element.Text.Equals(text))
                        {
                            list.Add(new Element(element.SeleniumElement, Log, text, Strategy.Text));
                        }
                    }
                }
            }
            catch
            {
                Log.Framework($"Unable to locate element by {Strategy.Text} '{text}'");
            }

            return list;
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public IElement ElementByPartialText(string partialText)
        {
            var elements = ElementsByPartialText(partialText);

            return elements.Count > 0 ? elements.First() : new Element(null, log: Log, locatorString: partialText, locatorType: Strategy.PartialText);
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public List<IElement> ElementsByPartialText(string partialText)
        {
            var list = new List<IElement>();

            try
            {
                list = ToList(Driver.FindElements(Element(Strategy.PartialText, partialText)), partialText, Strategy.PartialText);

                if (list.Count == 0)
                {
                    var tempList = ToList(Driver.FindElements(Element(Strategy.Css, "*")), partialText, Strategy.PartialText);

                    foreach (var element in tempList)
                    {
                        if (element.Text.Contains(partialText))
                        {
                            list.Add(new Element(element.SeleniumElement, Log, partialText, Strategy.PartialText));
                        }
                    }
                }
            }
            catch
            {
                Log.Framework($"Unable to locate element by {Strategy.PartialText} '{partialText}'");
            }

            return list;
        }

        /// <summary>
        /// Get By object based on the strategy and locator string.
        /// Strategy.Text and Strategy.Partial text are not available here because the current implementation is using Strategy.Css.
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
                case Strategy.Text:
                {
                    return By.LinkText(locator);
                }
                case Strategy.PartialText:
                {
                    return By.PartialLinkText(locator);
                }
                default:
                {
                    return null;
                }
            }
        }

        private List<IElement> ToList(IReadOnlyCollection<IWebElement> elements, string locator, Strategy strategy)
        {
            var list = new List<IElement>();

            foreach (var element in elements)
            {
                list.Add(new Element(element, Log, locator, strategy));
            }

            return list;
        }

        internal Browser Browser { get; }
        internal Log Log => Browser.Log;

        private IWebDriver Driver => Browser.Driver.WebDriver;
    }


    /// <summary>
    /// Supported approaches to locate an element.
    /// </summary>
    public enum Strategy
    {
#pragma warning disable 1591
        Undefined = 0,
        Id,
        Class,
        Css,
        Text,
        PartialText,
#pragma warning restore 1591
    }
}
