﻿using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Search.Query.Extension;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class StandardQueryParameterTests
    {
        /// <summary>
        /// Where   Using a StandardQueryParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void StandardQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IStandardQueryParameter<TestDocument>)new StandardQueryParameter<TestDocument>();
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrExpressOptions);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            parameter.Value = searchQuery.Field(q => q.Id).EqualsTo("ITEM01");

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("q.alt=id:\"ITEM01\"", container[0]);
        }
    }
}