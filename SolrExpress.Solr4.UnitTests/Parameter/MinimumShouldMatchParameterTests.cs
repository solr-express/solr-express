using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new MinimumShouldMatchParameter("75%");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("mm=75%", container[0]);
        }
    }
}
