using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr5.Query.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class SpatialFilterParameterTests
    {
        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using geofilt function
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
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
            var parameter = new SpatialFilterParameter<TestDocument>();
            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using bbox function
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
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
            var parameter = new SpatialFilterParameter<TestDocument>();
            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Bbox, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact]
        public void SpatialFilterParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotIndexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.False(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [Fact]
        public void SpatialFilterParameter004()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Indexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.True(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SpatialFilterParameter005()
        {
            // Arrange
            var parameter = new SpatialFilterParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 10));
        }
    }
}
