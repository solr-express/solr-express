using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Search.Result
{
    /// <summary>
    /// Statistic data builder
    /// </summary>
    public sealed class InformationResult<TDocument> : IInformationResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the statistic parse of the json
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void IConvertJsonObject.Execute(IEnumerable<ISearchParameter> parameters, JObject jsonObject)
        {
            Checker.IsNull(parameters);
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["response"]?["numFound"] == null || jsonObject["responseHeader"]?["QTime"] == null, new[] { jsonObject.ToString() });

            var qTime = jsonObject["responseHeader"]["QTime"].ToObject<int>();
            var documentCount = jsonObject["response"]["numFound"].ToObject<long>();

            this.Data = new Information().Calculate(parameters, qTime, documentCount);
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public Information Data { get; set; }
    }
}
