//using Xunit;
//using SolrExpress.Core;
//using SolrExpress.Core.Search.ParameterValue;
//using SolrExpress.Solr4.Search.Parameter;
//using System;
//using System.Collections.Generic;
//using SolrExpress.Core.Utility;
//using Moq;

//namespace SolrExpress.Solr4.UnitTests.Search.Parameter
//{
//    public class SpatialFilterParameterTests
//    {
//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Invoking the method "Execute" using geofilt function
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter001()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocument>(expressionBuilder);
//            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

//            // Act
//            parameter.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("fq={!geofilt sfield=_spatial_ pt=-1.1,-2.2 d=5.5}", container[0]);
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Invoking the method "Execute" using bbox function
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter002()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocument>(expressionBuilder);
//            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Bbox, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

//            // Act
//            parameter.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("fq={!bbox sfield=_spatial_ pt=-1.1,-2.2 d=5.5}", container[0]);
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
//        /// What    Returns valid=false
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter003()
//        {
//            // Arrange
//            bool actual;
//            string dummy;
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.NotIndexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

//            // Act
//            parameter.Validate(out actual, out dummy);

//            // Assert
//            Assert.False(actual);
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
//        /// What    Returns valid=true
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter004()
//        {
//            // Arrange
//            bool actual;
//            string dummy;
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.Indexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

//            // Act
//            parameter.Validate(out actual, out dummy);

//            // Assert
//            Assert.True(actual);
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Create the instance with null
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter005()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 10));
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Invoking the method "Validate" using field Indexed=true
//        /// What    Valid is true
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter006()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.Indexed, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 0);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.True(isValid);
//        }

//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Invoking the method "Validate" using field Indexed=false
//        /// What    Valid is true
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter007()
//        {
//            // Arrange
//            bool isValid;
//            string errorMessage;
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
//            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
//            parameter.Configure(q => q.NotIndexed, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 0);

//            // Act
//            parameter.Validate(out isValid, out errorMessage);

//            // Assert
//            Assert.False(isValid);
//            Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException, errorMessage);
//        }
        
//        /// <summary>
//        /// Where   Using a SpatialFilterParameter instance
//        /// When    Invoking the method "Execute" using same configurations that in issue #148
//        /// What    Create a valid JSON
//        /// </summary>
//        [Fact]
//        public void SpatialFilterParameter008()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new SpatialFilterParameter<TestDocument>(expressionBuilder);
//            parameter.Configure(q => q.Spatial, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(52.9127M, 4.7818799M), 1M);

//            // Act
//            parameter.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("fq={!geofilt sfield=_spatial_ pt=52.9127,4.7818799 d=1}", container[0]);
//        }
//    }
//}
