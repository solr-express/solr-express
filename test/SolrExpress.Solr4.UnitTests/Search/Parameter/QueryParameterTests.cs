using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class QueryParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void QueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IQueryParameter<TestDocument>)new QueryParameter<TestDocument>();
            //parameter.Value(q => q.Id);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("q=_id_:ITEM01", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void QueryParameter002()
        {
            //// Arrange
            //var expressionCache = new ExpressionCache<TestDocument>();
            //var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            //var parameter = new QueryParameter<TestDocument>(expressionBuilder);

            //// Act / Assert
            //Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
