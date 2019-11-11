using Bromine.Core;
using Bromine.Core.ElementInteraction;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Testing of the CalJobs page.
    /// </summary>
    public class CalJobsHome : global::Bromine.Core.Page
    {
        /// <inheritdoc />
        public CalJobsHome(Browser browser) : base(browser) { }

#pragma warning disable 1591
        public Element GeographicJobSearchButton => Browser.SeleniumFind.ElementByText("Geographic Job Search");
        public Element LosAngelesCountyLink => Browser.Find.Element("#cphMainContent_rptCountiesAM_hlCounty_18 > div");
#pragma warning restore 1591

        /// <summary>
        /// https://www.calcareers.ca.gov/
        /// </summary>
        public override string Url => "https://www.calcareers.ca.gov/";
    }
}
