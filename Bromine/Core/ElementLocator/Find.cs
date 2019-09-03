using System;
using System.Collections.Generic;
using System.Text;

using Bromine.Core.ElementInteraction;

namespace Bromine.Core.ElementLocator
{
    /// <summary>
    /// Find elements on the current page / screen.
    /// NOTE: All Find methods use the CSS selector locator strategy.
    /// </summary>
    public class Find
    {
        /// <summary>
        /// Construct a Find object to locate elements.
        /// </summary>
        /// <param name="driver">Driver used to navigate.</param>
        public Find(Driver driver)
        {
            SeleniumFind = new SeleniumFind(driver);
        }

        /// <summary>
        /// Find element by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="className">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public Element ElementByClasses(params string[] className)
        {
            var builder = new StringBuilder();

            foreach (var item in className)
            {
                builder.Append($".{item}");
            }

            return Element(builder.ToString());
        }

        /// <summary>
        /// Find elements by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="className">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByClasses(params string[] className)
        {
            var builder = new StringBuilder();

            foreach (var item in className)
            {
                builder.Append($".{item}");
            }

            return Elements(builder.ToString());
        }

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="className">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public Element ElementByDescendentClass(params string[] className)
        {
            var builder = new StringBuilder();

            foreach (var item in className)
            {
                builder.Append($".{item} ");
            }

            return Element(builder.ToString().TrimEnd());
        }

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="className">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<Element> ElementsByDescendentClass(params string[] className)
        {
            var builder = new StringBuilder();

            foreach (var item in className)
            {
                builder.Append($".{item} ");
            }

            return Elements(builder.ToString().TrimEnd());
        }

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate element by CSS selector.</param>
        /// <param name="childLocator">Locate element by CSS selector.</param>
        /// <returns></returns>
        public Element ChildElement(string parentLocator, string childLocator) => Element(parentLocator).FindElement(childLocator);

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public Element ChildElement(Element parentElement, string childLocator) => parentElement.FindElement(childLocator);

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate the parent element by CSS selector.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public List<Element> ChildElements(string parentLocator, string childLocator) => Elements(parentLocator)[0].FindElements(childLocator);

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child elements by CSS selector.</param>
        /// <returns></returns>
        public List<Element> ChildElements(Element parentElement, string childLocator) => parentElement.FindElements(childLocator);

        /// <summary>
        /// Find Element by CSS selector.
        /// </summary>
        /// <param name="cssSelector">Locate element by CSS selector.</param>
        /// <returns></returns>
        public Element Element(string cssSelector)
        {
            try
            {
                return Elements(cssSelector)[0];
            }
            catch (Exception e)
            {
                SeleniumFind.Exceptions.Add(e);

                return new Element();
            }
        }

        /// <summary>
        /// Find Elements by CSS selector.
        /// </summary>
        /// <param name="cssSelector">Locate elements by CSS selector.</param>
        /// <returns></returns>
        public List<Element> Elements(string cssSelector) => SeleniumFind.Elements(LocatorStrategy.Css, cssSelector);

        private SeleniumFind SeleniumFind { get; }
    }
}
