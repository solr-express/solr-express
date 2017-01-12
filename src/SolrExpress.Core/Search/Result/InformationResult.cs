using Newtonsoft.Json.Linq;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// Statistic data builder
    /// </summary>
    public sealed class InformationResult<TDocument> : IInformationResult<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        void IConvertJsonObject.Execute(IEnumerable<ISearchParameter> parameters, JObject jsonObject)
        {
            Checker.IsNull(parameters);
            Checker.IsTrue<UnexpectedJsonFormatException>(jsonObject["response"]?["numFound"] == null || jsonObject["responseHeader"]?["QTime"] == null, new[] { jsonObject.ToString() });

            var qTime = jsonObject["responseHeader"]["QTime"].ToObject<int>();
            var documentCount = jsonObject["response"]["numFound"].ToObject<long>();

            this.Data = InformationBuilder<TDocument>.Create(parameters, qTime, documentCount);
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public Information Data { get; set; }
    }
}
