using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Core.Query;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.Tests.Parameter
{
    [TestClass]
    public class SpatialArbitraryFilterParameterTests
    {
        /// <summary>
        /// Where   Using a SpatialArbitraryFilterParameter instance
        /// When    Invoking the method "Execute" using arbitrary rectangle annotation
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void SpatialArbitraryFilterParameter001()
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
            var parameter = new SpatialArbitraryFilterParameter(new RangeValue<TestDocument, GeoCoordinate>(q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), new GeoCoordinate(5.5M, 6.6M)));

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
