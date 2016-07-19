using Xunit;
using SolrExpress.Core;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class SpatialFilterParameterTests
    {
        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using geofilt function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SpatialFilterParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SpatialFilterParameter<TestDocument>();
            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fq={!geofilt sfield=_spatial_ pt=-1.1,-2.2 d=5.5}", container[0]);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using bbox function
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SpatialFilterParameter002()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new SpatialFilterParameter<TestDocument>();
            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Bbox, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fq={!bbox sfield=_spatial_ pt=-1.1,-2.2 d=5.5}", container[0]);
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
