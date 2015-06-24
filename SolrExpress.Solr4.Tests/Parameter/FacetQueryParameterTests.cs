using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class FacetQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter("X", new FreeValue("avg('Y')"));

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.query={!ex=dt key=X}avg('Y')", container[1]);
            Assert.AreEqual("f.X.facet.mincount=1", container[2]);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter("X", new FreeValue("avg('Y')"), SolrFacetSortType.CountDesc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(4, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.query={!ex=dt key=X}avg('Y')", container[1]);
            Assert.AreEqual("f.X.facet.sort=count", container[2]);
            Assert.AreEqual("f.X.facet.mincount=1", container[3]);
        }

    }
}
