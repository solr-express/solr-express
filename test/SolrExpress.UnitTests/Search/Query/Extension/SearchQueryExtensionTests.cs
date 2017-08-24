using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search.Query;
using SolrExpress.Search.Query.Extension;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.UnitTests.Search.Query.Extension
{
    public class SearchQueryExtensionTests
    {
        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "All" using values 10, 20, 30
        /// What    Create query (10 AND 20 AND 30)
        /// </summary>
        [Fact]
        public void SearchQueryFact001()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.All(10, 20, 30).Execute();

            // Assert
            Assert.Equal("(10 AND 20 AND 30)", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "Any" using values 10, 20, 30
        /// What    Create query (10 OR 20 OR 30)
        /// </summary>
        [Fact]
        public void SearchQueryFact002()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.Any(10, 20, 30).Execute();

            // Assert
            Assert.Equal("(10 OR 20 OR 30)", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "StartsWith" using value 10
        /// What    Create query "/xpto.*/"
        /// </summary>
        [Fact]
        public void SearchQueryFact003()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.StartsWith("xpto").Execute();

            // Assert
            Assert.Equal("\"/xpto.*/\"", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "EqualsTo" using value 10
        /// What    Create query 10
        /// </summary>
        [Fact]
        public void SearchQueryFact004()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.EqualsTo(10).Execute();

            // Assert
            Assert.Equal("10", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "NotEqualsTo" using value 10
        /// What    Create query -(10)
        /// </summary>
        [Fact]
        public void SearchQueryFact005()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.NotEqualsTo(10).Execute();

            // Assert
            Assert.Equal("-(10)", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "Field" using value q => q.Id
        /// What    Create query id:
        /// </summary>
        [Fact]
        public void SearchQueryFact006()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

            // Act
            var result = searchQuery.Field(q => q.Id).Execute();

            // Assert
            Assert.Equal("id", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "Or" using value q => q.EqualsTo(10)
        /// What    Create query OR (10)
        /// </summary>
        [Fact]
        public void SearchQueryFact007()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.Or(q => q.EqualsTo(10)).Execute();

            // Assert
            Assert.Equal(" OR (10)", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "And" using value q => q.EqualsTo(10)
        /// What    Create query AND (10)
        /// </summary>
        [Fact]
        public void SearchQueryFact008()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.And(q => q.EqualsTo(10)).Execute();

            // Assert
            Assert.Equal(" AND (10)", result);

        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking method "Not" using value q => q.EqualsTo(10)
        /// What    Create query -(10)
        /// </summary>
        [Fact]
        public void SearchQueryFact009()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.Not(q => q.EqualsTo(10)).Execute();

            // Assert
            Assert.Equal("-(10)", result);

        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking chain methods, creating a complex query
        /// What    Create query id:10 OR (name:("nam#1" OR "nam#2" OR "nam#3"))
        /// </summary>
        [Fact]
        public void SearchQueryFact010()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery
                .Field(f => f.Id).EqualsTo(10)
                .Or(q =>
                    q.Field(f => f.Name).Any("name#1", "name#2", "name#3"))
                .Execute();

            // Assert
            Assert.Equal("id:10 OR (name:(\"name#1\" OR \"name#2\" OR \"name#3\"))", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance and SearchQueryExtension methods
        /// When    Invoking chain methods, creating a complex query
        /// What    Create query id:10 OR (name:("nam#1" OR "nam#2" OR "nam#3"))
        /// </summary>
        [Fact]
        public void SearchQueryFact011()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery
                .Group(g => g
                    .Field(f => f.Id).EqualsTo(10)
                    .And(q =>
                        q.Field(f => f.Popularity).InRange(10, 20))
                )
                .Or(q =>
                    q.Field(f => f.Name).Any("name#1", "name#2", "name#3"))
                .Execute();

            // Assert
            Assert.Equal("(id:10 AND (popularity:[10 TO 20])) OR (name:(\"name#1\" OR \"name#2\" OR \"name#3\"))", result);
        }
    }
}
