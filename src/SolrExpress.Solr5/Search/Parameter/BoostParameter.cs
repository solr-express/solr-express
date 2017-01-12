using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public class BoostParameter<TDocument> : BaseBoostParameter<TDocument>, ISearchParameterExecute<JObject>
        where TDocument : IDocument
    {
        public BoostParameter(IExpressionBuilder<TDocument> expressionBuilder)
            : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            var boostFunction = this.BoostFunctionType.ToString().ToLower();

            jObj.Add(new JProperty(boostFunction, this.Query.Execute()));

            jObject["params"] = jObj;
        }
    }
}
