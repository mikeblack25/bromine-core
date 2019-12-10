namespace Bromine.Core.ElementLocator
{
    /// <summary>
    /// Supported approaches to locate an element.
    /// </summary>
    public enum LocatorStrategy
    {
#pragma warning disable 1591
        Undefined = 0,
        Id,
        Class,
        Css,
        Text,
        PartialText,
#pragma warning restore 1591
    }
}
