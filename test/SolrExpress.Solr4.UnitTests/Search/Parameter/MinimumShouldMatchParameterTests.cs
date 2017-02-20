using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IMinimumShouldMatchParameter<TestDocument>)new MinimumShouldMatchParameter<TestDocument>();
            parameter.Value = "75%";

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("mm=75%", container[0]);
        }

        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void MinimumShouldMatchParameter002()
        {
            //// Arrange
            //var parameter = new MinimumShouldMatchParameter<TestDocument>();

            //// Act / Assert
            //Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
