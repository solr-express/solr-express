using Newtonsoft.Json.Linq;
using SolrExpress.Options;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SolrExpress.Builder
{
    /// <summary>
    /// Builder expressions class
    /// </summary>
    public sealed class ExpressionBuilder<TDocument>
        where TDocument : Document
    {
        private readonly SolrExpressOptions _solrExpressOptions;
        private readonly ISolrConnection _solrConnection;
        private readonly Dictionary<string, FieldData> _fieldsData = new Dictionary<string, FieldData>();
        private bool _isDocumentLoaded;
        internal Dictionary<string, FieldSchema> FieldSchemas;
        internal Dictionary<Regex, FieldSchema> DynamicFieldSchemas;

        public ExpressionBuilder(SolrExpressOptions solrExpressOptions, ISolrConnection solrConnection)
        {
            this._solrExpressOptions = solrExpressOptions;
            this._solrConnection = solrConnection;
        }

        /// <summary>
        /// Load schema of all fields in SOLR server
        /// </summary>
        internal void LoadSolrSchemaFields()
        {
            var allFields = JObject.Parse(this._solrConnection.Get("schema/fields", null));
            var allDynamicFields = JObject.Parse(this._solrConnection.Get("schema/dynamicfields", null));

            this.FieldSchemas = allFields["fields"]
                .ToDictionary(
                    k => k["name"].Value<string>(),
                    v => new FieldSchema
                    {
                        FieldName = v["name"].Value<string>(),
                        IsIndexed = v["indexed"]?.Value<bool>() ?? true,
                        IsStored = v["stored"]?.Value<bool>() ?? true
                    });

            this.DynamicFieldSchemas = allDynamicFields["dynamicFields"]
                .ToDictionary(
                    k => new Regex($"^{k["name"].Value<string>().Replace("*", "(.*)")}$", RegexOptions.Compiled),
                    v => new FieldSchema
                    {
                        FieldName = v["name"].Value<string>(),
                        IsIndexed = v["indexed"]?.Value<bool>() ?? true,
                        IsStored = v["stored"]?.Value<bool>() ?? true
                    });
        }

        private Expression RemoveConvert(Expression expression)
        {
            var appliedExpression = expression;
            var expectedNodeTypes = new[] { ExpressionType.Convert, ExpressionType.ConvertChecked };

            while (expectedNodeTypes.Contains(appliedExpression.NodeType))
            {
                appliedExpression = RemoveConvert(((UnaryExpression)appliedExpression).Operand);
            }

            return appliedExpression;
        }

        public Expression GetRootExpression(Expression expression)
        {
            var rootExpression = expression;

            while (rootExpression is MemberExpression memberExpression)
            {
                rootExpression = memberExpression.Expression;
            }

            return rootExpression;
        }

        /// <summary>
        /// Get property referenced into indicated expression 
        /// </summary>
        /// <param name="expression">Expression used to find property info</param>
        /// <returns>Property referenced into indicated expression</returns>
        internal PropertyInfo GetPropertyInfoFromExpression(Expression expression)
        {
            PropertyInfo propertyInfo = null;
            var lambda = (LambdaExpression)this.GetRootExpression(expression);

            MemberExpression memberExpression;

            // ReSharper disable once SwitchStatementMissingSomeCases
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
                default:
                    throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }

            if (propertyInfo == null)
            {
                throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }

            return propertyInfo;
        }

        /// <summary>
        /// Get Solr field attribute referenced into indicated property info
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo used to find attribute</param>
        /// <returns>Solr field attribute associeted with POCO property</returns>
        internal SolrFieldAttribute GetSolrFieldAttributeFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);
        }

        /// <summary>
        /// Get field schema from internal list of field schema
        /// </summary>
        /// <param name="name">Name of field to find</param>
        /// <returns>Field schema</returns>
        internal FieldSchema GetFieldSchema(string name)
        {
            if (this.FieldSchemas.ContainsKey(name))
            {
                return this.FieldSchemas[name];
            }

            foreach (var item in this.DynamicFieldSchemas)
            {
                if (item.Key.IsMatch(name))
                {
                    return new FieldSchema
                    {
                        FieldName = name,
                        IsIndexed = item.Value.IsIndexed,
                        IsStored = item.Value.IsStored
                    };
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Returns information about field from indicated expression
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>Information about field from indicated expression</returns>
        internal FieldData GetData(Expression<Func<TDocument, object>> expression)
        {
            if (!this._isDocumentLoaded)
            {
                this.LoadDocument();
            }

            var propertyInfo = this.GetPropertyInfoFromExpression(expression);

            return this._fieldsData[propertyInfo.Name];
        }

        /// <summary>
        /// Load TDocument associeted with instance to create internal list of data
        /// </summary>
        internal void LoadDocument()
        {
            var documentParameter = Expression.Parameter(typeof(TDocument), "document");
            var properties = typeof(TDocument)
#if NETCORE
                .GetTypeInfo()
#endif
                .GetProperties();

            this.LoadSolrSchemaFields();

            foreach (var property in properties)
            {
                var nameProperty = Expression.Convert(Expression.Property(documentParameter, property.Name), typeof(object));
                var expression = Expression.Lambda<Func<TDocument, object>>(nameProperty, documentParameter);
                var propertyInfo = this.GetPropertyInfoFromExpression(expression);
                var solrFieldAttribute = this.GetSolrFieldAttributeFromPropertyInfo(propertyInfo);

                if (solrFieldAttribute == null)
                {
                    continue;
                }

                var data = new FieldData
                {
                    AliasName = propertyInfo.Name,
                    DynamicFieldPrefixName = solrFieldAttribute.DynamicFieldPrefixName ?? this._solrExpressOptions.GlobalDynamicFieldPrefix,
                    DynamicFieldSuffixName = solrFieldAttribute.DynamicFieldSuffixName ?? this._solrExpressOptions.GlobalDynamicFieldSuffix,
                    IsDynamicField = solrFieldAttribute.IsDynamicField,
                    PropertyType = propertyInfo.PropertyType
                };

                if (solrFieldAttribute.IsMagicField)
                {
                    data.FieldSchema = new FieldSchema
                    {
                        IsIndexed = true,
                        IsStored = true,
                        FieldName = solrFieldAttribute.Name
                    };
                }
                else
                {
                    data.FieldSchema = this.GetFieldSchema(solrFieldAttribute.Name);
                }

                this._fieldsData.Add(propertyInfo.Name, data);
            }

            this._isDocumentLoaded = true;
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
            var data = this.GetData(expression);
            var fieldName = data.FieldSchema.FieldName;

            // ReSharper disable once InvertIf
            if (data.IsDynamicField)
            {
                if (!string.IsNullOrWhiteSpace(data.DynamicFieldPrefixName))
                {
                    fieldName = $"{data.DynamicFieldPrefixName}_{fieldName}";
                }

                if (!string.IsNullOrWhiteSpace(data.DynamicFieldSuffixName))
                {
                    fieldName = $"{fieldName}_{data.DynamicFieldSuffixName}";
                }
            }

            return fieldName;
        }

        /// <summary>
        /// Returns if field can be used in queries to retrieve matching documents
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>If true, value of the field can be used in queries to retrieve matching documents</returns>
        public bool GetIsIndexed(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).FieldSchema.IsIndexed;
        }

        /// <summary>
        /// Returns if field can be retrieved by queries
        /// </summary>
        /// <param name="expression">Expression used to find property features</param>
        /// <returns>If true, actual value of the field can be retrieved by queries</returns>
        public bool GetIsStored(Expression<Func<TDocument, object>> expression)
        {
            return this.GetData(expression).FieldSchema.IsStored;
        }
    }
}
