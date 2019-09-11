using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class FieldElement : VisibleElementBase
    {
        /// <inheritdoc />
        public FieldElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string SendKeys { get; set; }
    }
}
