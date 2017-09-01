using Moq;
using SolrExpress.Builder;
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
    public class FacetFieldParameterTests
    {
        public static IEnumerable<object[]> Data1
        {
            get
            {
                Action<IFacetFieldParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                };
                const string expected1 = "facet=true&facet.field={!key=Id}id";

                Action<IFacetFieldParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1);
                };
                const string expected2 = "facet=true&facet.field={!key=Id}id&f.id.facet.mincount=1";

                Action<IFacetFieldParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc);
                };
                const string expected3 = "facet=true&facet.field={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1";

                Action<IFacetFieldParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                const string expected4 = "facet=true&facet.field={!key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetFieldParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                const string expected5 = "facet=true&facet.field={!ex=tag1,tag2 key=Id}id&f.id.facet.sort=count&f.id.facet.mincount=1&f.id.facet.limit=10";

                Action<IFacetFieldParameter<TestDocument>> config6 = facet =>
                {
                    facet.FieldExpression(q => q.Id).MethodType(FacetMethodType.DocValues);
                };
                const string expected6 = "facet=true&facet.field={!key=Id}id&f.id.facet.method=enum";

                Action<IFacetFieldParameter<TestDocument>> config7 = facet =>
                {
                    facet.FieldExpression(q => q.Id).MethodType(FacetMethodType.UninvertedField);
                };
                const string expected7 = "facet=true&facet.field={!key=Id}id&f.id.facet.method=fc";

                Action<IFacetFieldParameter<TestDocument>> config8 = facet =>
                {
                    facet.FieldExpression(q => q.Id).MethodType(FacetMethodType.Stream);
                };
                const string expected8 = "facet=true&facet.field={!key=Id}id&f.id.facet.method=fcs";

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
                    new object[] { config6, expected6 },
                    new object[] { config7, expected7 },
                    new object[] { config8, expected8 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data1))]
        public void FacetFieldParameterTheory001(Action<IFacetFieldParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, null);
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

                Action<IFacetFieldParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.SortType(FacetSortType.IndexDesc);
                };

                Action<IFacetFieldParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.SortType(FacetSortType.CountDesc);
                };

                Action<IFacetFieldParameter<TestDocument>> config3 = facet =>
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
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Throws UnsupportedSortTypeException
        /// </summary>
        [Theory]
        [MemberData(nameof(Data2))]
        public void FacetFieldParameterTheory002(Action<IFacetFieldParameter<TestDocument>> config)
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            serviceProvider
                .Setup(q => q.GetService<SearchQuery<TestDocument>>())
                .Returns(new SearchQuery<TestDocument>(expressionBuilder));
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, serviceProvider.Object);
            config.Invoke(parameter);

            // Act / Assert
            Assert.Throws<UnsupportedFeatureException>(() => ((ISearchItemExecution<List<string>>)parameter).Execute());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetFieldParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetFieldParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
