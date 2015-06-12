using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Query;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.Tests.Parameter
{
    [TestClass]
    public class SpatialFilterParameterTests
    {
        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using geofilt function
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void SpatialFilterParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Geofilt, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("fq={!geofilt sfield=Spatial}", container[0]);
            Assert.AreEqual("pt=-1.1,-2.2", container[1]);
            Assert.AreEqual("d=5.5", container[2]);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using bbox function
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void SpatialFilterParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Bbox, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(3, container.Count);
            Assert.AreEqual("fq={!bbox sfield=Spatial}", container[0]);
            Assert.AreEqual("pt=-1.1,-2.2", container[1]);
            Assert.AreEqual("d=5.5", container[2]);
        }
    }
}
