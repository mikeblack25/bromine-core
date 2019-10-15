using Bromine.BehaviorGenerator.Constants;

namespace Bromine.BehaviorGenerator.Element.Base
{
    /// <summary>
    /// Common behavior of visible elements.
    /// </summary>
    public class VisibleElementBase : ElementBase
    {
        /// <inheritdoc />
        public VisibleElementBase(string format, params GherkinActions[] actions) : base(format, actions)
        {
        }

        /// <summary>
        /// Is the element enabled?
        /// </summary>
        public bool IsEnabled()
        {
            return false;
        }

        /// <summary>
        /// Is the element disabled?
        /// </summary>
        public bool IsDisabled()
        {
            return false;
        }

        /// <summary>
        /// Is the expected element text equal to the element text?
        /// </summary>
        /// <param name="expectedText">Expected element text.</param>
        public bool Text(string expectedText)
        {
            return false;
        }

        /// <summary>
        /// Is the expected text contained in the element text?
        /// </summary>
        /// <param name="expectedText">Expected text contained in the element text.</param>
        public bool TextContains(string expectedText)
        {
            return false;
        }

        /// <summary>
        /// Does the element text start with the expected text?
        /// </summary>
        /// <param name="expectedText">Expected starting text of the element text.</param>
        /// <returns></returns>
        public bool TextStartsWith(string expectedText)
        {
            return false;
        }

        /// <summary>
        /// Does the element text end with the expected text?
        /// </summary>
        /// <param name="expectedText">Expected ending text of the element text.</param>
        /// <returns></returns>
        public bool TextEndsWith(string expectedText)
        {
            return false;
        }

        /// <summary>
        /// Is the element visible?
        /// </summary>
        public bool IsVisible()
        {
            return false;
        }

        /// <summary>
        /// Is the element text not visible?
        /// </summary>
        public bool IsNotVisible()
        {
            return false;
        }
    }
}
