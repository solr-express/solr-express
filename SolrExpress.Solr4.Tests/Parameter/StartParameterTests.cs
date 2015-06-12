using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.Tests.Parameter
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
            var parameter = new StartParameter(10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("start=10", container[0]);
        }
    }
}
