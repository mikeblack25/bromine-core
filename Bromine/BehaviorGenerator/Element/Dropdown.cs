using System.Collections.Generic;

using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownElement : StateElementBase
    {
        /// <inheritdoc />
        public DropdownElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// Supported options for the dropdown element.
        /// </summary>
        public List<string> Options { get; }
    }
}
