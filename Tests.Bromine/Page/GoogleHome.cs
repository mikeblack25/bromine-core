using Bromine.Core;
using Bromine.Core.ElementInteraction;

using OpenQA.Selenium;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Representation of the Google Homepage https://www.google.com.
    /// </summary>
    public class GoogleHome : global::Bromine.Core.Page
    {
        /// <inheritdoc />
        public GoogleHome(Browser browser) : base(browser) { }

#pragma warning disable 1591
        public Element AboutLink => Browser.SeleniumFind.ElementByText("About");
        public Element StoreLink => Browser.SeleniumFind.ElementByText("Store");
        public Element GmailLink => Browser.SeleniumFind.ElementByText("Gmail");
        public Element ImagesLink => Browser.SeleniumFind.ElementByText("Images");
        public Element GoogleAppsLink => Browser.Find.ElementByClasses("gb_z gb_uc");
        public Element ProfileLink => Browser.Find.ElementByClasses("gb_z gb_Ia gb_g");
        public Element GoogleImage => Browser.Find.Element("hplogo".Id());
        public Element SearchField => Browser.Find.ElementByClasses("gLFyf gsfi");
        public Element GoogleSearchButton => Browser.Find.Element("[value='Google Search']");
        public Element RandomSearchButton => Browser.Find.Element("center > div > div > div:nth-child(2) > span");
        public Element AdvertisingLink => Browser.SeleniumFind.ElementByText("Advertising");
        public Element BusinessLink => Browser.SeleniumFind.ElementByText("Business");
        public Element HowSearchWorksLink => Browser.SeleniumFind.ElementByText("How Search works");
        public Element PrivacyLink => Browser.SeleniumFind.ElementByText("Privacy");
        public Element TermsLink => Browser.SeleniumFind.ElementByText("Terms");
        public Element SettingsLink => Browser.SeleniumFind.ElementByText("Settings");
#pragma warning restore 1591

        /// <summary>
        /// search for the given searchTerm.
        /// </summary>
        /// <param name="searchTerm">Term to search for on Google.</param>
        public void Search(string searchTerm)
        {
            SearchField.SendKeys(searchTerm);
            SearchField.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// https://www.google.com/
        /// </summary>
        public override string Url => "https://www.google.com/";
    }
}
