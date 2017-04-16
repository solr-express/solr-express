using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [AllowMultipleInstances]
    public class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private readonly List<string> _result = new List<string>();

        public FacetQueryParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        string IFacetQueryParameter<TDocument>.AliasName { get; set; }
        
        string[] IFacetQueryParameter<TDocument>.Excludes { get; set; }

        ISearchQuery IFacetQueryParameter<TDocument>.Query { get; set; }

        int? IFacetQueryParameter<TDocument>.Limit { get; set; }

        int? IFacetQueryParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetQueryParameter<TDocument>.SortType { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            if (!container.Contains("facet=true"))
            {
                container.Add("facet=true");
            }

            container.AddRange(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = (IFacetQueryParameter<TDocument>)this;
            var query = parameter.Query.Execute();

            this._result.Add($"facet.query={ParameterUtil.GetFacetName(parameter.Excludes, parameter.AliasName, query)}");

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string dummy;

                Checker.IsTrue<UnsupportedSortTypeException>(parameter.SortType.Value == FacetSortType.CountDesc || parameter.SortType.Value == FacetSortType.IndexDesc);

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out dummy);

                this._result.Add($"f.{parameter.AliasName}.facet.sort={typeName}");
            }

            if (parameter.Minimum.HasValue)
            {
                this._result.Add($"f.{parameter.AliasName}.facet.mincount={parameter.Minimum.Value}");
            }

            if (parameter.Limit.HasValue)
            {
                this._result.Add($"f.{parameter.AliasName}.facet.limit={parameter.Limit.Value}");
            }
        }
    }
}
