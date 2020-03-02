using Bromine.Core;
using Bromine.Core.ElementInteraction;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Test to check a search on the Google home page.
    /// </summary>
    public class GoogleSearchHome : global::Bromine.Core.Page
    {
        /// <inheritdoc />
        public GoogleSearchHome(Browser browser) : base(browser) { }

#pragma warning disable 1591
        public Element GoogleSearchField => Browser.Find.Element("#tsf > div:nth-child(2) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input");
        public Element GoogleSearchResults => Browser.Find.Element("#rso > div:nth-child(2) > div > div > div > div.r > a > h3");
#pragma warning restore 1591

        /// <summary>
        /// https://www.google.com/
        /// </summary>
        public override string Url => "https://www.google.com/";

    }
}
