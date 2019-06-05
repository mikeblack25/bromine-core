using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using OpenQA.Selenium;

namespace Bromine.Core
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
        internal Element(IWebElement element, string locatorString = "", LocatorType locatorType = 0) : this()
        {
            _element = element;

            if (!string.IsNullOrEmpty(locatorString) && locatorType != 0)
            {
                Information.LocatorString = locatorString;
                Information.LocatorType = locatorType;
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Element TagName value.
        /// </summary>
        public string TagName => _element?.TagName;

        /// <summary>
        /// Element Text value.
        /// </summary>
        public string Text => _element?.Text;

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        public bool Enabled => _element.Enabled;

        /// <summary>
        /// Element selected status.
        /// </summary>
        public bool Selected => _element.Selected;

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        public Point Location => _element.Location;

        /// <summary>
        /// Element size.
        /// </summary>
        public Size Size => _element.Size;

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        public bool Displayed => _element.Displayed;

        /// <summary>
        /// Details about the location strategy used for the requested element.
        /// </summary>
        public CallingInformation Information { get; }

        /// <summary>
        /// Clear the element content. This is usually used on a user editable field element.
        /// </summary>
        public void Clear()
        {
            if (!_isInitialized) { return; }

            try
            {
                _element.Clear();
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
            if (!_isInitialized) { return; }

            try
            {
                _element.Click();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Find an element by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find a requested element.</param>
        /// <returns></returns>
        public Element FindElement(By by)
        {
            return new Element(_element.FindElement(by));
        }

        /// <summary>
        /// Find elements by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find requested elements.</param>
        /// <returns></returns>
        public List<Element> FindElements(By by)
        {
            var list = new List<Element>();

            var elements = _element.FindElements(by);

            foreach (var element in elements)
            {
                list.Add(new Element(element));
            }

            return list;
        }

        /// <summary>
        /// Find the parent element of the requested element.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <returns></returns>
        public Element GetParent()
        {
            if (_isInitialized)
            {
                try
                {
                    return new Element(_element.FindElement(By.XPath("..")), "..", LocatorType.XPath);
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

            if (_isInitialized)
            {
                try
                {
                    attribute = _element.GetAttribute(attributeName);
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

            if (_isInitialized)
            {
                try
                {
                    cssValue = _element.GetCssValue(propertyName);
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
            if (_isInitialized)
            {
                try
                {
                    return _element.GetProperty(propertyName);
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
            if (_isInitialized)
            {
                try
                {
                    _element.SendKeys(text);
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
            _element.Submit();
        }

        /// <summary>
        /// Construct the default behavior of the Element object.
        /// </summary>
        private Element()
        {
            Exceptions = new List<Exception>();
            var stackTrace = new StackTrace();

            Information = new CallingInformation
            {
                CallingMethod = stackTrace.GetFrame(2).GetMethod().Name,
                CalledTimestamp = DateTime.Now
            };

            _isInitialized = false;
        }

        private readonly IWebElement _element;
        private readonly bool _isInitialized;
        private List<Exception> Exceptions { get; }
    }


    /// <summary>
    /// Provide details about how and when the element was requested.
    /// </summary>
    public class CallingInformation
    {
        /// <summary>
        /// Name of the calling method that requested an element.
        /// </summary>
        public string CallingMethod { get; set; }

        /// <summary>
        /// Timestamp the element was requested.
        /// </summary>
        public DateTime CalledTimestamp { get; set; }

        /// <summary>
        /// String used to find the requested element.
        /// </summary>
        public string LocatorString { get; set; }

        /// <summary>
        /// Locator strategy used to find the requested element.
        /// </summary>
        public LocatorType LocatorType { get; set; }
    }


    /// <summary>
    /// Supported ways to locate an element.
    /// </summary>
    public enum LocatorType
    {
        Id = 1,
        Class,
        CssSelector,
        Js,
        Tag,
        Text,
        PartialText,
        XPath
    }
}
