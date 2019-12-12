using System;
using System.Collections.Generic;

using Bromine;

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

        public IElement EnableButtonId => Browser.Find.Element(EnableButtonIdUnformattedString);
        public IElement EnableButtonInvalidSeleniumId => Browser.SeleniumFind.ElementById(InvalidString);

        public IElement EnableButtonClass => Browser.Find.Element(EnableButtonClassUnformattedString);
        public IElement EnableButtonInvalidSeleniumClass => Browser.SeleniumFind.ElementByClass(InvalidString);

        public IElement EnableButtonCss => Browser.Find.Element(EnableButtonIdString);
        public IElement EnableButtonInvalidSeleniumCss => Browser.SeleniumFind.ElementByCssSelector(InvalidString);

        public IElement EnableButtonText => Browser.Find.Element(EnableButtonTextString);

        public IElement EnableButtonPartialText => Browser.Find.Element(EnaButtonTextString);
        public IElement EnableButtonInvalidSeleniumPartialText => Browser.SeleniumFind.ElementByPartialText(InvalidString);

        public IElement EnabledButtonElementClasses => Browser.Find.ElementByClasses("button normal");
        public IElement EnabledButtonChildElement => Browser.Find.ChildElement(DivTagString, EnableButtonIdString);
        public IElement EnabledButtonChildElementParentElement => Browser.Find.ChildElement(Browser.Find.Element(DivTagString), EnableButtonIdString);
        public IElement EnabledButtonDescendentCssElement => Browser.Find.ElementByDescendentCss("html body div #enabled_button");

        public IElement ExampleField => Browser.Find.Element("field");

        public List<IElement> EnabledButtonElementsClasses => Browser.Find.ElementsByClasses("button normal");
        public List<IElement> EnabledButtonChildElements => Browser.Find.ChildElements(DivTagString, EnableButtonIdString);
        public List<IElement> EnabledButtonDescendentCssElements => Browser.Find.ElementsByDescendentCss("html body div button");
#pragma warning restore 1591

        /// <summary>
        /// "123456" This string was picked because it is not a valid string for id, class, or css.
        /// </summary>
        public string InvalidString => "123456";

        private const string DivTagString = "div";
        private const string EnableButtonIdUnformattedString = "enabled_button";
        private const string EnableButtonClassUnformattedString = "normal";
        private const string EnableButtonIdString = "#enabled_button";
        private const string EnableButtonTextString = "Enabled";
        private const string EnaButtonTextString = "Ena";
    }
}
