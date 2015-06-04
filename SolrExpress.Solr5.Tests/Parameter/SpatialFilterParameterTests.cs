using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Parameter;
using SolrExpress.Solr5.ParameterValue;

namespace SolrExpress.Solr5.Tests.Parameter
{
    [TestClass]
    public class SpatialSpatialFilterParameterTests
    {
        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using arbitrary rectangle annotation
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void SpatialFilterParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                fq:""Spatial:[-1.1,-2.2 TO 5.5,6.6]""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new SpatialFilterParameter(new RangeValue<TestDocument, GeoCoordinate>(q => q.Spatial, new GeoCoordinate(-1.1, -2.2), new GeoCoordinate(5.5, 6.6)));

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
