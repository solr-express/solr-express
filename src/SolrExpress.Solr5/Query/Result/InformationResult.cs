using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Query.Result
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
        public void Execute(List<IParameter> parameters, JObject jsonObject)
        {
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
