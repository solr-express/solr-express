using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Extension;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
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
            var parameter = (IQueryFieldParameter<TestDocument>)new QueryFieldParameter<TestDocument>();
            parameter.Expression("id^10 score~2^20");

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("qf=id^10 score~2^20", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void QueryFieldParameter002()
        {
            //// Arrange
            //var parameter = new QueryFieldParameter<TestDocument>();

            //// Act / Assert
            //Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
