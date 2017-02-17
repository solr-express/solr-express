using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class SpatialFilterParameter<TDocument> : ISpatialFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        GeoCoordinate ISpatialFilterParameter<TDocument>.CenterPoint { get; set; }

        decimal ISpatialFilterParameter<TDocument>.Distance { get; set; }

        Expression<Func<TDocument, object>> ISpatialFilterParameter<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType ISpatialFilterParameter<TDocument>.FunctionType { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}