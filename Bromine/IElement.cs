using System.Collections.Generic;
using System.Drawing;

using Bromine.Core;
using Bromine.Element;

using OpenQA.Selenium;

namespace Bromine
{
    /// <summary>
    /// Wrapper around the Selenium IWebElement. 
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Details about the location strategy used for the requested element.
        /// </summary>
        Information Information { get; }

        /// <summary>
        /// Session log object.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Raw Selenium IWebElement.
        /// </summary>
        IWebElement SeleniumElement { get; }

        /// <summary>
        /// By object used by Selenium.
        /// </summary>
        By SeleniumBy { get; }

        /// <summary>
        /// Element TagName value.
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// Element Text value.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Element Enabled status. This can be used to determine if an element can be interacted with.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Element selected status.
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// Element location in the rendered DOM.
        /// </summary>
        Point Location { get; }

        /// <summary>
        /// Element size.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Element displayed status. This is helpful as some interactions require an element to be in view.
        /// </summary>
        bool Displayed { get; }

        /// <summary>
        /// Find child elements with the given strategy and locator string.
        /// </summary>
        /// <param name="strategy">How will the element be found?</param>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        List<IElement> FindElements(Strategy strategy, string locator);

        /// <summary>
        /// Find child elements by CSS and locator string.
        /// </summary>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        List<IElement> FindElements(string locator);

        /// <summary>
        /// Find child element by CSS and locator string.
        /// </summary>
        /// <param name="locator">String to locate child elements.</param>
        /// <returns></returns>
        IElement FindElement(string locator);

        /// <summary>
        /// Find the requested element with the given attribute.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="attributeName">Attribute name of the requested element.</param>
        /// <returns></returns>
        string GetAttribute(string attributeName);

        /// <summary>
        /// Get the CSS value for the element by property name.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="cssValue">CSS value for the element.</param>
        /// <returns></returns>
        string GetCss(string cssValue);

        /// <summary>
        /// Get the JavaScript value for the requested property.
        /// Note: This requires first locating an element and then calling this.
        /// </summary>
        /// <param name="propertyName">Property value for the requested element.</param>
        /// <returns></returns>
        string GetJavaScriptProperty(string propertyName);

        /// <summary>
        /// Update the value of the attribute for the element.
        /// </summary>
        /// <param name="attributeName">Attribute name to update the value of.</param>
        /// <param name="attributeValue">New value for the attribute.</param>
        IElement SetAttribute(string attributeName, string attributeValue);
        
        /// <summary>
        /// Update the value property for the element.
        /// </summary>
        /// <param name="text">Update element text.</param>
        IElement SendKeys(string text);

        /// <summary>
        /// Clear the element content. This is usually used on a user editable field element.
        /// </summary>
        IElement Clear();

        /// <summary>
        /// Click the element. The element should be enabled to be clickable.
        /// </summary>
        IElement Click();

        /// <summary>
        /// Double click the left mouse button on the element.
        /// </summary>
        /// <returns></returns>
        IElement DoubleClick();

        /// <summary>
        /// Move the mouse to the element.
        /// </summary>
        /// <returns></returns>
        IElement MoveTo();
    }
}
