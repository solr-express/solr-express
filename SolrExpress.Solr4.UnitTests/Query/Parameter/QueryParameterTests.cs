using Xunit;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class QueryParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void QueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new QueryParameter<TestDocument>();
            parameter.Configure(new Single<TestDocument>(q => q.Id, "ITEM01"));

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("q=Id:ITEM01", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void QueryParameter002()
        {
            // Arrange
            var parameter = new QueryParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
