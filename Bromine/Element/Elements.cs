using System.Collections.Generic;

namespace Bromine.Element
{
    /// <summary>
    /// Get information about Elements.
    /// </summary>
    public static class Elements
    {
        /// <summary>
        /// Get attribute values from a given list of elements.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static List<string> Attribute(this List<IElement> elements, string attribute)
        {
            var list = new List<string>();

            foreach (var element in elements)
            {
                var value = element.GetAttribute(attribute);
                if (!string.IsNullOrEmpty(value))
                {
                    list.Add(value);
                }
            }

            return list;
        }
    }
}
