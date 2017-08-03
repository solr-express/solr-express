using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FacetRangeParameterTests
    {
        public static IEnumerable<object[]> Data1
        {
            get
            {
                Action<IFacetRangeParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                };
                var expected1 = "facet=true&facet.range=id";

                Action<IFacetRangeParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id");
                };
                var expected2 = "facet=true&facet.range={!key=Id}id";

                Action<IFacetRangeParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1);
                };
                var expected3 = "facet=true&facet.range={!key=Id}id&f.id.facet.mincount=1";

                Action<IFacetRangeParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc);
                };
                var expected4 = "facet=true&facet.range={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1";

                Action<IFacetRangeParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                var expected5 = "facet=true&facet.range={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config6 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                var expected6 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config7 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true);
                };
                var expected7 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config8 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true);
                };
                var expected8 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config9 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1");
                };
                var expected9 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config10 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1").Start("10");
                };
                var expected10 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.start=10&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config11 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1").Start("10").End("20");
                };
                var expected11 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.start=10&f.id.facet.range.end=20&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";
                
                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
                    new object[] { config6, expected6 },
                    new object[] { config7, expected7 },
                    new object[] { config8, expected8 },
                    new object[] { config9, expected9 },
                    new object[] { config10, expected10 },
                    new object[] { config11, expected11 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data1))]
        public void FacetRangeParameterTheory001(Action<IFacetRangeParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            var actual = string.Join("&", container);

            Assert.Equal(expectd, actual);
        }

        public static IEnumerable<object[]> Data2
        {
            get
            {
                var solrOptions = new SolrExpressOptions();
                var solrConnection = new FakeSolrConnection<TestDocument>();
                var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
                expressionBuilder.LoadDocument();
                var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

                Action<IFacetRangeParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.SortType(FacetSortType.IndexDesc);
                };

                Action<IFacetRangeParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.SortType(FacetSortType.CountDesc);
                };

                return new[]
                {
                    new object[] { config1 },
                    new object[] { config2 },
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Throws UnsupportedSortTypeException
        /// </summary>
        [Theory]
        [MemberData(nameof(Data2))]
        public void FacetRangeParameterTheory002(Action<IFacetRangeParameter<TestDocument>> config)
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => ((ISearchItemExecution<List<string>>)parameter).Execute());
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetRangeParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetRangeParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
