using System.Collections.Generic;

namespace Bromine.BehaviorGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pom"></param>
        /// <param name="pomDestination"></param>
        /// <param name="fileSuffix"></param>
        public ActionGenerator(object pom, string pomDestination, string fileSuffix = "Steps")
        {
            PomDestinationPath = pomDestination;
            var type = pom.GetType();
            var elements = type.GetProperties();
            var generatedFileName = $"{type.Name}{fileSuffix}.cs";

            foreach (var element in elements)
            {
                var name = element.Name;

                if (name.EndsWith(Button))
                {

                }
                else if (name.EndsWith(Dropdown))
                {

                }
                else if (name.EndsWith(Field))
                {

                }
                else if (name.EndsWith(Frame))
                {

                }
                else if (name.EndsWith(Image))
                {

                }
                else if (name.EndsWith(Label))
                {

                }
                else if (name.EndsWith(Link))
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Elements { get; }

        /// <summary>
        /// 
        /// </summary>
        public string PomDestinationPath { get; }

#pragma warning disable 1591
        public const string Button = "Button";
        public const string Dropdown = "Dropdown";
        public const string Field = "Field";
        public const string Frame = "Frame";
        public const string Image = "Image";
        public const string Label = "Label";
        public const string Link = "Link";
#pragma warning restore 1591

    }
}
