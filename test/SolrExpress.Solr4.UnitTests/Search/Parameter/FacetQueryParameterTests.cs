using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
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
    public class FacetQueryParameterTests
    {
        public static IEnumerable<object[]> Data1
        {
            get
            {
                var solrOptions = new SolrExpressOptions();
                var solrConnection = new FakeSolrConnection<TestDocument>();
                var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
                var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
                expressionBuilder.LoadDocument();

                Action<IFacetQueryParameter<TestDocument>> config1 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                };
                const string expected1 = "facet=true&facet.query=avg('Y')";

                Action<IFacetQueryParameter<TestDocument>> config2 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                };
                const string expected2 = "facet=true&facet.query={!key=X}avg('Y')";

                Action<IFacetQueryParameter<TestDocument>> config3 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                };
                const string expected3 = "facet=true&facet.query={!key=X}avg('Y')&f.X.facet.mincount=1";

                Action<IFacetQueryParameter<TestDocument>> config4 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                };
                const string expected4 = "facet=true&facet.query={!key=X}avg('Y')&f.X.facet.sort=count&f.X.facet.mincount=1";

                Action<IFacetQueryParameter<TestDocument>> config5 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                };
                const string expected5 = "facet=true&facet.query={!key=X}avg('Y')&f.X.facet.sort=count&f.X.facet.mincount=1&f.X.facet.limit=10";

                Action<IFacetQueryParameter<TestDocument>> config6 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                    facet.Excludes = new[] { "tag1", "tag2" };
                };
                const string expected6 = "facet=true&facet.query={!ex=tag1,tag2 key=X}avg('Y')&f.X.facet.sort=count&f.X.facet.mincount=1&f.X.facet.limit=10";

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
                    new object[] { config6, expected6 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data1))]
        public void FacetQueryParameterTheory001(Action<IFacetQueryParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IFacetQueryParameter<TestDocument>)new FacetQueryParameter<TestDocument>(null);
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
                var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

                Action<IFacetQueryParameter<TestDocument>> config1 = facet =>
                {
                    facet.Query = searchQuery.AddValue(".", false);
                    facet.SortType = FacetSortType.IndexDesc;
                };

                Action<IFacetQueryParameter<TestDocument>> config2 = facet =>
                {
                    facet.Query = searchQuery.AddValue(".", false);
                    facet.SortType = FacetSortType.CountDesc;
                };

                Action<IFacetQueryParameter<TestDocument>> config3 = facet =>
                {
                    facet.Query = searchQuery.AddValue(".", false);
                    facet.Filter = new SearchQuery<TestDocument>(expressionBuilder);
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
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Throws UnsupportedSortTypeException
        /// </summary>
        [Theory]
        [MemberData(nameof(Data2))]
        public void FacetQueryParameterTheory002(Action<IFacetQueryParameter<TestDocument>> config)
        {
            // Arrange
            var parameter = (IFacetQueryParameter<TestDocument>)new FacetQueryParameter<TestDocument>(null);
            config.Invoke(parameter);

            // Act / Assert
            Assert.Throws<UnsupportedFeatureException>(() => ((ISearchItemExecution<List<string>>)parameter).Execute());
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact(Skip = "Think about this, no implements ISearchItemFieldExpressions<> or ISearchItemFieldExpression<>")]
        public void FacetQueryParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetQueryParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
