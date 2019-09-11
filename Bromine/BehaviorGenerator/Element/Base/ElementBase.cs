using System.Collections.Generic;
using System.Linq;
using Bromine.BehaviorGenerator.Constants;

namespace Bromine.BehaviorGenerator.Element.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ElementBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="actions"></param>
        public ElementBase(string format, params GherkinActions[] actions)
        {
            Format = format;
            Keywords = actions.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<GherkinActions> Keywords { get; }

        /// <summary>
        /// 
        /// </summary>
        public void HasAttribute()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void HasAttributes()
        {

        }
    }
}
