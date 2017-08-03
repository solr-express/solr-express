using SolrExpress.Builder;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using Xunit;

namespace SolrExpress.UnitTests.Search.Query
{
    public class SearchQueryTests
    {
        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a string value
        /// What    Create query with correct value
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
            var result = searchQuery.AddValue("some value").Execute();

            // Assert
            Assert.Equal("\"some value\"", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a int value
        /// What    Create query with correct value
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
            var result = searchQuery.AddValue(1).Execute();

            // Assert
            Assert.Equal("1", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a decimal value
        /// What    Create query with correct value
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
            var result = searchQuery.AddValue((decimal)10.12345).Execute();

            // Assert
            Assert.Equal("10.12345", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a double value
        /// What    Create query with correct value
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
            var result = searchQuery.AddValue(10.12345).Execute();

            // Assert
            Assert.Equal("10.12345", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a float value
        /// What    Create query with correct value
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
            var result = searchQuery.AddValue((float)10.12345).Execute();

            // Assert
            Assert.Equal("10.12345", result);
        }

        /// <summary>
        /// Where   Using a SearchQuery instance
        /// When    Invoking method "AddValue" using a DateTime value
        /// What    Create query with correct value
        /// </summary>
        [Fact]
        public void SearchQueryFact006()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TechProductDocument>();
            var expressionBuilder = new ExpressionBuilder<TechProductDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TechProductDocument>(expressionBuilder);

            // Act
            var result = searchQuery.AddValue(new DateTime(1984, 09, 05, 10, 20, 30)).Execute();

            // Assert
            Assert.Equal("1984-09-05T10:20:30Z", result);
        }
    }
}
