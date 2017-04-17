using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Behaviour
{
    /// <summary>
    /// Change behaviour about dynamic field
    /// </summary>
    public class ChangeDynamicFieldBehaviour<TDocument> : IChangeDynamicFieldBehaviour<TDocument>
        where TDocument : IDocument
    {
        string IChangeDynamicFieldBehaviour<TDocument>.DynamicFieldPrefixName { get; set; }

        string IChangeDynamicFieldBehaviour<TDocument>.DynamicFieldSuffixName { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        void IChangeBehaviour.Execute()
        {
            var parameter = ((ISearchItemFieldExpression<TDocument>)this);
            var fieldData = parameter.ExpressionBuilder.GetData(parameter.FieldExpression);

            fieldData.DynamicFieldPrefixName = ((IChangeDynamicFieldBehaviour<TDocument>)this).DynamicFieldPrefixName;
            fieldData.DynamicFieldSuffixName = ((IChangeDynamicFieldBehaviour<TDocument>)this).DynamicFieldSuffixName;

            parameter.ExpressionBuilder.SetData(parameter.FieldExpression, fieldData);
        }
    }
}
