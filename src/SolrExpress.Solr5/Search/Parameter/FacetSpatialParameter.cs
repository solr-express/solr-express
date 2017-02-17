using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{

    public class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        string IFacetSpatialParameter<TDocument>.AliasName { get; set; }

        bool ISearchParameter.AllowMultipleInstances { get; set; }

        GeoCoordinate IFacetSpatialParameter<TDocument>.CenterPoint { get; set; }

        decimal IFacetSpatialParameter<TDocument>.Distance { get; set; }

        string[] IFacetSpatialParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> IFacetSpatialParameter<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType IFacetSpatialParameter<TDocument>.FunctionType { get; set; }

        FacetSortType IFacetSpatialParameter<TDocument>.SortType { get; set; }

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
