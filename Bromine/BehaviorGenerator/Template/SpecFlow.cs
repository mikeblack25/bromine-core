using System.Collections.Generic;
using System.Text;
using Bromine.BehaviorGenerator.Constants;
using Bromine.BehaviorGenerator.Element.Base;

namespace Bromine.BehaviorGenerator.Template
{
    /// <summary>
    /// 
    /// </summary>
    public class SpecFlowTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        public SpecFlowTemplate(List<ElementBase> elements)
        {
            Elements = elements;
        }

        private void BuildSteps()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private string BuildAttribute(GherkinActions action, string format)
        {
            return $"[{action.ToString()}(\"{format}\")]";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string BuildMethodHeader(GherkinActions action, string format)
        {
            var items = format.Split(' ');

            var builder = new StringBuilder($"public void {action}");

            foreach (var item in items)
            {
                var formattedItem = item.Substring(0).ToUpper();
                builder.Append(formattedItem);
                builder.Append("()");
                builder.AppendLine("{");
            }

            return builder.ToString();
        }


        private List<ElementBase> Elements { get; }
        /*
         *[Given("we '(.*)' items in stock\.")]
public void GivenWeHaveASpecificNumberOfItemsInStock(string itemsInStockExpression)
{
   var itemsInStock = (itemsInStockExpression == "no")
                    ? 0
                    : int.Parse(itemsInStockExpression);
   // ... set up the stock
}
         *
         */
    }
}
