using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageElement : NavigationElementBase
    {

        /// <inheritdoc />
        public ImageElement(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Image()
        {

        }
    }
}
