using SolrExpress.Search.Result;
using System.Collections.Generic;
using SolrExpress.Search.Parameter;
using System;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetFieldResult<TDocument> : IFacetFieldResult<TDocument>
        where TDocument : IDocument
    {
        IEnumerable<FacetKeyValue<string>> IFacetFieldResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            Checker.IsNull(searchParameters);
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["facets"] == null, jsonObject.ToString());

            var list = jsonObject["facets"]
                .Children()
                .Where(q =>
                    q is JProperty &&
                    q.Values().Count() == 1 &&
                    ((JProperty)q).Value is JObject &&
                    ((JProperty)q).Value["buckets"] != null)
                .ToList();

            if (!list.Any())
            {
                this.Data = new List<FacetKeyValue<string>>();

                return;
            }

            this.Data = list
                .Select(item => new FacetKeyValue<string>
                {
                    Name = ((JProperty)item).Name,
                    Data = ((JProperty)(item)).Value["buckets"]
                        .Select(q => new FacetItemValue<string>
                        {
                            Key = q["val"].ToObject<string>(),
                            Quantity = q["count"].ToObject<long>()
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}
