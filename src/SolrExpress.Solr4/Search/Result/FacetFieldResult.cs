using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Search.Result
{
    /// <summary>
    /// Facet field data builder
    /// </summary>
    public sealed class FacetFieldResult<TDocument> : IFacetFieldResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the JSON object parse in facet field list
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void IConvertJsonObject.Execute(IEnumerable<ISearchParameter> parameters, JObject jsonObject)
        {
            Checker.IsNull(parameters);
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["facet_counts"]?["facet_fields"] == null, jsonObject.ToString());

            if (!jsonObject["facet_counts"]["facet_fields"].Children().Any())
            {
                this.Data = new List<FacetKeyValue<string>>();
            }
            else
            {
                this.Data = jsonObject["facet_counts"]["facet_fields"]
                    .Children()
                    .Select(item =>
                    {
                        var value = new FacetKeyValue<string>
                        {
                            Name = ((JProperty)item).Name,
                            Data = new List<FacetItemValue<string>>()
                        };

                        var array = ((JArray)((JProperty)(item)).Value);

                        for (int i = 0; i < array.Count; i += 2)
                        {

                            ((List<FacetItemValue<string>>)value.Data).Add(new FacetItemValue<string> { Key = array[i].ToObject<string>(), Quantity = array[i + 1].ToObject<long>() });
                        }

                        return value;
                    })
                    .ToList();
            }
        }

        public IEnumerable<FacetKeyValue<string>> Data { get; set; }
    }
}
