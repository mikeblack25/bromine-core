using System.Collections.Generic;
using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownElement : VisibleElementBase
    {
        /// <inheritdoc />
        public DropdownElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Items { get; set; }
    }
}
