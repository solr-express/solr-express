using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Builder
{
    /// <summary>
    /// Builder expressions class
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public sealed class ExpressionBuilder<TDocument>
        where TDocument : IDocument
    {
        private readonly SolrExpressOptions _solrExpressOptions;
        private Dictionary<string, FieldData> fieldsData = new Dictionary<string, FieldData>();

        public ExpressionBuilder(SolrExpressOptions solrExpressOptions)
        {
            this._solrExpressOptions = solrExpressOptions;
        }

        /// <summary>
        /// Get behaviour informations from indicated expression 
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <param name="propertyInfo">Informations about POCO property</param>
        /// <param name="solrFieldAttribute">Solr field attribute associeted with POCO property</param>
        private void GetInfosFromExpression(Expression<Func<TDocument, object>> expression, out PropertyInfo propertyInfo, out SolrFieldAttribute solrFieldAttribute)
        {
            propertyInfo = null;
            solrFieldAttribute = null;

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
            }
            else
            {
                throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }
        }

        /// <summary>
        /// Check informed field in SOLR server
        /// </summary>
        /// <param name="data"></param>
        private void CheckSolrField(ref FieldData data)
        {
            // TODO: Implements
            // https://github.com/solr-express/solr-express/issues/112

            data.IsIndexed = true;
            data.IsStored = true;
        }

        /// <summary>
        /// Process TDocument associeted with instance to create internal list of data
        /// </summary>
        internal void ProcessDocument()
        {
            var documentParameter = Expression.Parameter(typeof(TDocument), "document");
            var properties = typeof(TDocument)
#if NETCORE
                .GetTypeInfo()
#endif
                .GetProperties();

            foreach (var propertye in properties)
            {
                var nameProperty = Expression.Convert(Expression.Property(documentParameter, propertye.Name), typeof(object));
                var expression = Expression.Lambda<Func<TDocument, object>>(nameProperty, documentParameter);
                PropertyInfo propertyInfo;
                SolrFieldAttribute solrFieldAttribute;

                this.GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

                var data = new FieldData
                {
                    AliasName = propertyInfo.Name,
                    DynamicFieldPrefixName = solrFieldAttribute?.DynamicFieldPrefixName ?? this._solrExpressOptions.GlobalDynamicFieldPrefixName,
                    DynamicFieldSuffixName = solrFieldAttribute?.DynamicFieldSuffixName ?? this._solrExpressOptions.GlobalDynamicFieldSuffixName,
                    FieldName = solrFieldAttribute?.Name ?? propertyInfo.Name,
                    IsDynamicField = solrFieldAttribute?.IsDynamicField ?? false,
                    PropertyType = propertyInfo.PropertyType
                };

                this.CheckSolrField(ref data);

                this.fieldsData.Add(propertyInfo.Name, data);
            }
        }

        /// <summary>
        /// Returns information about field from indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Information about field from indicated expression</returns>
        public FieldData GetData(Expression<Func<TDocument, object>> expression)
        {
            // TODO: Improve performance
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            this.GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return this.fieldsData[propertyInfo.Name];
        }

        /// <summary>
        /// Set information about field from indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <param name="data">Information about field</param>
        public void SetData(Expression<Func<TDocument, object>> expression, FieldData data)
        {
            // TODO: Improve performance
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            this.GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            this.fieldsData.Remove(propertyInfo.Name);

            this.fieldsData.Add(propertyInfo.Name, data);
        }
    }
}
