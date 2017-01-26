using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result.Utility;
using SolrExpress.Serialization;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Document data builder
    /// </summary>
    /// <typeparam name="TDocument">Type of the document returned in the search</typeparam>
    public sealed class DocumentResult<TDocument> : IDocumentResult<TDocument>
        where TDocument : IDocument
    {
        IEnumerable<TDocument> IDocumentResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            Checker.IsNull(searchParameters);
            Checker.IsNullOrWhiteSpace(jsonPlainText);

            var match = SearchResultRegex.DocumentResultFragment.Match(jsonPlainText);
            // TODO: Create exception
            //Checker.IsTrue<UnexpectedJsonFormatException>(match.Success, jsonPlainText);

            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> {
                    new GeoCoordinateConverter(),
                    new DateTimeConverter()
                },
                ContractResolver = new CustomContractResolver()
            };

            ((IDocumentResult<TDocument>)this).Data = JsonConvert.DeserializeObject<List<TDocument>>(match.Groups[3].Value, settings);
        }
    }
}
