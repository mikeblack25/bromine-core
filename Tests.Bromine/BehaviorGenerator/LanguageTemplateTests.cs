using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Bromine.BehaviorGenerator.Models;
using Xunit;

namespace Tests.Bromine.BehaviorGenerator
{
    /// <summary>
    /// Tests for the LanguageTemplate.
    /// </summary>
    public class LanguageTemplateTests
    {
        /// <summary>
        /// Create a default template for LanguageTemplate.
        /// </summary>
        [Fact]
        public void DeSerializeLanguageTemplate()
        {
            var obj = new LanguageTemplate();
            var serializer = new XmlSerializer(obj.GetType());
            var filePath = $@"{Environment.CurrentDirectory}\LanguageTemplate.xml";

            using (var reader = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(reader, obj);
            }

            // TODO: Add verify that all properties exist in the saved file.
        }
    }
}
