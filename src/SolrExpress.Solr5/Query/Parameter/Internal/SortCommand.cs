using Newtonsoft.Json.Linq;

namespace SolrExpress.Solr5.Query.Parameter.Internal
{
    public class SortCommand
    {
        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="fieldName">Field name to add in sort parameter</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(string fieldName, bool ascendent, JObject jObject)
        {
            var jValue = (JValue)jObject["sort"] ?? new JValue((string)null);

            var value = $"{fieldName} {(ascendent ? "asc" : "desc")}";

            if (jValue.Value != null)
            {
                jValue.Value += $", {value}";
            }
            else
            {
                jValue.Value = value;
            }

            jObject["sort"] = jValue;
        }
    }
}
