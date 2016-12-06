using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SolrExpress.Core.Utility
{
    public class ExpressionCache : IExpressionCache
    {
        private readonly Regex _expressionKeyRegex = new Regex(@"(\w+)\s\=\>\s(Convert\()?\1\.(\w+)\)?", RegexOptions.Compiled);
        private readonly Dictionary<string, Tuple<PropertyInfo, SolrFieldAttribute>> _internalCache = new Dictionary<string, Tuple<PropertyInfo, SolrFieldAttribute>>();

        /// <summary>
        /// Get key from informed expression
        /// </summary>
        /// <param name="expression">Expression used to get key</param>
        /// <returns>A key from informed expression</returns>
        private string GetKeyFromExpression<TDocument>(Expression<Func<TDocument, object>> expression)
        {
            return this._expressionKeyRegex.Match(expression.ToString()).Groups[3].Value;
        }

        bool IExpressionCache.Get<TDocument>(Expression<Func<TDocument, object>> expression, out PropertyInfo propertyInfo, out SolrFieldAttribute solrFieldAttribute)
        {
            propertyInfo = null;
            solrFieldAttribute = null;

            var key = GetKeyFromExpression(expression);

            if (this._internalCache.ContainsKey(key))
            {
                var cache = this._internalCache[key];

                propertyInfo = cache.Item1;
                solrFieldAttribute = cache.Item2;
            }

            return false;
        }

        void IExpressionCache.Set<TDocument>(Expression<Func<TDocument, object>> expression, PropertyInfo propertyInfo, SolrFieldAttribute solrFieldAttribute)
        {
            var key = GetKeyFromExpression(expression);
            var cache = new Tuple<PropertyInfo, SolrFieldAttribute>(propertyInfo, solrFieldAttribute);

            if (this._internalCache.ContainsKey(key))
            {
                this._internalCache[key] = cache;
            }
            else
            {
                this._internalCache.Add(key, cache);
            }
        }
    }
}
