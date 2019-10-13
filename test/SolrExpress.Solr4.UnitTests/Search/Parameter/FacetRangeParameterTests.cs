﻿using Moq;
using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
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
                const string expected1 = "facet=true&facet.range=id";

                Action<IFacetRangeParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id");
                };
                const string expected2 = "facet=true&facet.range={!key=Id}id";

                Action<IFacetRangeParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1);
                };
                const string expected3 = "facet=true&facet.range={!key=Id}id&f.id.facet.mincount=1";

                Action<IFacetRangeParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc);
                };
                const string expected4 = "facet=true&facet.range={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1";

                Action<IFacetRangeParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                const string expected5 = "facet=true&facet.range={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config6 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                const string expected6 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config7 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true);
                };
                const string expected7 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config8 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true);
                };
                const string expected8 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config9 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1");
                };
                var expected9 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config10 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1").Start("10");
                };
                const string expected10 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.start=10&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config11 = facet =>
                {
                    facet.FieldExpression(q => q.Id).AliasName("Id").Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2").CountAfter(true).CountBefore(true).Gap("1").Start("10").End("20");
                };
                var expected11 = "facet=true&facet.range={!ex=tag1,tag2 key=Id}id&f.id.facet.range.gap=1&f.id.facet.range.start=10&f.id.facet.range.end=20&f.id.facet.range.other=before&f.id.facet.range.other=after&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetRangeParameter<TestDocument>> config12 = facet =>
                {
                    facet.FieldExpression(q => q.Id).HardEnd(true);
                };
                const string expected12 = "facet=true&facet.range=id&f.id.facet.range.hardend=true";

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
                    new object[] { config11, expected11 },
                    new object[] { config12, expected12 }
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
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder, null);
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
                var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
                var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
                expressionBuilder.LoadDocument();

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

                Action<IFacetRangeParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.Filter(filter => filter.Field(q => q.Id).EqualsTo(10));
                };

                return new[]
                {
                    new object[] { config1 },
                    new object[] { config2 },
                    new object[] { config3 }
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
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            serviceProvider
                .Setup(q => q.GetService<SearchQuery<TestDocument>>())
                .Returns(new SearchQuery<TestDocument>(expressionBuilder));
            var parameter = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder, serviceProvider.Object);
            config.Invoke(parameter);

            // Act / Assert
            Assert.Throws<UnsupportedFeatureException>(() => ((ISearchItemExecution<List<string>>)parameter).Execute());
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
