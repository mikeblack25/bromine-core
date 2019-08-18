﻿using System.Collections.Generic;

using OpenQA.Selenium;

namespace Bromine.Core
{
    /// <summary>
    /// Find elements on the current page / screen.
    /// </summary>
    public class Find
    {
        /// <summary>
        /// Construct a Find object to locate elements.
        /// </summary>
        /// <param name="driver">Driver used to navigate.</param>
        public Find(Driver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Find Element by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public Element ElementById(string id) => ElementsById(id)[0];

        /// <summary>
        /// Find Elements by ID.
        /// </summary>
        /// <param name="id">ID to locate an element.</param>
        /// <returns></returns>
        public List<Element> ElementsById(string id) => Elements(LocatorType.Id, id);

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public Element ElementByClass(string className) => ElementsByClass(className)[0];

        /// <summary>
        /// Find Elements by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByClass(string className) => Elements(LocatorType.Class, className);

        /// <summary>
        /// Find Element by CSS Selector.
        /// </summary>
        /// <param name="css">CSS Selector to locate an element.</param>
        /// <returns></returns>
        public Element ElementByCssSelector(string css) => ElementsByCssSelector(css)[0];

        /// <summary>
        /// Find all Elements by CSS Selector.
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public List<Element> ElementsByCssSelector(string css) => Elements(LocatorType.Css, css);

        /// <summary>
        /// Find Element by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByText(string text) => ElementsByText(text)[0];

        /// <summary>
        /// Find Elements by text.
        /// </summary>
        /// <param name="text">Element text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByText(string text) => Elements(LocatorType.Text, text);

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public Element ElementByPartialText(string partialText) => ElementsByPartialText(partialText)[0];

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByPartialText(string partialText) => Elements(LocatorType.PartialText, partialText);

        /// <summary>
        /// Find Element by tag name.
        /// </summary>
        /// <param name="tag">HTML tag of the element to find.</param>
        /// <returns></returns>
        public Element ElementByTag(string tag) => ElementsByTag(tag)[0];

        /// <summary>
        /// Find Elements by tag name.
        /// </summary>
        /// <param name="tag">HTML tag of the element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByTag(string tag) => Elements(LocatorType.Tag, tag);

        /// <summary>
        /// Locate elements by locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will elements be found?</param>
        /// <param name="locator">String to locate elements.</param>
        /// <returns></returns>
        public List<Element> Elements(LocatorType locatorStrategy, string locator)
        {
            var elementsList = new List<Element>();
            var elements = _driver.WebDriver.FindElements(Element(locatorStrategy, locator));

            foreach (var element in elements)
            {
                elementsList.Add(new Element(element, locator, locatorStrategy));
            }

            return elementsList;
        }

        /// <summary>
        /// Get By object based on the locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will the element be located?</param>
        /// <param name="locator">String used to find the element.</param>
        /// <returns></returns>
        internal static By Element(LocatorType locatorStrategy, string locator)
        {
            By byObj = null;

            switch (locatorStrategy)
            {
                case LocatorType.Class:
                {
                    byObj = By.ClassName(locator);

                    break;
                }
                case LocatorType.Css:
                {
                    byObj = By.CssSelector(locator);

                    break;
                }
                case LocatorType.Id:
                {
                    byObj = By.Id(locator);

                    break;
                }
                case LocatorType.PartialText:
                {
                    byObj = By.PartialLinkText(locator);
                    break;
                }
                case LocatorType.Tag:
                {
                    byObj = By.TagName(locator);

                    break;
                }
                case LocatorType.Text:
                {
                    byObj = By.LinkText(locator);

                    break;
                }
                case LocatorType.Js:
                case LocatorType.XPath:
                {
                    break;
                }
            }

            return byObj;
        }

        private readonly Driver _driver;
    }
}
