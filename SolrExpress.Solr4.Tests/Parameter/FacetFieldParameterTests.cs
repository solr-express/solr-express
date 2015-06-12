using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Enumerator;
using SolrExpress.Solr4.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Tests.Parameter
{
    [TestClass]
    public class FacetFieldParameterTests
    {
        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(2, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field=Id", container[1]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.CountDesc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field=Id", container[1]);
            Assert.AreEqual("f.Id.facet.sort=count", container[2]);
        }
    }
}
