using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetQueryParameterTests
    {
        public static IEnumerable<object[]> Data1
        {
            get
            {
                var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
                expressionBuilder.LoadDocument();

                Action<IFacetQueryParameter<TestDocument>> config1 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                };
                var expected1 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config2 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                };
                var expected2 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config3 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                };
                var expected3 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config4 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                };
                var expected4 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config5 = facet =>
                {
                    facet.AliasName = "X";
                    facet.SortType = FacetSortType.IndexAsc;
                };
                var expected5 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config6 = facet =>
                {
                    facet.AliasName = "X";
                    facet.SortType = FacetSortType.CountDesc;
                };
                var expected6 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config7 = facet =>
                {
                    facet.AliasName = "X";
                    facet.SortType = FacetSortType.IndexDesc;
                };
                var expected7 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config8 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                };
                var expected8 = JObject.Parse("{}");

                Action<IFacetQueryParameter<TestDocument>> config9 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                    facet.Excludes = new string[] { "tag1", "tag2" };
                };
                var expected9 = JObject.Parse("{}");

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
        public void FacetQueryParameterTheory001(Action<IFacetQueryParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var parameter = (IFacetQueryParameter<TestDocument>)new FacetQueryParameter<TestDocument>();
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expectd.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
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
