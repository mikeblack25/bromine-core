using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace Bromine.Core
{
    /// <summary>
    /// Provides ability to interact with elements.
    /// </summary>
    public class Element : IWebElement
    {
        public string TagName => _element?.TagName;

        public string Text => _element?.Text;

        public bool Enabled => _element.Enabled;

        public bool Selected => _element.Selected;

        public Point Location => _element.Location;

        public Size Size => _element.Size;

        public bool Displayed => _element.Displayed;

        public CallingInformation Information { get; private set; }

        public List<Exception> Exceptions { get; private set; }

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

        internal Element()
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

        public void Clear()
        {
            if (_isInitialized)
            {
                try
                {
                    _element.Clear();
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }
        }

        public void Click()
        {
            if (_isInitialized)
            {
                try
                {
                    _element.Click();
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            }
        }

        public Element FindElement(By by)
        {
            return new Element(_element.FindElement(by));
        }

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

        public string GetProperty(string propertyName)
        {
            var property = string.Empty;

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

        public void Submit()
        {
            _element.Submit();
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            return FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            throw new System.NotImplementedException();
        }

        private IWebElement _element;
        private bool _isInitialized;
    }


    public class CallingInformation
    {
        public string CallingMethod { get; set; }

        public DateTime CalledTimestamp { get; set; }

        public string LocatorString { get; set; }

        public LocatorType LocatorType { get; set; }
    }


    public enum LocatorType
    {
        Class = 1,
        CssSelector,
        Id,
        Tag,
        Text,
        PartialText,
        XPath
    }
}
