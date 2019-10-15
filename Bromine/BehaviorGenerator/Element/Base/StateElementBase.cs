using Bromine.BehaviorGenerator.Constants;

namespace Bromine.BehaviorGenerator.Element.Base
{
    /// <summary>
    /// Common behavior of all elements that can change the state of a page.
    /// </summary>
    public class StateElementBase : VisibleElementBase
    {
        /// <inheritdoc />
        public StateElementBase(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// Destination URL to check on the resulting navigation of an element.
        /// </summary>
        public string DestinationUrl { get; }

        /// <summary>
        /// Click the element.
        /// </summary>
        public void Click()
        {

        }
    }
}
