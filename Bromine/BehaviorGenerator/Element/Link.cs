﻿using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// Link element.
    /// </summary>
    public class LinkElement : VisibleElementBase
    {
        /// <inheritdoc />
        public LinkElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }
    }
}
