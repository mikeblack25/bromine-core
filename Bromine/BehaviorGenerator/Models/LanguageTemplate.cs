using System.Xml.Serialization;

namespace Bromine.BehaviorGenerator.Models
{
    /// <summary>
    /// Template for statements used to generate behavior statements.
    /// Use {0} in the string to denote where the element should be added to the statement.
    /// </summary>
    public class LanguageTemplate
    {
        /// <inheritdoc />
        public LanguageTemplate()
        {
            HasAttribute = InitialString;
            HasAttributes = InitialString;
            NavigationUrl = InitialString;
            Click = InitialString;
            IsEnabled = InitialString;
            IsDisabled = InitialString;
            Text = InitialString;
            TextContains = InitialString;
            TextStartsWith = InitialString;
            TextEndsWith = InitialString;
            IsVisible = InitialString;
            IsNotVisible = InitialString;
            Options = InitialString;
            EnterText = InitialString;
            Image = InitialString;
        }

        /// <summary>
        /// Formatted string for checking if an element has an expected attribute.
        /// Use {1} in the string to denote where the attribute should be checked.
        /// </summary>
        [XmlElement]
        public string HasAttribute { get; set; }

        /// <summary>
        /// Formatted string for checking if an element has expected attributes.
        /// Use {1} in the string to denote where the attributes should be checked.
        /// Attributes in the {1} should be separated by ','.
        /// </summary>
        public string HasAttributes { get; set; }

        /// <summary>
        /// Formatted string for checking if an element has navigated to the expected URL.
        /// Use {1} in the string to denote where the URL should be checked.
        /// </summary>
        public string NavigationUrl { get; set; }

        /// <summary>
        /// Formatted string for clicking an element.
        /// </summary>
        public string Click { get; set; }

        /// <summary>
        /// Formatted string for checking if an element is enabled.
        /// </summary>
        public string IsEnabled { get; set; }

        /// <summary>
        /// Formatted string for checking if an element is disabled.
        /// </summary>
        public string IsDisabled { get; set; }

        /// <summary>
        /// Formatted string for checking if an element has the expected text.
        /// Use {1} in the string to denote where the expected text should be checked.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Formatted string for checking if the expected text is contained in the element text.
        /// Use {1} in the string to denote where the expected text should be checked.
        /// </summary>
        public string TextContains { get; set; }

        /// <summary>
        /// Formatted string for checking if the element text starts with the expected text.
        /// Use {1} in the string to denote where the expected text should be checked.
        /// </summary>
        public string TextStartsWith { get; set; }

        /// <summary>
        /// Formatted string for checking if the element text ends with the expected text.
        /// Use {1} in the string to denote where the expected text should be checked.
        /// </summary>
        public string TextEndsWith { get; set; }

        /// <summary>
        /// Formatted string for checking if an element is visible.
        /// </summary>
        public string IsVisible { get; set; }

        /// <summary>
        /// Formatted string for checking if an element is not visible.
        /// </summary>
        public string IsNotVisible { get; set; }

        /// <summary>
        /// Formatted string for checking if an element options (for a dropdown element).
        /// Use {1} in the string to denote the expected options.
        /// Options in the {1} should be separated by ','.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// Formatted string for entering text into a field.
        /// Use {1} in the string to denote the text entered in the field.
        /// </summary>
        public string EnterText { get; set; }

        /// <summary>
        /// Formatted string for the expected image.
        /// Note: The image will be compared against a baseline for comparison.
        /// </summary>
        public string Image { get; set; }

        private const string InitialString = " ";
    }
}
