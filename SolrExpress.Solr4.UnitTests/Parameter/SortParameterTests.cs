using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class SortParameterTests
    {
        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void SortParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SortParameter<TestDocument>(q => q.Id, true);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("sort=Id asc", container[0]);
        }
    }
}
