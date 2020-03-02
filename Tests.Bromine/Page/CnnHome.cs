using Bromine.Core;
using Bromine.Core.ElementInteraction;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Testing of the CNN page.
    /// </summary>
    public class CnnHome : global::Bromine.Core.Page
    {
        /// <inheritdoc />
        public CnnHome(Browser browser) : base(browser) { }

#pragma warning disable 1591
        public Element HealthLink => Browser.SeleniumFind.ElementByText("Health");
#pragma warning restore 1591

        /// <summary>
        /// https://www.cnn.com/
        /// </summary>
        public override string Url => "https://www.cnn.com/";
    }
}
