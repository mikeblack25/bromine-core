﻿using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// Frame element.
    /// </summary>
    public class FrameElement : ElementBase
    {
        /// <inheritdoc />
        public FrameElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }
    }
}
