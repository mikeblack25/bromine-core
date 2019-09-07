namespace Bromine.Core.ElementLocator
{
    /// <summary>
    /// Supported approaches to locate an element.
    /// </summary>
    public enum LocatorStrategy
    {
#pragma warning disable 1591
        Id = 1,
        Class,
        Css,
        Js,
        Tag,
        Text,
        PartialText,
        XPath
#pragma warning restore 1591
    }
}
