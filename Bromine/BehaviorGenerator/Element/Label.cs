using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class LabelElement : VisibleElementBase
    {
        /// <inheritdoc />
        public LabelElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }
    }
}
