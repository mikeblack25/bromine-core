﻿using System;
using System.Collections.Generic;

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
        /// <param name="logManager"><see cref="Logger.LogManager"/></param>
        public SeleniumFind(Driver driver, LogManager logManager)
        {
            Driver = driver;
            LogManager = logManager;
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
        public List<Element> ElementsById(string id) => Elements(LocatorStrategy.Id, id);

        /// <summary>
        /// Find Element by Class identifier.
        /// </summary>
        /// <param name="className">Class name to locate an element.</param>
        /// <returns></returns>
        public Element ElementByClass(string className)
        {
            try
            {
                return ElementsByClass(className)[0];
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);

                return new Element();
            }
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
            try
            {
                return ElementsByCssSelector(cssSelector)[0];
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);

                return new Element();
            }
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
            try
            {
                return ElementsByText(text)[0];
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);

                return new Element();
            }
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
            try
            {
                return ElementsByPartialText(partialText)[0];
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);

                return new Element();
            }
        }

        /// <summary>
        /// Find Element by partial text.
        /// </summary>
        /// <param name="partialText">Partial text of the HTML element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByPartialText(string partialText) => Elements(LocatorStrategy.PartialText, partialText);

        /// <summary>
        /// Find Element by tag name.
        /// </summary>
        /// <param name="tag">HTML tag of the element to find.</param>
        /// <returns></returns>
        public Element ElementByTag(string tag)
        {
            try
            {
                return ElementsByTag(tag)[0];
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);

                return new Element();
            }
        }

        /// <summary>
        /// Find Elements by tag name.
        /// </summary>
        /// <param name="tag">HTML tag of the element to find.</param>
        /// <returns></returns>
        public List<Element> ElementsByTag(string tag) => Elements(LocatorStrategy.Tag, tag);

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

                foreach (var element in elements)
                {
                    elementsList.Add(new Element(element, LogManager, locator, locatorStrategy));
                }
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
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
            By byObj = null;

            switch (locatorStrategy)
            {
                case LocatorStrategy.Class:
                    {
                        byObj = By.ClassName(locator);

                        break;
                    }
                case LocatorStrategy.Css:
                    {
                        byObj = By.CssSelector(locator);

                        break;
                    }
                case LocatorStrategy.Id:
                    {
                        byObj = By.Id(locator);

                        break;
                    }
                case LocatorStrategy.PartialText:
                    {
                        byObj = By.PartialLinkText(locator);
                        break;
                    }
                case LocatorStrategy.Tag:
                    {
                        byObj = By.TagName(locator);

                        break;
                    }
                case LocatorStrategy.Text:
                    {
                        byObj = By.LinkText(locator);

                        break;
                    }
                case LocatorStrategy.Js:
                case LocatorStrategy.XPath:
                    {
                        break;
                    }
            }

            return byObj;
        }

        private Driver Driver { get; }
        private LogManager LogManager { get; }
    }
}
