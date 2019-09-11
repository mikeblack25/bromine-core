using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonElementBase : NavigationElementBase
    {
        /// <inheritdoc />
        public ButtonElementBase(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }
    }
}
