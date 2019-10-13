﻿using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Behaviour.Extension
{
    /// <summary>
    /// Extension classes to manipulate ChangeDynamicFieldBehaviour instances
    /// </summary>
    public static class ChangeDynamicFieldBehaviourExtension
    {
        /// <summary>
        /// Create a not mapped parameter
        /// </summary>
        /// <param name="documentSearch">Document search engine</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="prefixName">Indicates prefix name in dynamic field configurations</param>
        /// <param name="suffixName">Indicates suffix name in dynamic field configurations</param>
        /// <returns>Document search engine</returns>
        public static DocumentCollectionSearch<TDocument> ChangeDynamicFieldBehaviour<TDocument>(this DocumentCollectionSearch<TDocument> documentSearch, Expression<Func<TDocument, object>> fieldExpression, string prefixName = null, string suffixName = null)
            where TDocument : Document
        {
            Checker.IsNull(fieldExpression);

            var behaviour = documentSearch.ServiceProvider.GetService<IChangeDynamicFieldBehaviour<TDocument>>();
            behaviour.FieldExpression = fieldExpression;
            behaviour.DynamicFieldPrefix = prefixName;
            behaviour.DynamicFieldSuffix = suffixName;

            documentSearch.Add(behaviour);

            return documentSearch;
        }

        /// <summary>
        /// Create a not mapped parameter
        /// </summary>
        /// <param name="solrExpressBuilder">Solr express builder</param>
        /// <param name="fieldExpression">Expression used to find field name</param>
        /// <param name="prefixName">Indicates prefix name in dynamic field configurations</param>
        /// <param name="suffixName">Indicates suffix name in dynamic field configurations</param>
        /// <returns>Solr express builder</returns>
        public static SolrExpressBuilder<TDocument> ChangeDynamicFieldBehaviour<TDocument>(this SolrExpressBuilder<TDocument> solrExpressBuilder, Expression<Func<TDocument, object>> fieldExpression, string prefixName = null, string suffixName = null)
            where TDocument : Document
        {
            Checker.IsNull(fieldExpression);

            var behaviour = solrExpressBuilder.ServiceProvider.GetService<IChangeDynamicFieldBehaviour<TDocument>>();
            behaviour.FieldExpression = fieldExpression;
            behaviour.DynamicFieldPrefix = prefixName;
            behaviour.DynamicFieldSuffix = suffixName;

            solrExpressBuilder.Options.GlobalChangeBehaviours.Add(behaviour);

            return solrExpressBuilder;
        }
    }
}
