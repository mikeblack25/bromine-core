using System.Collections.Generic;

using OpenQA.Selenium;

namespace Bromine.Core
{
    /// <summary>
    /// Find elements on the current page / screen.
    /// </summary>
    public class Find
    {
        public Find(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Find Element by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public Element ElementById(string id)
        {
            return new Element(_driver.FindElement(By.Id(id)), id, LocatorType.Id);
        }

        /// <summary>
        /// Find Elements by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public List<Element> ElementsById(string id)
        {
            var list = new List<Element>();

            var elements = _driver.FindElements(By.Id(id));

            foreach (var element in elements)
            {
                list.Add(new Element(element, id, LocatorType.Id));
            }

            return list;
        }

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public Element ElementByClass(string className)
        {
            return new Element(_driver.FindElement(By.ClassName(className)), className, LocatorType.Class);
        }

        /// <summary>
        /// Find Elements by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByClass(string className)
        {
            var list = new List<Element>();
            var elements = _driver.FindElements(By.ClassName(className));

            foreach (var element in elements)
            {
                list.Add(new Element(element, className, LocatorType.Class));
            }

            return list;
        }

        /// <summary>
        /// Find Element by CSS Selector.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element.</param>
        /// <returns></returns>
        public Element ElementByCssSelector(string cssSelector)
        {
            return new Element(_driver.FindElement(By.CssSelector(cssSelector)), cssSelector, LocatorType.CssSelector);
        }

        /// <summary>
        /// Find all Elements by CSS Selector.
        /// </summary>
        /// <param name="cssSelector"></param>
        /// <returns></returns>
        public List<Element> ElementsByCssSelector(string cssSelector)
        {
            var list = new List<Element>();
            var elements = _driver.FindElements(By.CssSelector(cssSelector));

            foreach (var element in elements)
            {
                list.Add(new Element(element, cssSelector, LocatorType.CssSelector));
            }

            return list;
        }

        /// <summary>
        /// Find Element by text.
        /// </summary>
        /// <param name="elementText">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByText(string elementText)
        {
            return new Element(_driver.FindElement(By.LinkText(elementText)), elementText, LocatorType.Text);
        }

        /// <summary>
        /// Find Elements by text.
        /// </summary>
        /// <param name="elementText">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByText(string elementText)
        {
            var list = new List<Element>();

            var elements = _driver.FindElements(By.LinkText(elementText));

            foreach (var element in elements)
            {
                list.Add(new Element(element, elementText, LocatorType.Text));
            }

            return list;
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialElementText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByPartialText(string partialElementText)
        {
            return new Element(_driver.FindElement(By.PartialLinkText(partialElementText)), partialElementText, LocatorType.PartialText);
        }

        /// <summary>
        /// Find Elements by tag name.
        /// </summary>
        /// <param name="tag">HTML tag of the element to find.</param>
        /// <returns></returns>
        public List<Element> ElementByTag(string tag)
        {
            var elementsList = new List<Element>();
            var elements = _driver.FindElements(By.TagName(tag));

            foreach (var element in elements)
            {
                elementsList.Add(new Element(element, tag, LocatorType.Tag));
            }

            return elementsList;
        }

        private IWebDriver _driver;
    }
}
