using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Utility
{
    public class ExpressionBuilder<TDocument> : IExpressionBuilder<TDocument>
        where TDocument : IDocument
    {
        private readonly IExpressionCache<TDocument> _expressionCache;

        public ExpressionBuilder(IExpressionCache<TDocument> expressionCache)
        {
            this._expressionCache = expressionCache;
        }

        string IExpressionBuilder<TDocument>.GetFieldNameFromExpression(Expression<Func<TDocument, object>> expression)
        {
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            ((IExpressionBuilder<TDocument>)this).GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return solrFieldAttribute == null ? propertyInfo.Name : solrFieldAttribute.Name;
        }

        void IExpressionBuilder<TDocument>.GetInfosFromExpression(Expression<Func<TDocument, object>> expression, out PropertyInfo propertyInfo, out SolrFieldAttribute solrFieldAttribute)
        {
            if (this._expressionCache.Get(expression, out propertyInfo, out solrFieldAttribute))
            {
                return;
            }

            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)lambda.Body;

                    memberExpression = (MemberExpression)unaryExpression.Operand;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    break;
            }

            if (propertyInfo != null)
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                solrFieldAttribute = (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);

                this._expressionCache.Set(expression, propertyInfo, solrFieldAttribute);
            }
            else
            {
                throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }
        }

        string IExpressionBuilder<TDocument>.GetPropertyNameFromExpression(Expression<Func<TDocument, object>> expression)
        {
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            ((IExpressionBuilder<TDocument>)this).GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return propertyInfo.Name;
        }

        Type IExpressionBuilder<TDocument>.GetPropertyTypeFromExpression(Expression<Func<TDocument, object>> expression)
        {
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            ((IExpressionBuilder<TDocument>)this).GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return propertyInfo.PropertyType;
        }

        SolrFieldAttribute IExpressionBuilder<TDocument>.GetSolrFieldAttributeFromPropertyInfo(Expression<Func<TDocument, object>> expression)
        {
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            ((IExpressionBuilder<TDocument>)this).GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return solrFieldAttribute;
        }
    }
}
