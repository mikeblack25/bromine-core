using System;
using System.Collections.Generic;

using Bromine;
using Bromine.Core;
using Bromine.Core.Element;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Page Object Model (POM) based on Common.html.
    /// </summary>
    public class Common : global::Bromine.Core.Page
    {
        /// <inheritdoc />
        public Common(IBrowser browser) : base(browser)
        {
        }

#pragma warning disable 1591
        public override string Url => $@"{AppDomain.CurrentDomain.BaseDirectory}\Pages\Common.html";

        public Element EnableButtonId => Browser.Find.Element(EnableButtonIdUnformattedString);
        public Element EnableButtonInvalidSeleniumId => Browser.SeleniumFind.ElementById(InvalidString);

        public Element EnableButtonClass => Browser.Find.Element(EnableButtonClassUnformattedString);
        public Element EnableButtonInvalidSeleniumClass => Browser.SeleniumFind.ElementByClass(InvalidString);

        public Element EnableButtonCss => Browser.Find.Element(EnableButtonIdString);
        public Element EnableButtonInvalidSeleniumCss => Browser.SeleniumFind.ElementByCssSelector(InvalidString);

        public Element EnableButtonText => Browser.Find.Element(EnableButtonTextString);
        public Element EnableButtonInvalidSeleniumText => Browser.SeleniumFind.ElementByText(InvalidString);

        public Element EnableButtonPartialText => Browser.Find.Element(EnaButtonTextString);
        public Element EnableButtonInvalidSeleniumPartialText => Browser.SeleniumFind.ElementByPartialText(InvalidString);

        public Element EnabledButtonElementClasses => Browser.Find.ElementByClasses("button normal");
        public Element EnabledButtonChildElement => Browser.Find.ChildElement(DivTagString, EnableButtonIdString);
        public Element EnabledButtonChildElementParentElement => Browser.Find.ChildElement(Browser.Find.Element(DivTagString), EnableButtonIdString);
        public Element EnabledButtonDescendentCssElement => Browser.Find.ElementByDescendentCss("html body div #enabled_button");

        public Element ExampleField => Browser.Find.Element("field");

        public List<Element> EnabledButtonElementsClasses => Browser.Find.ElementsByClasses("button normal");
        public List<Element> EnabledButtonChildElements => Browser.Find.ChildElements(DivTagString, EnableButtonIdString);
        public List<Element> EnabledButtonDescendentCssElements => Browser.Find.ElementsByDescendentCss("html body div button");
#pragma warning restore 1591

        private const string DivTagString = "div";
        private const string EnableButtonIdUnformattedString = "enabled_button";
        private const string EnableButtonClassUnformattedString = "normal";
        private const string EnableButtonIdString = "#enabled_button";
        private const string EnableButtonTextString = "Enabled";
        private const string EnaButtonTextString = "Ena";
        private const string InvalidString = "123456";
    }
}
