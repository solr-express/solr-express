using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class QueryAllTests
    {
        /// <summary>
        /// Where   Using a QueryAll instance
        /// When    Create the instance
        /// What    Get "*:*" value
        /// </summary>
        [TestMethod]
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
