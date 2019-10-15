using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// Field Element. A field is an element that accepts input (SendKeys in Selenium.
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
        public void EnterText(string textToEnter)
        {

        }
    }
}
