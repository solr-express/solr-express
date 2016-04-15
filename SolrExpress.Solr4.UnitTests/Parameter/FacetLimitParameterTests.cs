using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class FacetLimitParameterTests
    {
        /// <summary>
        /// Where   Using a FacetLimitParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetLimitParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetLimitParameter();
            parameter.Configure(10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("facet.limit=10", container[0]);
        }
    }
}
