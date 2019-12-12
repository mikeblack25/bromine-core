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
        /// <param name="log"><see cref="Log"/></param>
        /// <param name="locatorString">Locator string used to find the requested element.</param>
        /// <param name="locatorType">Type of locator used to find the requested element.</param>
        internal Element(IWebElement element, Log log, string locatorString = "", Strategy locatorType = Strategy.Undefined) : this()
        {
            SeleniumElement = element;
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
        /// Selenium IWebElement.
        /// </summary>
        public IWebElement SeleniumElement { get; }

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
        public bool Enabled => (bool) GetProperty(() => SeleniumElement.Enabled, "Unable to find the enabled property for the requested element");

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
        public bool Displayed => (bool)GetProperty(() => SeleniumElement.Displayed, "Unable to find the displayed property for the requested element");

        /// <summary>
        /// Find the parent element of the requested element.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <returns></returns>
        public Element ParentElement => (Element)GetProperty(() => new Element(SeleniumElement.FindElement(By.XPath("..")), Log, ".."), "Unable to find the parent element for the requested element");

        /// <summary>
        /// Find the requested element with the given attribute.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="attributeName">Attribute name of the requested element.</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName) => (string)GetProperty(() => SeleniumElement.GetAttribute(attributeName), $"Unable to find the value {attributeName} for the requested element");

        /// <summary>
        /// Get the CSS value for the requested element by property name.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="cssValue">CSS value for the requested element.</param>
        /// <returns></returns>
        public string GetCssValue(string cssValue) => (string)GetProperty(() => SeleniumElement.GetCssValue(cssValue), $"Unable to find the CSS value {cssValue} for the requested element");

        /// <summary>
        /// Get the JavaScript value for the requested property.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">Property value for the requested element.</param>
        /// <returns></returns>
        public string GetJavaScriptProperty(string propertyName) => (string)GetProperty(() => SeleniumElement.GetProperty(propertyName), $"Unable to find the property {propertyName} for the requested element");

        /// <summary>
        /// Update the value property for the requested element.
        /// </summary>
        /// <param name="text">Text to update to the requested element.</param>
        public void SendKeys(string text)
        {
            if (SeleniumElement != null)
            {
                SeleniumElement.SendKeys(text);
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
            if (SeleniumElement != null)
            {
                SeleniumElement.Clear();
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
            if (SeleniumElement != null)
            {
                SeleniumElement.Click();
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
            if (SeleniumElement != null)
            {
                SeleniumElement.Submit();
            }
            else
            {
                Log.Error("Unable to submit to the requested element");
            }
        }

        /// <summary>
        /// Convert an IReadOnlyCollection of IWebElements to a List of IElements.
        /// </summary>
        /// <param name="elements"></param>
        internal static List<IElement> ToList(IReadOnlyCollection<IWebElement> elements)
        {
            var elementList = new List<IElement>();

            foreach (var element in elements)
            {
                elementList.Add(new Element(element));
            }

            return elementList;
        }

        /// <summary>
        /// Find an element by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find a requested element.</param>
        /// <returns></returns>
        internal IElement FindElement(By by) => new Element(SeleniumElement.FindElement(by), Log);

        /// <summary>
        /// Find elements by the requested locator strategy.
        /// </summary>
        /// <param name="by">Locator strategy to use to find requested elements.</param>
        /// <returns></returns>
        internal List<IElement> FindElements(By by)
        {
            var list = new List<IElement>();

            var elements = SeleniumElement.FindElements(by);

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

        internal Element(IWebElement element)
        {
            Information = new Information
            {
                CalledTimestamp = DateTime.Now
            };

            SeleniumElement = element;

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

        internal Log Log { get; }
    }


    /// <summary>
    /// Extension methods to provide additional capabilities to Elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Find child elements with the given strategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="strategy">How will the element be found?</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<IElement> FindElements(this IElement element, Strategy strategy, string locator) => Element.ToList(element.SeleniumElement.FindElements(SeleniumFind.Element(strategy, locator)));

        /// <summary>
        /// Find child elements by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find children of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static List<IElement> FindElements(this IElement element, string locator) => element.FindElements(Strategy.Css, locator);

        /// <summary>
        /// Find child element with the given strategy and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="strategy">How will the element be found.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static IElement FindElement(this IElement element, Strategy strategy, string locator) => FindElements(element, strategy, locator).FirstOrDefault();

        /// <summary>
        /// Find child element by CSS and locator string.
        /// </summary>
        /// <param name="element">Parent element to find a child of.</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        public static IElement FindElement(this IElement element, string locator) => FindElements(element, Strategy.Css, locator).FirstOrDefault();
    }
}
