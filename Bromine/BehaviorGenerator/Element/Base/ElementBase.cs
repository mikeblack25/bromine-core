using System.Collections.Generic;
using System.Linq;

using Bromine.BehaviorGenerator.Constants;

namespace Bromine.BehaviorGenerator.Element.Base
{
    /// <summary>
    /// Common behavior of all elements.
    /// </summary>
    public class ElementBase
    {
        /// <inheritdoc />
        public ElementBase(string format, params GherkinActions[] actions)
        {
            Format = format;
            Keywords = actions.ToList();
        }

        /// <summary>
        /// Language syntax for the behavior of the element.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Keyword statements to generate for the given element.
        /// </summary>
        public List<GherkinActions> Keywords { get; }

        /// <summary>
        /// Does the element have the given attribute?
        /// </summary>
        public bool HasAttribute()
        {
            return false;
        }

        /// <summary>
        /// Does the element have the given attributes?
        /// </summary>
        public bool HasAttributes()
        {
            return false;
        }
    }
}
