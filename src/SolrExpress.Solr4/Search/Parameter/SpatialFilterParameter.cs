using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Search.Parameter
{
    [FieldMustBeIndexedTrue]
    public class SpatialFilterParameter<TDocument> : ISpatialFilterParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private string _result;

        public SpatialFilterParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }
        
        GeoCoordinate ISpatialFilterParameter<TDocument>.CenterPoint { get; set; }

        decimal ISpatialFilterParameter<TDocument>.Distance { get; set; }

        Expression<Func<TDocument, object>> ISearchParameterFieldExpression<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType ISpatialFilterParameter<TDocument>.FunctionType { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            container.Add(this._result);
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            var parameter = ((ISpatialFilterParameter<TDocument>)this);
            var fieldName = this._expressionBuilder.GetFieldNameFromExpression(parameter.FieldExpression);
            var formule = ParameterUtil.GetSpatialFormule(fieldName, parameter.FunctionType, parameter.CenterPoint, parameter.Distance);

            this._result = $"fq={formule}";
        }
    }
}