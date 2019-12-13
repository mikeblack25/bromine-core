using System;
using System.Linq;

using Bromine.Core;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Bromine.Element
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
        /// Add a border to an element located by the given strategy and locator string.
        /// </summary>
        /// <param name="element">Element to add a border around.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorder(IElement element, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementBorderScript(LocatorStrategyFromCss(element.Information.Strategy, element.Information.LocatorString), RemoveLocatorPrefix(element.Information.LocatorString), color));
            }
            catch (Exception e)
            {
                Browser.Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Add Borders to elements located by the given strategy and locator string.
        /// </summary>
        /// <param name="element">Element to add a border around.</param>
        /// <param name="color">Element border color.</param>
        public void AddBorders(IElement element, string color)
        {
            try
            {
                Driver.ExecuteJavaScript(ElementsBorderScript(LocatorStrategyFromCss(element.Information.Strategy, element.Information.LocatorString), RemoveLocatorPrefix(element.Information.LocatorString), color));
            }
            catch (Exception e)
            {
                Browser.Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Get the style attribute for the given element.
        /// </summary>
        /// <param name="element">Element to get style attribute of.</param>
        /// <returns></returns>
        public string GetStyleAttribute(IElement element) => element.GetAttribute("style");

        private Strategy LocatorStrategyFromCss(Strategy locatorStrategy, string locator)
        {
            var firstChar = locator.First();

            if (locatorStrategy == Strategy.Css)
            {
                if (firstChar == '#') { return Strategy.Id; }

                if (firstChar == '.') { return Strategy.Class; }
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

        private string GetLocatorStrategyForJsScript(Strategy locatorStrategy)
        {
            var locatorString = string.Empty;

            switch (locatorStrategy)
            {
                case Strategy.Class:
                {
                    locatorString = "getElementsByClassName";

                    break;
                }
                case Strategy.Id:
                {
                    locatorString = "getElementById";

                    break;
                }
                case Strategy.Css:
                case Strategy.PartialText:
                case Strategy.Text:
                {
                    Browser.Log.Error($"{locatorStrategy} is not a valid location strategy here");

                    break;
                }
            }

            return locatorString;
        }

        private string ElementBorderScript(Strategy locatorStrategy, string locator, string color) => $"document.{GetLocatorStrategyForJsScript(locatorStrategy)}(\"{locator}\").style.borderColor = \"{color}\"";
        private string ElementsBorderScript(Strategy locatorStrategy, string locator, string color) => "{\n  var x = document." + $"{GetLocatorStrategyForJsScript(locatorStrategy)}" + "(\"" + $"{locator}\");\n  var i;\n  for (i = 0; i < x.length; i++) " + "{\n    " + "x[i].style.borderColor = \"" + $"{color}" + "\";\n  }\n}";

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
    }
}
