using Xunit;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class QueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void QueryFieldParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new QueryFieldParameter();
            parameter.Configure("id^10 score~2^20");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("qf=id^10 score~2^20", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void QueryFieldParameter002()
        {
            // Arrange
            var parameter = new QueryFieldParameter();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
