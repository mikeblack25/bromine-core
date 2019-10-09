using System;
using System.Linq;

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
            Browser = browser;
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
                Browser.LogManager.Error(e.Message);
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
                Driver.ExecuteJavaScript(ElementBorderScript(LocatorStrategyFromCss(element.Information.LocatorStrategy, element.Information.LocatorString), RemoveLocatorPrefix(element.Information.LocatorString), color));
            }
            catch (Exception e)
            {
                Browser.LogManager.Error(e.Message);
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
                Browser.LogManager.Error(e.Message);
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
                Driver.ExecuteJavaScript(ElementsBorderScript(LocatorStrategyFromCss(element.Information.LocatorStrategy, element.Information.LocatorString), RemoveLocatorPrefix(element.Information.LocatorString), color));
            }
            catch (Exception e)
            {
                Browser.LogManager.Error(e.Message);
            }
        }

        /// <summary>
        /// Get the style attribute for the given element.
        /// </summary>
        /// <param name="element">Element to get style attribute of.</param>
        /// <returns></returns>
        public string GetStyleAttribute(Element element) => element.GetAttribute("style");

        private LocatorStrategy LocatorStrategyFromCss(LocatorStrategy locatorStrategy, string locator)
        {
            var firstChar = locator.First();

            if (locatorStrategy == LocatorStrategy.Css)
            {
                if (firstChar == '#') { return LocatorStrategy.Id; }

                if (firstChar == '.') { return LocatorStrategy.Class; }
            }

            return locatorStrategy;
        }

        private string RemoveLocatorPrefix(string locator)
        {
            var firstChar = locator.First();
            var locatorWithoutPrefix = locator.Substring(1);

            switch (firstChar)
            {
                case '#':
                case '.':
                {
                    return locatorWithoutPrefix;
                }
            }

            return locator;
        }

        private string GetLocatorStrategyForJsScript(LocatorStrategy locatorStrategy)
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
                    Browser.LogManager.Error($"{locatorStrategy} is not a valid location strategy here");

                    break;
                }
            }

            return locatorString;
        }

        private string ElementBorderScript(LocatorStrategy locatorStrategy, string locator, string color) => $"document.{GetLocatorStrategyForJsScript(locatorStrategy)}(\"{locator}\").style.borderColor = \"{color}\"";
        private string ElementsBorderScript(LocatorStrategy locatorStrategy, string locator, string color) => "{\n  var x = document." + $"{GetLocatorStrategyForJsScript(locatorStrategy)}" + "(\"" + $"{locator}\");\n  var i;\n  for (i = 0; i < x.length; i++) " + "{\n    " + "x[i].style.borderColor = \"" + $"{color}" + "\";\n  }\n}";

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
    }
}
