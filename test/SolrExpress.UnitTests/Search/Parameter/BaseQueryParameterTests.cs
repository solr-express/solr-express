﻿using Moq;
using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using Xunit;

namespace SolrExpress.UnitTests.Search.Parameter
{
    public class BaseQueryParameterTests
    {
        /// <summary>
        /// Where   Using a BaseQueryParameter instance
        /// When    Invoking method "Equals" using same instance
        /// What    Returns true
        /// </summary>
        [Fact]
        public void BaseQueryParameter001()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);

            var parameter1 = new Mock<BaseQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = searchQuery.AddField(q => q.Id);

            // Act
            var result = parameter1.Equals(parameter1);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryParameter instance
        /// When    Invoking method "Equals" using an instance with different values
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryParameter002()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();

            var parameter1 = new Mock<BaseQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter1.Value = new SearchQuery<TestDocument>(expressionBuilder).AddField(q => q.Id);

            var parameter2 = new Mock<BaseQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;
            parameter2.Value = new SearchQuery<TestDocument>(expressionBuilder).AddField(q => q.Dummy);

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Where   Using a BaseQueryParameter instance
        /// When    Invoking method "Equals" using an instance with different type
        /// What    Returns false
        /// </summary>
        [Fact]
        public void BaseQueryParameter003()
        {
            // Arrange
            var parameter1 = new Mock<BaseQueryParameter<TestDocument>>
            {
                CallBase = true
            }.Object;

            var parameter2 = new FakeSearchParameter();

            // Act
            var result = parameter1.Equals(parameter2);

            // Assert
            Assert.False(result);
        }
    }
}
