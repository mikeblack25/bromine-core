using System;
using System.IO;
using System.Reflection;

using Bromine.Core;

using Xunit;
using System.Collections.Generic;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Test the behavior of the Find class.
    /// </summary>
    public class FindTests : IDisposable
    {
        public FindTests()
        {
            Browser = new Browser(BrowserType.Chrome);
        }

        /// <summary>
        /// Tests the supported Find.ElementBy methods to ensure elements can be located properly.
        /// </summary>
        [Fact]
        public void FindElementsByTest()
        {
            Browser.Navigate.ToFile($@"{BasePath}\{AmazonHome}");

            TryId(IdString);
            TryIds(IdString);

            TryClass(ClassString);
            TryClasses(ClassString);

            TryCss(CssSelectorString);
            TryCsses(CssSelectorString);

            TryText(TextString);
            TryTexts(TextString);

            TryPartialText(TextString.Substring(2));

            TryTag("div");
        }

        /// <summary>
        /// Dispose of the browser when the test is done.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();

            Assert.True(ErrorList.Count == 0, string.Join(",", ErrorList));
        }

        private void TryId(string id)
        {
            try
            {
                Browser.Find.ElementById(id);
            }
            catch
            {
                ErrorList.Add($"Unable to find element by id: {id}");
            }
        }

        private void TryIds(string id)
        {
            try
            {
                Browser.Find.ElementsById(id);
            }
            catch
            {
                ErrorList.Add($"Unable to find elements by id: {id}");
            }
        }

        private void TryClass(string className)
        {
            try
            {
                Browser.Find.ElementByClass(className);
            }
            catch
            {
                ErrorList.Add($"Unable to find element by className: {className}");
            }
        }

        private void TryClasses(string className)
        {
            try
            {
                Browser.Find.ElementsByClass(className);
            }
            catch
            {
                ErrorList.Add($"Unable to find elements by className: {className}");
            }
        }

        private void TryCss(string css)
        {
            try
            {
                Browser.Find.ElementByCssSelector(css);
            }
            catch
            {
                ErrorList.Add($"Unable to find element by CSS: {css}");
            }
        }

        private void TryCsses(string css)
        {
            try
            {
                Browser.Find.ElementsByCssSelector(css);
            }
            catch
            {
                ErrorList.Add($"Unable to find elements by CSS: {css}");
            }
        }

        private void TryText(string text)
        {
            try
            {
                Browser.Find.ElementByText(text);
            }
            catch
            {
                ErrorList.Add($"Unable to find element by text: {text}");
            }
        }

        private void TryTexts(string text)
        {
            try
            {
                Browser.Find.ElementsByText(text);
            }
            catch
            {
                ErrorList.Add($"Unable to find elements by text: {text}");
            }
        }

        private void TryPartialText(string text)
        {
            try
            {
                Browser.Find.ElementByPartialText(text);
            }
            catch
            {
                ErrorList.Add($"Unable to find element by partial text: {text}");
            }
        }

        private void TryTag(string text)
        {
            try
            {
                Browser.Find.ElementByTag(text);
            }
            catch
            {
                ErrorList.Add($"Unable to find elements by tag: {text}");
            }
        }

        private Browser Browser { get; }
        private List<string> ErrorList = new List<string>();

        private string BasePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6)}\Pages";
        private string AmazonHome => @"Amazon.com\Amazon.com.html";

        private string IdString => "s-suggestion";
        private string ClassString => "a-link-normal";
        private string CssSelectorString => $"#{IdString}";
        private string TextString => "Careers";
    }
}
