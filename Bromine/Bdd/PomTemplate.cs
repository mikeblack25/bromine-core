using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bromine.Bdd
{
    /// <summary>
    /// 
    /// </summary>
    public class PomTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        public PomTemplate(string pomNamespace, string pomName, List<string> elementNames, string pageUrl = "")
        {
            ElementList = new List<string>();

            Namespace = pomNamespace;
            Name = pomName;
            Url = pageUrl;
            ElementList = elementNames;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> ElementList { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Header()
        {
            var summary = $"/// Representation of the {Name}page {Url}".Trim();

            var builder = new StringBuilder();
            builder.AppendLine("using Bromine.Core;");
            builder.AppendLine("using Bromine.Core.ElementInteraction;");
            builder.AppendLine();
            builder.AppendLine("using OpenQA.Selenium;");
            builder.AppendLine();
            builder.AppendLine($"namespace {Namespace}");
            builder.AppendLine("{");
            builder.AppendLine("    /// <summary>");
            builder.AppendLine($"    {summary}.");
            builder.AppendLine("    /// </summary>");
            builder.AppendLine($"    public class {Name} : Page");
            builder.AppendLine("    {");
            builder.AppendLine("        /// <inheritdoc />");
            builder.AppendLine($"        public {Name}(Browser browser) : base(browser)");
            builder.AppendLine("        {");
            builder.AppendLine("        }");
            builder.AppendLine();

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Elements()
        {
            var builder = new StringBuilder();
            builder.AppendLine("#pragma warning disable 1591");

            builder.AppendLine("#pragma warning restore 1591");
        }
    }
}
