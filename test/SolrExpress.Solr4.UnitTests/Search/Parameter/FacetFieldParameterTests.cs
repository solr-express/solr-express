using SolrExpress.Extension;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FacetFieldParameterTests
    {
        public static IEnumerable<object[]> Data
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
                var expected3 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.mincount=1&f._id_.facet.sort=count";

                Action<IFacetFieldParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                var expected4 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.mincount=1&f._id_.facet.sort=count&f._id_.facet.limit=10";

                Action<IFacetFieldParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                var expected5 = "facet=true&facet.field={!key=Id}_id_&f._id_.facet.mincount=1&f._id_.facet.sort=count&f._id_.facet.limit=10&facet.field={!ex=tag1,tag2 key=Id}_id_";

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
        /// When    Invoking the method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void FacetFieldParameterTheory001(Action<IFacetFieldParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            var actual = string.Join("&", container);

            Assert.Equal(expectd, actual);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=true
        /// What    Valid is true
        /// </summary>
        [Fact(Skip = "Need review validation logic")]
        public void FacetFieldParameter001()
        {
            //TODO: Need review validation logic

            //// Arrange
            //bool isValid;
            //string errorMessage;
            //var container = new List<string>();
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new FacetFieldParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.Indexed);

            //// Act
            //parameter.Validate(out isValid, out errorMessage);

            //// Assert
            //Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=false
        /// What    Valid is true
        /// </summary>
        [Fact(Skip = "Need review validation logic")]
        public void FacetFieldParameter002()
        {
            //TODO: Need review validation logic

            //// Arrange
            //bool isValid;
            //string errorMessage;
            //var container = new List<string>();
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new FacetFieldParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.NotIndexed);

            //// Act
            //parameter.Validate(out isValid, out errorMessage);

            //// Assert
            //Assert.False(isValid);
            //Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException, errorMessage);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact(Skip = "Need create exception")]
        public void FacetFieldParameter003()
        {
            // Arrange
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpression(q => q.Id).SortType(FacetSortType.CountDesc);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            
            // Assert
            // TODO: Need create exception
            //Assert.Throws<UnsupportedSortTypeException>(() => ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [Fact(Skip = "Need create exception")]
        public void FacetFieldParameter004()
        {
            // Arrange
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpression(q => q.Id).SortType(FacetSortType.IndexDesc);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();

            // Assert
            // TODO: Need create exception
            //Assert.Throws<UnsupportedSortTypeException>(() => ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetFieldParameter005()
        {
            // Arrange
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>();
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();

            // Assert
            Assert.Throws<ArgumentNullException>(() => ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container));
        }
    }
}
