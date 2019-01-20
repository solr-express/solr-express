using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    [FieldMustBeIndexedTrue]
    public sealed class SpatialFilterParameter<TDocument> : BaseSpatialFilterParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        private string _result;

        public SpatialFilterParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public void AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            var formule = ParameterUtil.GetSpatialFormule(fieldName, this.FunctionType, this.CenterPoint, this.Distance);

            this._result = $"fq={formule}";
        }
    }
}