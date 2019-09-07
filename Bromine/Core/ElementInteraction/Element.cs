using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using Bromine.Core.ElementLocator;

using OpenQA.Selenium;

// ReSharper disable once CheckNamespace
namespace Bromine.Core.ElementInteraction
{
    /// <summary>
    /// Provides ability to interact with elements.
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Create an Element which can interact with web applications.
        /// </summary>
        /// <param name="element">Requested element.</param>
        /// <param name="locatorString">Locator string used to find the requested element.</param>
        /// <param name="locatorType">Type of locator used to find the requested element.</param>
        internal Element(IWebElement element, string locatorString = "", LocatorStrategy locatorType = 0) : this()
        {
            WebElement = element;

            if (!string.IsNullOrEmpty(locatorString) && locatorType != 0)
            {
                Information.LocatorString = locatorString;
                Information.LocatorStrategy = locatorType;
            }

            IsInitialized = true;
        }

        /// <summary>
        /// Flag to determine if the element has been created correctly.
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// Element TagName value.
        /// </summary>
        public string TagName => WebElement?.TagName;

        /// <summary>
        /// Element Text value.
        /// </summary>
        public string Text => WebElement?.Text;

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        public bool Enabled => WebElement.Enabled;

        /// <summary>
        /// Element selected status.
        /// </summary>
        public bool Selected => WebElement.Selected;

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        public Point Location => WebElement.Location;

        /// <summary>
        /// Element size.
        /// </summary>
        public Size Size => WebElement.Size;

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        public bool Displayed => WebElement.Displayed;

        /// <summary>
        /// Details about the location strategy used for the requested element.
        /// </summary>
        public CallingInformation Information { get; }

        /// <summary>
        /// Clear the element content. This is usually used on a user editable field element.
        /// </summary>
        public void Clear()
        {
            if (!IsInitialized) { return; }

            try
            {
                WebElement.Clear();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Click the element. The element should be enabled to be clickable.
        /// </summary>
        public void Click()
        {
            if (!IsInitialized) { return; }

            try
            {
                WebElement.Click();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Find the parent element of the requested element.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <returns></returns>
        public Element GetParent()
        {
            if (IsInitialized)
            {
                try
                {
                    return new Element(WebElement.FindElement(By.XPath("..")), "..", LocatorStrategy.XPath);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }

            return new Element();
        }

        /// <summary>
        /// Find the requested element with the given attribute.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="attributeName">Attribute name of the requested element.</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            var attribute = string.Empty;

            if (IsInitialized)
            {
                try
                {
                    attribute = WebElement.GetAttribute(attributeName);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }

            return attribute;
        }

        /// <summary>
        /// Get the CSS value for the requested element by property name.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">CSS value for the requested element.</param>
        /// <returns></returns>
        public string GetCssValue(string propertyName)
        {
            var cssValue = string.Empty;

            if (IsInitialized)
            {
                try
                {
                    cssValue = WebElement.GetCssValue(propertyName);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }

            return cssValue;
        }

        /// <summary>
        /// Get the value for the requested property.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">Property value for the requested element.</param>
        /// <returns></returns>
        public string GetProperty(string propertyName)
        {
            if (IsInitialized)
            {
                try
                {
                    return WebElement.GetProperty(propertyName);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }

            return propertyName;
        }

        /// <summary>
        /// Update the value property for the requested element.
        /// </summary>
        /// <param name="text">Text to update to the requested element.</param>
        public void SendKeys(string text)
        {
            if (IsInitialized)
            {
                try
                {
                    WebElement.SendKeys(text);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }
        }

        /// <summary>
        /// Update information about the element. This is similar to <see cref="Click"/>, but can be used on any form element not just buttons.
        /// </summary>
        public void Submit()
        {
            WebElement.Submit();
        }

        /// <summary>
        /// Find an element by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find a requested element.</param>
        /// <returns></returns>
        internal Element FindElement(By by) => new Element(WebElement.FindElement(by));

        /// <summary>
        /// Find elements by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find requested elements.</param>
        /// <returns></returns>
        internal List<Element> FindElements(By by)
        {
            var list = new List<Element>();

            var elements = WebElement.FindElements(by);

            foreach (var element in elements)
            {
                list.Add(new Element(element));
            }

            return list;
        }

        /// <summary>
        /// Construct the default behavior of the Element object.
        /// </summary>
        internal Element()
        {
            Exceptions = new List<Exception>();
            var stackTrace = new StackTrace();

            Information = new CallingInformation
            {
                CallingMethod = stackTrace.GetFrame(2).GetMethod().Name,
                CalledTimestamp = DateTime.Now
            };

            IsInitialized = false;
        }

        internal readonly IWebElement WebElement;
        // ReSharper disable once CollectionNeverQueried.Local
        private List<Exception> Exceptions { get; }
    }
}
