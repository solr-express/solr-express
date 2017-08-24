using Moq;
using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Update;
using System.Collections.Generic;
using System.IO;

namespace SolrExpress.Benchmarks.Helper
{
    public static class SolrExpressBuilderExtension
    {
        /// <summary>
        /// Use SolrExpress services implemented in SOLR 4
        /// </summary>
        /// <param name="solrExpressBuilder">Builder to control SolrExpress behavior</param>
        /// <returns>Builder to control SolrExpress behavior</returns>
        public static SolrExpressBuilder<TDocument> UseSolrFake<TDocument>(this SolrExpressBuilder<TDocument> solrExpressBuilder)
            where TDocument : Document
        {
            var searchItemCollection = new Mock<ISearchItemCollection<TDocument>>();
            searchItemCollection
                .Setup(q => q.Execute(It.IsAny<string>()))
                .Returns(new JsonTextReader(new StringReader("{}")));
            searchItemCollection
                .Setup(q => q.GetSearchParameters())
                .Returns(new List<ISearchParameter>());
            searchItemCollection
                .Setup(q => q.GetSearchResults())
                .Returns(new List<ISearchResult<TDocument>>());


            solrExpressBuilder
                .ServiceProvider
                .AddTransient<IAnyParameter, FakeAnyParameter>()
                .AddTransient<IAtomicUpdate<TDocument>>(new Mock<IAtomicUpdate<TDocument>>().Object)
                .AddTransient<IAtomicDelete<TDocument>>(new Mock<IAtomicDelete<TDocument>>().Object)
                .AddTransient<IBoostParameter<TDocument>>(new Mock<IBoostParameter<TDocument>>().Object)
                .AddTransient<IDefaultFieldParameter<TDocument>>(new Mock<IDefaultFieldParameter<TDocument>>().Object)
                .AddTransient<IFacetFieldParameter<TDocument>>(new Mock<IFacetFieldParameter<TDocument>>().Object)
                .AddTransient<IFacetLimitParameter<TDocument>>(new Mock<IFacetLimitParameter<TDocument>>().Object)
                .AddTransient<IFacetQueryParameter<TDocument>>(new Mock<IFacetQueryParameter<TDocument>>().Object)
                .AddTransient<IFacetRangeParameter<TDocument>>(new Mock<IFacetRangeParameter<TDocument>>().Object)
                .AddTransient<IFacetSpatialParameter<TDocument>>(new Mock<IFacetSpatialParameter<TDocument>>().Object)
                .AddTransient<IFacetsResult<TDocument>>(new Mock<IFacetsResult<TDocument>>().Object)
                .AddTransient<IFieldsParameter<TDocument>>(new Mock<IFieldsParameter<TDocument>>().Object)
                .AddTransient<IFilterParameter<TDocument>>(new Mock<IFilterParameter<TDocument>>().Object)
                .AddTransient<ILimitParameter<TDocument>>(new Mock<ILimitParameter<TDocument>>().Object)
                .AddTransient<IMinimumShouldMatchParameter<TDocument>>(new Mock<IMinimumShouldMatchParameter<TDocument>>().Object)
                .AddTransient<IOffsetParameter<TDocument>>(new Mock<IOffsetParameter<TDocument>>().Object)
                .AddTransient<IQueryFieldParameter<TDocument>>(new Mock<IQueryFieldParameter<TDocument>>().Object)
                .AddTransient<IQueryParameter<TDocument>>(new Mock<IQueryParameter<TDocument>>().Object)
                .AddTransient<IQueryParserParameter<TDocument>>(new Mock<IQueryParserParameter<TDocument>>().Object)
                .AddTransient<ISearchItemCollection<TDocument>>(searchItemCollection.Object)
                .AddTransient<ISortParameter<TDocument>>(new Mock<ISortParameter<TDocument>>().Object)
                .AddTransient<ISortRandomlyParameter<TDocument>>(new Mock<ISortRandomlyParameter<TDocument>>().Object)
                .AddTransient<ISpatialFilterParameter<TDocument>>(new Mock<ISpatialFilterParameter<TDocument>>().Object)
                .AddTransient<IStandardQueryParameter<TDocument>>(new Mock<IStandardQueryParameter<TDocument>>().Object)
                .AddTransient<ISystemParameter<TDocument>>(new Mock<ISystemParameter<TDocument>>().Object)
                .AddTransient<IWriteTypeParameter<TDocument>>(new Mock<IWriteTypeParameter<TDocument>>().Object);

            return solrExpressBuilder;
        }
    }
}
