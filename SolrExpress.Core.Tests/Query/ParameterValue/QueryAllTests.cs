using Xunit;
using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Core.Tests.Query.ParameterValue
{
    public class QueryAllTests
    {
        /// <summary>
        /// Where   Using a QueryAll instance
        /// When    Create the instance
        /// What    Get "*:*" value
        /// </summary>
        [Fact]
        public void QueryAll001()
        {
            // Arrange
            var expected = "*:*";
            string actual;
            var parameter = new QueryAll();

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
