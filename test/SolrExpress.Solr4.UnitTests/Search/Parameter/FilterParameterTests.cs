﻿using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search.Query;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FilterParameterTests
    {
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter1 = new FilterParameter<TestDocument>();
            parameter1.Query = new SearchQuery<TestDocument>(expressionBuilder).Field(q => q.Id).AddValue("X");
            var parameter2 = new FilterParameter<TestDocument>();
            parameter2.Query = new SearchQuery<TestDocument>(expressionBuilder).Field(q => q.Score).AddValue("Y");

            // Act
            parameter1.Execute();
            parameter1.AddResultInContainer(container);
            parameter2.Execute();
            parameter2.AddResultInContainer(container);

            // Assert
            Assert.Equal(2, container.Count);
            Assert.Equal("fq=id:\"X\"", container[0]);
            Assert.Equal("fq=score:\"Y\"", container[1]);
        }
        
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking method "Execute" using tag name
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter002()
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = new FilterParameter<TestDocument>();
            parameter.Query = searchQuery.Field(q => q.Id).AddValue("X");
            parameter.TagName = "tag1";

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("fq={!tag=tag1}id:\"X\"", container[0]);
        }
    }
}
