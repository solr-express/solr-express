using Newtonsoft.Json.Linq;
using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using System;
using System.Collections.Generic;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR query result with fluent API
    /// </summary>
    public class SolrQueryResult<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private JObject _jsonObject;

        /// <summary>
        /// Factory used to resolve builder creation in Linq facilities
        /// </summary>
        protected readonly IBuilderFactory<TDocument> _builderFactory;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="builderFactory">Factory used to resolve builder creation in Linq facilities</param>
        /// <param name="json">Result of the SOLR</param>
        public SolrQueryResult(IBuilderFactory<TDocument> builderFactory, string json)
        {
            ThrowHelper<ArgumentNullException>.If(builderFactory == null);
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(json));

            this._builderFactory = builderFactory;
            this._jsonPlainText = json;
        }

        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T builder)
            where T : IResultBuilder
        {
            if (builder is IConvertJsonObject)
            {
                this._jsonObject = this._jsonObject ?? JObject.Parse(this._jsonPlainText);

                ((IConvertJsonObject)builder).Execute(this._jsonObject);
            }
            else if (builder is IConvertJsonPlainText)
            {
                ((IConvertJsonPlainText)builder).Execute(this._jsonPlainText);
            }
            else
            {
                throw new UnknownResolveResultBuilderException(builder.GetType().Name);
            }

            return builder;
        }

        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public SolrQueryResult<TDocument> Document(out List<TDocument> data)
        {
            data = this.Get(this._builderFactory.GetDocumentBuilder()).Data;

            return this;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public SolrQueryResult<TDocument> FacetField(out List<FacetKeyValue<string>> data)
        {
            data = this.Get(this._builderFactory.GetFacetFieldBuilder()).Data;

            return this;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public SolrQueryResult<TDocument> FacetQuery(out Dictionary<string, long> data)
        {
            data = this.Get(this._builderFactory.GetFacetQueryBuilder()).Data;

            return this;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public SolrQueryResult<TDocument> FacetRange(out List<FacetKeyValue<FacetRange>> data)
        {
            data = this.Get(this._builderFactory.GetFacetRangeBuilder()).Data;

            return this;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="data">Statics about search execution</param>
        public SolrQueryResult<TDocument> Statistic(out Statistic data)
        {
            data = this.Get(this._builderFactory.GetStatisticBuilder()).Data;

            return this;
        }
    }
}
