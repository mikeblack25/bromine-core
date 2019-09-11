using System.Collections.Generic;
using System.Linq;

namespace Bromine.Bdd
{
    /// <summary>
    /// 
    /// </summary>
    public class BddElementBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="keywords"></param>
        public BddElementBase(string format, params GherkinKeywords[] keywords)
        {
            Format = format;
            Keywords = keywords.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<GherkinKeywords> Keywords { get; }

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
