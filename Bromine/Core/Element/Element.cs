using System;
using System.Collections.Generic;
using System.Drawing;

using OpenQA.Selenium;

namespace Bromine.Core.Element
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
        /// <param name="log"><see cref="Log"/></param>
        /// <param name="locatorString">Locator string used to find the requested element.</param>
        /// <param name="locatorType">Type of locator used to find the requested element.</param>
        internal Element(IWebElement element, Log log, string locatorString = "", Strategy locatorType = Strategy.Undefined) : this()
        {
            WebElement = element;
            Log = log;

            if (!string.IsNullOrEmpty(locatorString) && locatorType != 0)
            {
                Information.LocatorString = locatorString;
                Information.Strategy = locatorType;
            }

            if (element != null)
            {
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Details about the location strategy used for the requested element.
        /// </summary>
        public Information Information { get; }

        /// <summary>
        /// Flag to determine if the element has been created correctly.
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// Element TagName value.
        /// </summary>
        public string TagName => GetProperty(() => WebElement.TagName, "Unable to find the tag for the requested element").ToString();

        /// <summary>
        /// Element Text value.
        /// </summary>
        public string Text => GetProperty(() => WebElement.Text, "Unable to find the text for the requested element").ToString();

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        public bool Enabled => (bool) GetProperty(() => WebElement.Enabled, "Unable to find the enabled property for the requested element");

        /// <summary>
        /// Element selected status.
        /// </summary>
        public bool Selected => (bool)GetProperty(() => WebElement.Selected, "Unable to find the selected property for the requested element");

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        public Point Location => (Point)GetProperty(() => WebElement.Location, "Unable to find the location for the requested element");

        /// <summary>
        /// Element size.
        /// </summary>
        public Size Size => (Size)GetProperty(() => WebElement.Size, "Unable to find the size for the requested element");

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        public bool Displayed => (bool)GetProperty(() => WebElement.Displayed, "Unable to find the displayed property for the requested element");

        /// <summary>
        /// Find the parent element of the requested element.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <returns></returns>
        public Element ParentElement => (Element)GetProperty(() => new Element(WebElement.FindElement(By.XPath("..")), Log, ".."), "Unable to find the parent element for the requested element");

        /// <summary>
        /// Find the requested element with the given attribute.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="attributeName">Attribute name of the requested element.</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName) => (string)GetProperty(() => WebElement.GetAttribute(attributeName), $"Unable to find the value {attributeName} for the requested element");

        /// <summary>
        /// Get the CSS value for the requested element by property name.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="cssValue">CSS value for the requested element.</param>
        /// <returns></returns>
        public string GetCssValue(string cssValue) => (string)GetProperty(() => WebElement.GetCssValue(cssValue), $"Unable to find the CSS value {cssValue} for the requested element");

        /// <summary>
        /// Get the JavaScript value for the requested property.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">Property value for the requested element.</param>
        /// <returns></returns>
        public string GetJavaScriptProperty(string propertyName) => (string)GetProperty(() => WebElement.GetProperty(propertyName), $"Unable to find the property {propertyName} for the requested element");

        /// <summary>
        /// Update the value property for the requested element.
        /// </summary>
        /// <param name="text">Text to update to the requested element.</param>
        public void SendKeys(string text)
        {
            if (WebElement != null)
            {
                WebElement.SendKeys(text);
            }
            else
            {
                Log.Error($"Unable to send keys {text} to the requested element");
            }
        }

        /// <summary>
        /// Clear the element content. This is usually used on a user editable field element.
        /// </summary>
        public void Clear()
        {
            if (WebElement != null)
            {
                WebElement.Clear();
            }
            else
            {
                Log.Error("Unable to clear the requested element");
            }
        }

        /// <summary>
        /// Click the element. The element should be enabled to be clickable.
        /// </summary>
        public void Click()
        {
            if (WebElement != null)
            {
                WebElement.Click();
            }
            else
            {
                Log.Error("Unable to click the requested element");
            }
        }

        /// <summary>
        /// Update information about the element. This is similar to <see cref="Click"/>, but can be used on any form element not just buttons.
        /// </summary>
        public void Submit()
        {
            if (WebElement != null)
            {
                WebElement.Submit();
            }
            else
            {
                Log.Error("Unable to submit to the requested element");
            }
        }

        /// <summary>
        /// Find an element by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find a requested element.</param>
        /// <returns></returns>
        internal Element FindElement(By by) => new Element(WebElement.FindElement(by), Log);

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
                list.Add(new Element(element, Log));
            }

            return list;
        }

        /// <summary>
        /// Construct the default behavior of the Element object.
        /// </summary>
        internal Element()
        {
            Information = new Information
            {
                CalledTimestamp = DateTime.Now
            };

            IsInitialized = false;
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

        internal readonly IWebElement WebElement;
        internal Log Log { get; }
    }
}
