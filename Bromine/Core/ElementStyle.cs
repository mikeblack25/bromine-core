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
                var script = $"document.{GetLocatorStrategy(locatorStrategy)}(\"{locator}\").style.borderColor = \"{color}\"";

                Driver.ExecuteJavaScript(script);
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
                var script = "{\n  var x = document." + $"{GetLocatorStrategy(locatorStrategy)}" + "(\"" + $"{locator}\");\n  var i;\n  for (i = 0; i < x.length; i++) " + "{\n    " + "x[i].style.borderColor = \"" + $"{color}" + "\";\n  }\n}";

                Driver.ExecuteJavaScript(script);
            }
            catch (Exception e)
            {
                _browser.Exceptions.Add(e);
            }
        }

        /// <summary>
        /// Get the style attribute for the given element.
        /// </summary>
        /// <param name="element"></param>
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

        private readonly Browser _browser;
        private IWebDriver Driver => _browser.Driver.WebDriver;
    }
}
