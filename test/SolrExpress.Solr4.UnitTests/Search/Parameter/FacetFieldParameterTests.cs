using SolrExpress.Builder;
using SolrExpress.Extension;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr4.Search.Parameter;
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
                var expected1 = "facet=true&facet.field={!key=Id}_id_";

                Action<IFacetFieldParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1);
                };
                var expected2 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.mincount=1";

                Action<IFacetFieldParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc);
                };
                var expected3 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.sort=count&f._id_.facet.mincount=1";

                Action<IFacetFieldParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                var expected4 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.sort=count&f._id_.facet.mincount=1&f._id_.facet.limit=10";

                Action<IFacetFieldParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                var expected5 = "facet=true&facet.field={!ex=tag1,tag2 key=Id}_id_&f._id_.facet.sort=count&f._id_.facet.mincount=1&f._id_.facet.limit=10";

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
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
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            expressionBuilder.LoadDocument();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
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
                var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
                expressionBuilder.LoadDocument();
                var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

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

                return new[]
                {
                    new object[] { config1 },
                    new object[] { config2 },
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
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            expressionBuilder.LoadDocument();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act / Assert
            Assert.Throws<UnsupportedSortTypeException>(() => ((ISearchItemExecution<List<string>>)parameter).Execute());
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
