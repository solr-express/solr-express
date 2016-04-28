using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    [TestClass]
    public class RowsParameterTests
    {
        /// <summary>
        /// Where   Using a RowsParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void RowsParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new RowsParameter();
            parameter.Configure(10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("rows=10", container[0]);
        }
    }
}
