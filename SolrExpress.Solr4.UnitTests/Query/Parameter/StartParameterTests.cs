using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    [TestClass]
    public class StartParameterTests
    {
        /// <summary>
        /// Where   Using a StartParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void StartParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new StartParameter();
            parameter.Configure(10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("start=10", container[0]);
        }
    }
}
