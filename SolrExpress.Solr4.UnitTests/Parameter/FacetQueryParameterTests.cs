using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Exception;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr4.Parameter;
using System;
using System.Collections.Generic;

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
            Assert.AreEqual("facet.query={!key=X}avg('Y')", container[1]);
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
            var parameter = new FacetQueryParameter("X", new FreeValue("avg('Y')"), SolrFacetSortType.CountAsc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(4, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.query={!key=X}avg('Y')", container[1]);
            Assert.AreEqual("f.X.facet.sort=count", container[2]);
            Assert.AreEqual("f.X.facet.mincount=1", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnsupportedSortTypeException))]
        public void FacetQueryParameter003()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter("X", new QueryAll(), SolrFacetSortType.CountDesc);

            // Act / Assert
            parameter.Execute(container);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnsupportedSortTypeException))]
        public void FacetQueryParameter004()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetQueryParameter("X", new QueryAll(), SolrFacetSortType.IndexDesc);

            // Act / Assert
            parameter.Execute(container);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetQueryParameter005()
        {
            // Arrange / Act / Assert
            new FacetQueryParameter(null, new FreeValue("x"));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in expression
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetQueryParameter006()
        {
            // Arrange / Act / Assert
            new FacetQueryParameter("x", null);
        }
    }
}
