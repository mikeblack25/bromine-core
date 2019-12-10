using System;
using System.Collections.Generic;
using System.Drawing;

using Bromine.Logger;

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
        internal Element(IWebElement element, Log log, string locatorString = "", Strategy locatorType = 0) : this()
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
        public string TagName
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.TagName;
                }

                Log.Error("Unable to find the tag for the requested element");

                return string.Empty;
            }
        }

        /// <summary>
        /// Element Text value.
        /// </summary>
        public string Text
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Text;
                }

                Log.Error("Unable to find the text for the requested element");

                return string.Empty;
            }
        }

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Enabled;
                }

                Log.Error("Unable to find the enabled property for the requested element");

                return false;
            }
        }

        /// <summary>
        /// Element selected status.
        /// </summary>
        public bool Selected
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Selected;
                }

                Log.Error("Unable to find the selected property for the requested element");

                return false;
            }
        }

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        public Point Location
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Location;
                }

                Log.Error("Unable to find the location for the requested element");

                return new Point();
            }
        }

        /// <summary>
        /// Element size.
        /// </summary>
        public Size Size
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Size;
                }

                Log.Error("Unable to find the size for the requested element");

                return new Size();
            }
        }

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        public bool Displayed
        {
            get
            {
                if (WebElement != null)
                {
                    return WebElement.Displayed;
                }

                Log.Error("Unable to find the displayed property for the requested element");

                return false;
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
        /// Find the parent element of the requested element.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <returns></returns>
        public Element ParentElement
        {
            get
            {
                if (WebElement != null)
                {
                    return new Element(WebElement.FindElement(By.XPath("..")), Log, "..");
                }

                Log.Error("Unable to find the displayed property for the requested element");

                return new Element();
            }
        }

        /// <summary>
        /// Find the requested element with the given attribute.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="attributeName">Attribute name of the requested element.</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            if (WebElement != null)
            {
                return WebElement.GetAttribute(attributeName);
            }
            else
            {
                Log.Error($"Unable to find the attribute {attributeName} for the requested element");

                return string.Empty;
            }
        }

        /// <summary>
        /// Get the CSS value for the requested element by property name.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="cssValue">CSS value for the requested element.</param>
        /// <returns></returns>
        public string GetCssValue(string cssValue)
        {
            if (WebElement != null)
            {
                return WebElement.GetCssValue(cssValue);
            }
            else
            {
                Log.Error($"Unable to find the CSS value {cssValue} for the requested element");

                return string.Empty;
            }
        }

        /// <summary>
        /// Get the value for the requested property.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">Property value for the requested element.</param>
        /// <returns></returns>
        public string GetProperty(string propertyName)
        {
            if (WebElement != null)
            {
                return WebElement.GetProperty(propertyName);
            }
            else
            {
                Log.Error($"Unable to find the property {propertyName} for the requested element");

                return string.Empty;
            }
        }

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

        internal readonly IWebElement WebElement;
        internal Log Log { get; }
    }
}
