using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// Button element.
    /// </summary>
    public class ButtonElementStateBase : StateElementBase
    {
        /// <inheritdoc />
        public ButtonElementStateBase(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }
    }
}
