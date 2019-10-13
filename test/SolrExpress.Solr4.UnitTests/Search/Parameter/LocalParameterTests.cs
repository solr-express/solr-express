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
    public class LocalParameterTests
    {
        /// <summary>
        /// Where   Using a LocalParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void LocalParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new LocalParameter<TestDocument>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            parameter.Name = "myLocalParameter";
            parameter.Query = searchQuery.Field(q => q.Id).EqualsTo("ITEM01");

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("myLocalParameter=id:\"ITEM01\"", container[0]);
        }

        /// <summary>
        /// Where   Using a LocalParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void LocalParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new LocalParameter<TestDocument>();
            parameter.Name = "myLocalParameter";
            parameter.Value = "some value";

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("myLocalParameter=some value", container[0]);
        }
    }
}
