using System.Collections.Generic;

using Bromine.Element;

namespace Bromine.Core
{
    /// <summary>
    /// Maintain information through an automation session.
    /// Element history is currently supported for calls to <see cref="Find.Element"/>.
    /// NOTE: Set <see cref="BrowserOptions.LogElementHistory"/> to true when initializing the browser.
    /// </summary>
    public class Session
    {

        /// <inheritdoc />
        public Session()
        {
            ElementHistory = new List<Information>();
        }

        /// <summary>
        /// Get the session element history.
        /// </summary>
        public List<Information> History => ElementHistory;

        /// <summary>
        /// Add information about an element to the session history.
        /// </summary>
        /// <param name="information"></param>
        public void AddElement(Information information)
        {
            ElementHistory.Add(information);
        }
      
        private List<Information> ElementHistory { get; }
    }
}
