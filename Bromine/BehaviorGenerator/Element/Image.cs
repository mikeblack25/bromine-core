using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Element
{
    /// <summary>
    /// Image element.
    /// </summary>
    public class ImageElementState : StateElementBase
    {

        /// <inheritdoc />
        public ImageElementState(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// Is the image the expected image?
        /// </summary>
        public bool Image()
        {
            return false;
        }
    }
}
