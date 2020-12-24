using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bromine.Core;

namespace Bromine.Element
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
        /// <param name="browser">Browser used to navigate.</param>
        public Find(Browser browser)
        {
            SeleniumFind = new SeleniumFind(browser);
        }

        /// <summary>
        /// Find Element by a valid locator strategy.
        /// <see cref="Strategy"/> for supported options.
        /// </summary>
        /// <param name="locator">String to locate an element.</param>
        /// <param name="name"></param>
        /// <param name="sourcePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public IElement Element(string locator,
                                [System.Runtime.CompilerServices.CallerMemberName] string name = "",
                                [System.Runtime.CompilerServices.CallerFilePath] string sourcePath = "",
                                [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            var elements = new List<IElement>();
            IElement element;

            var locateTime = DateTime.Now;

            try
            {
                elements = Elements(locator, waitTime: 0);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

                if (Browser.BrowserOptions.StopOnError)
                {
                    // ReSharper disable once PossibleIntendedRethrow
                    throw e;
                }
            }
            finally
            {
                element = elements.Count > 0 ? elements.First() : new Element();

                UpdateElementInformation(element, locateTime, locator, name, sourcePath, lineNumber);
            }

            if (Browser.BrowserOptions.LogElementHistory)
            {
                Browser.Session.AddElement(element.Information);
            }

            return element;
        }

        /// <summary>
        /// Find Elements by a valid locator strategy.
        /// <see cref="Strategy"/> for supported options.
        /// </summary>
        /// <param name="locator">String to locate elements.</param>
        /// <param name="name"></param>
        /// <param name="sourcePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        public List<IElement> Elements(string locator,
                                       [System.Runtime.CompilerServices.CallerMemberName] string name = "",
                                       [System.Runtime.CompilerServices.CallerFilePath] string sourcePath = "",
                                       [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0,
                                       int waitTime = -1)
        {
            var locateTime = DateTime.Now;
            var implicitWait = Browser.Wait.ImplicitWaitTime;

            if (waitTime > -1)
            {
                Browser.Wait.EnableImplicitWait(waitTime);
            }

            var elements = SeleniumFind.ElementsByCssSelector(locator);

            if (elements.Count == 0) // No elements were found from the above call.
            {
                if (!locator.Contains(" ")) // Id and Class location doesn't have a string but can be in CssSelector location above.
                {
                    elements = SeleniumFind.ElementsById(locator);

                    if (elements.Count == 0)
                    {
                        elements = SeleniumFind.ElementsByClass(locator);
                    }
                }
            }

            foreach (var element in elements)
            {
                UpdateElementInformation(element, locateTime, locator, name, sourcePath, lineNumber);
            }

            if (waitTime > -1)
            {
                Browser.Wait.EnableImplicitWait(implicitWait);
            }

            return elements;
        }

        /// <summary>
        /// Find element by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public IElement ElementByClasses(string classes) => Element(BuildClasses(classes));

        /// <summary>
        /// Find elements by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<IElement> ElementsByClasses(string classes) => Elements(BuildClasses(classes));

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate element by CSS selector.</param>
        /// <param name="childLocator">Locate element by CSS selector.</param>
        /// <returns></returns>
        public IElement ChildElement(string parentLocator, string childLocator) => (Element(parentLocator) as Element).FindElement(childLocator);

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public IElement ChildElement(IElement parentElement, string childLocator) => (parentElement as Element).FindElement(childLocator);

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate the parent element by CSS selector.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public List<IElement> ChildElements(string parentLocator, string childLocator)
        {
            var elements = Elements(parentLocator);

            return elements.Count > 0 ? ChildElements(elements.First(), childLocator) : elements;
        }

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child elements by CSS selector.</param>
        /// <returns></returns>
        public List<IElement> ChildElements(IElement parentElement, string childLocator) => (parentElement as Element).FindElements(childLocator);

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public IElement ElementByDescendentCss(string classes) => Element(classes);

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<IElement> ElementsByDescendentCss(string classes) => Elements(classes);

        private void UpdateElementInformation(IElement element, DateTime requestTime, string locator = "", string name = "", string sourcePath = "", int lineNumber = 0)
        {
            element.Information.FindTime = (element.Information.Created - requestTime).Duration();
            element.Information.LocatorString = locator;
            element.Information.Name = name;
            element.Information.CallerFilePath = sourcePath;
            element.Information.CallerLineNumber = lineNumber;

            element.Information.Attributes = GetAttributes(element);
            element.Information.TotalTime = (DateTime.Now - element.Information.Created).Duration();
        }

        /// <summary>
        /// Get all element attributes.
        /// </summary>
        /// <param name="element">Element to get the attributes of.</param>
        /// <returns></returns>
        private Dictionary<string, object> GetAttributes(IElement element)
        {
            var dictionary = new Dictionary<string, object>();

            if (element.Information.IsInitialized)
            {
                var objects = (element as Element)?.Browser.ExecuteJs("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", element);
                var strings = ((IEnumerable)objects).Cast<object>().Select(x => x?.ToString()).ToArray();

                foreach (var attribute in strings)
                {
                    var attributes = attribute.Split(new[] { '[', ']', ',' });
                    dictionary.Add(attributes[1], attributes[2]);
                }

                dictionary.Add("text", element.Text);
            }

            return dictionary;
        }

        private string BuildClasses(string classes)
        {
            var classItems = classes.Split(' ');
            var builder = new StringBuilder();

            foreach (var item in classItems)
            {
                builder.Append(item.Class());
            }

            return builder.ToString();
        }

        private SeleniumFind SeleniumFind { get; }
        private Browser Browser => SeleniumFind.Browser;
        private Log Log => SeleniumFind.Log;
    }
}
