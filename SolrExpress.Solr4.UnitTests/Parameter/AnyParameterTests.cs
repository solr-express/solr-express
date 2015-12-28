using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class AnyParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void AnyParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new AnyParameter("x", "y");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("x=y", container[0]);
        }
    }
}
