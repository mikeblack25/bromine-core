using System;

using Bromine.Core.ElementLocator;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Bromine.Core.ElementInteraction
{
    /// <summary>
    /// Add style to WebElements.
    /// </summary>
    public class ElementStyle
    {
        /// <summary>
        /// Add style to WebElements.
        /// </summary>
        public ElementStyle(Browser browser)
        {
            _browser = browser;
        }

        /// <summary>
        /// Add a border to an element located by the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will the element be found?</param>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorder(LocatorStrategy locatorStrategy, string locator, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementBorderScript(locatorStrategy, locator, color));
            }
            catch (Exception e)
            {
                _browser.Exceptions.Add(e);
            }
        }

        /// <summary>
        /// Add a border to an element located by the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="element">Element to add a border around.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorder(Element element, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementBorderScript(element.Information.LocatorStrategy, element.Information.LocatorString, color));
            }
            catch (Exception e)
            {
                _browser.Exceptions.Add(e);
            }
        }

        /// <summary>
        /// Add Borders to elements located by the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="locatorStrategy">How will the element be found?</param>
        /// <param name="locator">String used to locate the element.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorders(LocatorStrategy locatorStrategy, string locator, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementsBorderScript(locatorStrategy, locator, color));
            }
            catch (Exception e)
            {
                _browser.Exceptions.Add(e);
            }
        }

        /// <summary>
        /// Add Borders to elements located by the given locatorStrategy and locator string.
        /// </summary>
        /// <param name="element">Element to add a border around.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorders(Element element, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementsBorderScript(element.Information.LocatorStrategy, element.Information.LocatorString, color));
            }
            catch (Exception e)
            {
                _browser.Exceptions.Add(e);
            }
        }

        /// <summary>
        /// Get the style attribute for the given element.
        /// </summary>
        /// <param name="element">Element to get style attribute of.</param>
        /// <returns></returns>
        public string GetStyleAttribute(Element element) => element.GetAttribute("style");

        private string GetLocatorStrategy(LocatorStrategy locatorStrategy)
        {
            var locatorString = string.Empty;

            switch (locatorStrategy)
            {
                case LocatorStrategy.Class:
                {
                    locatorString = "getElementsByClassName";

                    break;
                }
                case LocatorStrategy.Id:
                {
                    locatorString = "getElementById";

                    break;
                }
                case LocatorStrategy.Css:
                case LocatorStrategy.Js:
                case LocatorStrategy.PartialText:
                case LocatorStrategy.Tag:
                case LocatorStrategy.Text:
                case LocatorStrategy.XPath:
                {
                    _browser.Exceptions.Add(new Exception($"{locatorStrategy} is not a valid location strategy here"));

                    break;
                }
            }

            return locatorString;
        }

        private string ElementBorderScript(LocatorStrategy locatorStrategy, string locator, string color) => $"document.{GetLocatorStrategy(locatorStrategy)}(\"{locator}\").style.borderColor = \"{color}\"";
        private string ElementsBorderScript(LocatorStrategy locatorStrategy, string locator, string color) => "{\n  var x = document." + $"{GetLocatorStrategy(locatorStrategy)}" + "(\"" + $"{locator}\");\n  var i;\n  for (i = 0; i < x.length; i++) " + "{\n    " + "x[i].style.borderColor = \"" + $"{color}" + "\";\n  }\n}";

        private readonly Browser _browser;
        private IWebDriver Driver => _browser.Driver.WebDriver;
    }
}
