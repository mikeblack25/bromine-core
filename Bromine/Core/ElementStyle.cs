using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Bromine.Core
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
        public void AddBorder(LocatorType locatorStrategy, string locator, string color)
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
                Driver.ExecuteJavaScript(ElementBorderScript(element.Information.LocatorType, element.Information.LocatorString, color));
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
        public void AddBorders(LocatorType locatorStrategy, string locator, string color)
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
                Driver.ExecuteJavaScript(ElementsBorderScript(element.Information.LocatorType, element.Information.LocatorString, color));
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

        private string GetLocatorStrategy(LocatorType locatorStrategy)
        {
            var locatorString = string.Empty;

            switch (locatorStrategy)
            {
                case LocatorType.Class:
                {
                    locatorString = "getElementsByClassName";

                    break;
                }
                case LocatorType.Id:
                {
                    locatorString = "getElementById";

                    break;
                }
                case LocatorType.Css:
                case LocatorType.Js:
                case LocatorType.PartialText:
                case LocatorType.Tag:
                case LocatorType.Text:
                case LocatorType.XPath:
                {
                    _browser.Exceptions.Add(new Exception($"{locatorStrategy} is not a valid location strategy here"));

                    break;
                }
            }

            return locatorString;
        }

        private string ElementBorderScript(LocatorType locatorStrategy, string locator, string color) => $"document.{GetLocatorStrategy(locatorStrategy)}(\"{locator}\").style.borderColor = \"{color}\"";
        private string ElementsBorderScript(LocatorType locatorStrategy, string locator, string color) => "{\n  var x = document." + $"{GetLocatorStrategy(locatorStrategy)}" + "(\"" + $"{locator}\");\n  var i;\n  for (i = 0; i < x.length; i++) " + "{\n    " + "x[i].style.borderColor = \"" + $"{color}" + "\";\n  }\n}";

        private readonly Browser _browser;
        private IWebDriver Driver => _browser.Driver.WebDriver;
    }
}
