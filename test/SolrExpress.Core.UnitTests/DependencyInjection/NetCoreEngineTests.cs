using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace SolrExpress.Core.UnitTests.DependencyInjection
{
    public class TestSearchParameterBuilder<TDocument> : ISearchParameterBuilder<TDocument>
        where TDocument : IDocument
    {
        public IEngine Engine { get; set; }

        IAnyParameter<TDocument> ISearchParameterBuilder<TDocument>.Any(string name, string value)
        {
            throw new NotImplementedException();
        }

        IBoostParameter<TDocument> ISearchParameterBuilder<TDocument>.Boost(ISearchParameterValue<TDocument> query, BoostFunctionType? boostFunctionType)
        {
            throw new NotImplementedException();
        }

        IFacetFieldParameter<TDocument> ISearchParameterBuilder<TDocument>.FacetField(Expression<Func<TDocument, object>> expression, FacetSortType? sortType, int? limit, params string[] excludes)
        {
            throw new NotImplementedException();
        }

        IFacetLimitParameter<TDocument> ISearchParameterBuilder<TDocument>.FacetLimit(int value)
        {
            throw new NotImplementedException();
        }

        IFacetQueryParameter<TDocument> ISearchParameterBuilder<TDocument>.FacetQuery(string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType, params string[] excludes)
        {
            throw new NotImplementedException();
        }
        
        IFacetRangeParameter<TDocument> ISearchParameterBuilder<TDocument>.FacetRange(string aliasName, Expression<Func<TDocument, object>> expression, string gap, string start, string end, bool countBefore, bool countAfter, FacetSortType? sortType, params string[] excludes)
        {
            throw new NotImplementedException();
        }

        IFieldsParameter<TDocument> ISearchParameterBuilder<TDocument>.Fields(params Expression<Func<TDocument, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        IFilterParameter<TDocument> ISearchParameterBuilder<TDocument>.Filter(ISearchParameterValue<TDocument> value)
        {
            throw new NotImplementedException();
        }

        IFilterParameter<TDocument> ISearchParameterBuilder<TDocument>.Filter(Expression<Func<TDocument, object>> expression, string value, string tagName)
        {
            throw new NotImplementedException();
        }

        IFilterParameter<TDocument> ISearchParameterBuilder<TDocument>.Filter<TValue>(Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName)
        {
            throw new NotImplementedException();
        }

        ILimitParameter<TDocument> ISearchParameterBuilder<TDocument>.Limit(int value)
        {
            throw new NotImplementedException();
        }

        IMinimumShouldMatchParameter<TDocument> ISearchParameterBuilder<TDocument>.MinimumShouldMatch(string expression)
        {
            throw new NotImplementedException();
        }

        IOffsetParameter<TDocument> ISearchParameterBuilder<TDocument>.Offset(int value)
        {
            throw new NotImplementedException();
        }

        IQueryParameter<TDocument> ISearchParameterBuilder<TDocument>.Query(string value)
        {
            throw new NotImplementedException();
        }

        IQueryParameter<TDocument> ISearchParameterBuilder<TDocument>.Query(ISearchParameterValue<TDocument> value)
        {
            throw new NotImplementedException();
        }

        IQueryParameter<TDocument> ISearchParameterBuilder<TDocument>.Query(Expression<Func<TDocument, object>> expression, string value)
        {
            throw new NotImplementedException();
        }

        IQueryFieldParameter<TDocument> ISearchParameterBuilder<TDocument>.QueryField(string expression)
        {
            throw new NotImplementedException();
        }

        IRandomSortParameter<TDocument> ISearchParameterBuilder<TDocument>.RandomSort(bool ascendent)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ISortParameter<TDocument>> ISearchParameterBuilder<TDocument>.Sort(bool ascendent, params Expression<Func<TDocument, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        ISortParameter<TDocument> ISearchParameterBuilder<TDocument>.Sort(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            throw new NotImplementedException();
        }

        ISpatialFilterParameter<TDocument> ISearchParameterBuilder<TDocument>.SpatialFilter(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance)
        {
            throw new NotImplementedException();
        }
    }

    public class TestDocument1 : IDocument
    {
    }

    public class TestDocument2 : IDocument
    {
    }

    public class NetCoreEngineTests
    {
        /// <summary>
        /// Where   Using a SearchParameterBuilder instance
        /// When    Registering 2 Documents and invoke Any from second document
        /// What    Get class that implements ISearchParameterBuilder
        /// </summary>
        [Fact]
        public void SearchParameterBuilder001()
        {
            // Arrange
            var engine = (IEngine)new NetCoreEngine();
            engine.AddTransient<ISearchParameterBuilder<TestDocument1>, TestSearchParameterBuilder<TestDocument1>>();
            engine.AddTransient<ISearchParameterBuilder<TestDocument2>, TestSearchParameterBuilder<TestDocument2>>();

            // Act
            var result = engine.GetService<ISearchParameterBuilder<TestDocument2>>();

            // Assert
            Assert.IsType(typeof(TestSearchParameterBuilder<TestDocument2>), result);
        }
    }
}