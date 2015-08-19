using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Solr5.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Parameter
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
            var expected = JObject.Parse(@"
            {
              params:{
                fq:""{!geofilt sfield=Spatial pt=-1.1,-2.2 d=5.5}"",
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Geofilt, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
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
            var expected = JObject.Parse(@"
            {
              params:{
                fq:""{!bbox sfield=Spatial pt=-1.1,-2.2 d=5.5}""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Bbox, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void SpatialFilterParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(SolrSpatialFunctionType.Geofilt, q => q.NotIndexed, new GeoCoordinate(), 0);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [TestMethod]
        public void SpatialFilterParameter004()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(SolrSpatialFunctionType.Geofilt, q => q.Indexed, new GeoCoordinate(), 0);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SpatialFilterParameter005()
        {
            // Arrange / Act / Assert
            new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Bbox, null, new GeoCoordinate(), 10);
        }
    }
}
