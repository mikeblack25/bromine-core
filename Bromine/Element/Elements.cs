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

        /// <summary>
        /// Get an Element in a list by index.
        /// NOTE: An empty Element will be returned if the provided index is out of range.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IElement ElementByIndex(this List<IElement> elements, int index)
        {
            IElement element = new Element();

            try
            {
                if (elements != null)
                {
                    if (index < elements.Count)
                    {
                        element = elements[index];
                    }
                }
            }
            catch
            {
                // TODO: Should an error be logged if the check fails?
            }

            return element;
        }
    }
}
