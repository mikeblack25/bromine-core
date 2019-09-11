using Bromine.BehaviorGenerator.Constants;

namespace Bromine.BehaviorGenerator.Element.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class NavigationElementBase : VisibleElementBase
    {
        /// <inheritdoc />
        public NavigationElementBase(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Click()
        {

        }
    }
}
