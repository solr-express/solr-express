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
        /// Returns information about field from indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Information about field from indicated expression</returns>
        private FieldData GetData(Expression<Func<TDocument, object>> expression)
        {
            // TODO: Improve performance
            PropertyInfo propertyInfo;
            SolrFieldAttribute solrFieldAttribute;

            this.GetInfosFromExpression(expression, out propertyInfo, out solrFieldAttribute);

            return this.fieldsData[propertyInfo.Name];
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
        /// Set prefix name in dynamic field
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <param name="value">Prefix name in dynamic field</param>
        public void SetDynamicFieldPrefixName(Expression<Func<TDocument, object>> expression, string value)
        {
            var data = this.GetData(expression);
            data.DynamicFieldPrefixName = value;
        }

        /// <summary>
        /// Set suffix name in dynamic field
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <param name="value">Suffix name in dynamic field</param>
        public void SetDynamicFieldSuffixName(Expression<Func<TDocument, object>> expression, string value)
        {
            var data = this.GetData(expression);
            data.DynamicFieldSuffixName = value;
        }

        /// <summary>
        /// Returns type of POCO property from indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Type of POCO property from indicated expression</returns>
        public Type GetPropertyType(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).PropertyType;
        }

        /// <summary>
        /// Returns name of alias of field in queries
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Name of alias of field in queries</returns>
        public string GetAliasName(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).AliasName;
        }

        /// <summary>
        /// Returns name of field in the SOLR schema
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Name of field in the SOLR schema</returns>
        public string GetFieldName(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).FieldName;
        }

        /// <summary>
        /// Returns if field can be used in queries to retrieve matching documents
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>If true, value of the field can be used in queries to retrieve matching documents</returns>
        public bool GetIsIndexed(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).IsIndexed;
        }

        /// <summary>
        /// Returns if field can be retrieved by queries
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>If true, actual value of the field can be retrieved by queries</returns>
        public bool GetIsStored(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).IsStored;
        }
    }
}
