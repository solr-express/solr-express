using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Core.Utility
{
    public interface IExpressionCache<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get cache by informed expression
        /// </summary>
        /// <param name="expression">Expression used in key</param>
        /// <param name="propertyInfo">Propert info in cache</param>
        /// <param name="solrFieldAttribute">Solr field attribute in cache</param>
        /// <returns>Returns true if cache was found, otherwise false</returns>
        bool Get(Expression<Func<TDocument, object>> expression, out PropertyInfo propertyInfo, out SolrFieldAttribute solrFieldAttribute);            

        /// <summary>
        /// Set cache by informed expression
        /// </summary>
        /// <param name="expression">Expression used in key</param>
        /// <param name="propertyInfo">Propert info to add in cache</param>
        /// <param name="solrFieldAttribute">Solr field attribute to add in cache</param>
        void Set(Expression<Func<TDocument, object>> expression, PropertyInfo propertyInfo, SolrFieldAttribute solrFieldAttribute);
    }
}
