using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Bromine.Core;

using OpenQA.Selenium;

namespace Bromine.Element
{
    /// <summary>
    /// Provides ability to interact with elements.
    /// </summary>
    public class Element : IElement
    {
        /// <summary>
        /// Create an Element which can interact with web applications.
        /// </summary>
        /// <param name="element">Requested element.</param>
        /// <param name="browser"><see cref="IBrowser"/></param>
        /// <param name="by"><see cref="By"/></param>
        /// <param name="locator">Locator string used to find the requested element.</param>
        /// <param name="strategy">Type of locator used to find the requested element.</param>
        internal Element(IWebElement element, IBrowser browser, By by = null, string locator = "", Strategy strategy = Strategy.Undefined) : this()
        {
            SeleniumElement = element;
            Browser = browser;
            SeleniumBy = by;

            if (!string.IsNullOrEmpty(locator) && strategy != 0)
            {
                Information.LocatorString = locator;
                Information.Strategy = strategy;
            }

            if (element != null)
            {
                Information.IsInitialized = true;
            }
        }

        /// <summary>
        /// Details about the location strategy used for the requested element.
        /// </summary>
        public Information Information { get; }

        /// <inheritdoc />
        public Log Log => Browser.Log;

        /// <summary>
        /// Selenium IWebElement.
        /// </summary>
        public IWebElement SeleniumElement { get; }

        /// <summary>
        /// 
        /// </summary>
        public By SeleniumBy { get; internal set; }

        /// <summary>
        /// Element TagName value.
        /// </summary>
        public string TagName => GetProperty(() => SeleniumElement.TagName, "Unable to find the tag for the requested element").ToString();

        /// <summary>
        /// Element Text value.
        /// </summary>
        public string Text => GetProperty(() => SeleniumElement.Text, "Unable to find the text for the requested element").ToString();

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        public bool Enabled => (bool)GetProperty(() => SeleniumElement.Enabled, "Unable to find the enabled property for the requested element");

        /// <summary>
        /// Element selected status.
        /// </summary>
        public bool Selected => (bool)GetProperty(() => SeleniumElement.Selected, "Unable to find the selected property for the requested element");

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        public Point Location => (Point)GetProperty(() => SeleniumElement.Location, "Unable to find the location for the requested element");

        /// <summary>
        /// Element size.
        /// </summary>
        public Size Size => (Size)GetProperty(() => SeleniumElement.Size, "Unable to find the size for the requested element");

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        public bool Displayed
        {
            get
            {
                if (SeleniumElement != null)
                {
                    return (bool)GetProperty(() => SeleniumElement.Displayed, "Unable to find the displayed property for the requested element");
                }

                return false;
            }
        }

        /// <inheritdoc />
        public List<IElement> FindElements(Strategy strategy, string locator)
        {
            Element ele = this as Element;

            return ToList(ele.SeleniumElement.FindElements(SeleniumFind.Element(strategy, locator)), ele.Browser, locator, strategy);
        }

        /// <inheritdoc />
        public List<IElement> FindElements(string locator)
        {
            Element ele = this as Element;

            return ele.FindElements(Strategy.Css, locator);
        }

        /// <inheritdoc />
        public IElement FindElement(string locator) => FindElements(Strategy.Css, locator).FirstOrDefault();

        /// <inheritdoc />
        public string GetAttribute(string attributeName) => (string)GetProperty(() => SeleniumElement.GetAttribute(attributeName), $"Unable to find the value {attributeName} for the requested element");

        /// <inheritdoc />
        public string GetCss(string cssValue) => (string)GetProperty(() => SeleniumElement.GetCssValue(cssValue), $"Unable to find the CSS value {cssValue} for the requested element");

        /// <inheritdoc />
        public string GetJavaScriptProperty(string propertyName) => (string)GetProperty(() => SeleniumElement.GetProperty(propertyName), $"Unable to find the property {propertyName} for the requested element");

        /// <inheritdoc />
        public void SetAttribute(string attributeName, string attributeValue)
        {
            Browser.ExecuteJs($"arguments[0].setAttribute({attributeName}, {attributeValue});", this);
        }

        /// <inheritdoc />
        public void SendKeys(string text)
        {
            LogElementInformation($"Send Keys {text} to");

            SeleniumElement?.SendKeys(text);
        }

        /// <inheritdoc />
        public void Clear()
        {
            LogElementInformation("Clear");

            SeleniumElement?.Clear();
        }

        /// <inheritdoc />
        public void Click()
        {
            LogElementInformation("Click");

            SeleniumElement?.Click();
        }

        internal IBrowser Browser { get; }

        /// <summary>
        /// Convert an IReadOnlyCollection of IWebElements to a List of IElements.
        /// </summary>
        /// <param name="elements">IWebElement collection to wrap.</param>
        /// <param name="browser">Browser object for logging.</param>
        /// <param name="locator">Locator string used to find the elements.</param>
        /// <param name="strategy">Location strategy used to find the elements.</param>
        /// <returns></returns>
        internal static List<IElement> ToList(IReadOnlyCollection<IWebElement> elements, IBrowser browser, string locator = "",  Strategy strategy = Strategy.Undefined)
        {
            var elementList = new List<IElement>();

            foreach (var element in elements)
            {
                elementList.Add(new Element(element, browser: browser, locator: locator, strategy: strategy));
            }

            return elementList;
        }

        /// <summary>
        /// Construct the default behavior of the Element object.
        /// </summary>
        internal Element()
        {
            Information = new Information
            {
                Created = DateTime.Now,
                IsInitialized = false
            };
        }

        /// <summary>
        /// Log a standard message for element interaction in the form:
        /// {info} {Information.Name} element
        /// NOTE: Elements named 'Element' will be ignored from logging as this is the base element name on construction.
        /// </summary>
        /// <param name="info">Element interaction information to log.</param>
        private void LogElementInformation(string info)
        {
            if (Information.Name != "Element")
            {
                Log.Message($"{info} {Information.Name} element");
            }
        }

        private object GetProperty(Func<object> method, string errorMessage)
        {
            try
            {
                return method.Invoke();
            }
            catch (Exception e)
            {
                Log.Error($"{errorMessage}{Environment.NewLine}{e.Message}");

                return null;
            }
        }
    }
}
