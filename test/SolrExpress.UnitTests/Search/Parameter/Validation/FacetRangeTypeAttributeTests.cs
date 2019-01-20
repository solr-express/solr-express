using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter.Validation
{
    public sealed class FakeFacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : Document
    {
        public string AliasName { get; set; }
        public bool CountAfter { get; set; }
        public bool CountBefore { get; set; }
        public string End { get; set; }
        public string[] Excludes { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public string Gap { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public string Start { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }
        public bool HardEnd { get; set; }

        public void AddResultInContainer(List<string> container)
        {
            // Test purpose only
        }

        public bool Equals(ISearchParameter other)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            // Test purpose only
        }
    }

    public class TestDocumentFieldAllowToFacet : Document
    {
        [SolrField("FieldInt")]
        public int FieldInt { get; set; }
        [SolrField("FieldLong")]
        public long FieldLong { get; set; }
        [SolrField("FieldShort")]
        public short FieldShort { get; set; }
        [SolrField("FieldDouble")]
        public double FieldDouble { get; set; }
        [SolrField("FieldDecimal")]
        public decimal FieldDecimal { get; set; }
        [SolrField("FieldDateTime")]
        public DateTime FieldDateTime { get; set; }
        [SolrField("FieldFloat")]
        public float FieldFloat { get; set; }

        [SolrField("FieldNullableInt")]
        public int? FieldNullableInt { get; set; }
        [SolrField("FieldNullableLong")]
        public long? FieldNullableLong { get; set; }
        [SolrField("FieldNullableShort")]
        public short? FieldNullableShort { get; set; }
        [SolrField("FieldNullableDouble")]
        public double? FieldNullableDouble { get; set; }
        [SolrField("FieldNullableDecimal")]
        public decimal? FieldNullableDecimal { get; set; }
        [SolrField("FieldNullableDateTime")]
        public DateTime? FieldNullableDateTime { get; set; }
        [SolrField("FieldNullableFloat")]
        public float? FieldNullableFloat { get; set; }
    }

    public class FacetRangeTypeAttributeTests
    {
        public static IEnumerable<object[]> GetData()
        {
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr1 = (q) => q.FieldDateTime;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr2 = (q) => q.FieldDecimal;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr3 = (q) => q.FieldDouble;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr4 = (q) => q.FieldInt;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr5 = (q) => q.FieldLong;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr6 = (q) => q.FieldShort;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr7 = (q) => q.FieldFloat;

            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr11 = (q) => q.FieldNullableDateTime;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr12 = (q) => q.FieldNullableDecimal;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr13 = (q) => q.FieldNullableDouble;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr14 = (q) => q.FieldNullableInt;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr15 = (q) => q.FieldNullableLong;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr16 = (q) => q.FieldNullableShort;
            Expression<Func<TestDocumentFieldAllowToFacet, object>> expr17 = (q) => q.FieldNullableFloat;

            return new List<object[]>
            {
                new object[] { expr1 },
                new object[] { expr2 },
                new object[] { expr3 },
                new object[] { expr4 },
                new object[] { expr5 },
                new object[] { expr6 },
                new object[] { expr7 },
                new object[] { expr11 },
                new object[] { expr12 },
                new object[] { expr13 },
                new object[] { expr14 },
                new object[] { expr15 },
                new object[] { expr16 },
                new object[] { expr17 },
            };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void FacetRangeTypeAttributeTheory(Expression<Func<TestDocumentFieldAllowToFacet, object>> fieldExpression)
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocumentFieldAllowToFacet>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentFieldAllowToFacet>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = new FakeFacetRangeParameter<TestDocumentFieldAllowToFacet>
            {
                ExpressionBuilder = expressionBuilder
            };
            parameter.AliasName(".");
            parameter.FieldExpression(fieldExpression);

            var validator = new FacetRangeTypeAttribute();

            // Act
            var result = validator.IsValid<TestDocumentFieldAllowToFacet>(parameter, out var errorMessage);

            // Assert
            Assert.True(result);
        }
    }
}
