using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Utility
{
    public class FakeNoAttributeSolrConnection<TDocument> : ISolrConnection<TDocument>
        where TDocument : Document
    {
        private readonly IList<string> _fieldNames;

        public FakeNoAttributeSolrConnection(IList<string> fieldNames)
        {
            this._fieldNames = fieldNames;
        }

        private static PropertyInfo GetPropertyInfoFromExpression(Expression<Func<TDocument, object>> expression)
        {
            PropertyInfo propertyInfo = null;
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)lambda.Body;

                    memberExpression = (MemberExpression)unaryExpression.Operand;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;

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

        private static SolrFieldAttribute GetSolrFieldAttributeFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes(true);
            return (SolrFieldAttribute)attrs.FirstOrDefault(q => q is SolrFieldAttribute);
        }

        public string Get(string handler, List<string> data)
        {
            if (handler.Equals("schema/fields"))
            {
                var fields = this._fieldNames
                    .Select(fieldName =>
                    {
                        return new
                        {
                            name = fieldName,
                            indexed = true,
                            stored = true
                        };
                    });

                var wrapper = new
                {
                    fields
                };

                return JsonConvert.SerializeObject(wrapper);
            }

            if (handler.Equals("schema/dynamicfields"))
            {
                return @"
                {
	                ""dynamicFields"": [{

                        ""name"": ""*_c"",
		                ""type"": ""currency"",
		                ""indexed"": true,
		                ""stored"": true

                    }]
                }";
            }

            throw new NotImplementedException();
        }

        public Stream GetStream(string handler, List<string> data)
        {
            throw new NotImplementedException();
        }

        public string Post(string handler, JObject data)
        {
            throw new NotImplementedException();
        }

        public Stream PostStream(string handler, JObject data)
        {
            throw new NotImplementedException();
        }
    }
}
