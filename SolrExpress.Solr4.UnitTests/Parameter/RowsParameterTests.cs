using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
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
            var parameter = new RowsParameter(10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("rows=10", container[0]);
        }
    }
}
