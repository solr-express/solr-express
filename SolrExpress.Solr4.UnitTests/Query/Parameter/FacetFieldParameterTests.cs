using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
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
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field={!key=Id}Id", container[1]);
            Assert.AreEqual("f.Id.facet.mincount=1", container[2]);
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
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, SolrFacetSortType.CountAsc);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(4, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field={!key=Id}Id", container[1]);
            Assert.AreEqual("f.Id.facet.sort=count", container[2]);
            Assert.AreEqual("f.Id.facet.mincount=1", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort count desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnsupportedSortTypeException))]
        public void FacetFieldParameter003()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, SolrFacetSortType.CountDesc);

            // Act / Assert
            parameter.Execute(container);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort index desc
        /// What    Throws UnsupportedSortTypeException exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnsupportedSortTypeException))]
        public void FacetFieldParameter004()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, SolrFacetSortType.IndexDesc);

            // Act / Assert
            parameter.Execute(container);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetFieldParameter005()
        {
            // Arrange / Act / Assert
            var paramater = new FacetFieldParameter<TestDocument>();
            paramater.Configure(null);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the limit parameter
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter006()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, limit: 10);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(4, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field={!key=Id}Id", container[1]);
            Assert.AreEqual("f.Id.facet.mincount=1", container[2]);
            Assert.AreEqual("f.Id.facet.limit=10", container[3]);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter007()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("facet=true", container[0]);
            Assert.AreEqual("facet.field={!ex=tag1,tag2 key=Id}Id", container[1]);
            Assert.AreEqual("f.Id.facet.mincount=1", container[2]);
        }
    }
}
