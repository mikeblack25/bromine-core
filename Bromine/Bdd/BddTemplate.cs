using System.Collections.Generic;
using System.Text;

namespace Bromine.Bdd
{
    /// <summary>
    /// 
    /// </summary>
    public class BddTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        public BddTemplate(List<BddElementBase> elements)
        {
            Elements = elements;
        }

        private void BuildSteps()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private string BuildAttribute(GherkinKeywords keyword, string format)
        {
            return $"[{keyword.ToString()}(\"{format}\")]";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string BuildMethodHeader(GherkinKeywords keyword, string format)
        {
            var items = format.Split(' ');

            var builder = new StringBuilder($"public void {keyword}");

            foreach (var item in items)
            {
                var formattedItem = item.Substring(0).ToUpper();
                builder.Append(formattedItem);
                builder.Append("()");
                builder.AppendLine("{");
            }

            return builder.ToString();
        }


        private List<BddElementBase> Elements { get; }
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
