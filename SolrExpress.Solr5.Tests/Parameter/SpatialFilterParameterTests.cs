using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.Tests.Parameter
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
                fq:""{!geofilt sfield=Spatial}"",
                pt:""-1.1,-2.2"",
                d:""5.5""
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
                fq:""{!bbox sfield=Spatial}"",
                pt:""-1.1,-2.2"",
                d:""5.5""
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
    }
}
