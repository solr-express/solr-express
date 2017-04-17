using SolrExpress.Builder;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Solr4.Search.Parameter;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class SpatialFilterParameterTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                Action<ISpatialFilterParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Geofilt).CenterPoint(new GeoCoordinate(-1.1M, -2.2M)).Distance(5.5M);
                };
                var expected1 = "fq={!geofilt sfield=_spatial_ pt=-1.1,-2.2 d=5.5}";

                Action<ISpatialFilterParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Bbox).CenterPoint(new GeoCoordinate(-1.1M, -2.2M)).Distance(5.5M);
                };
                var expected2 = "fq={!bbox sfield=_spatial_ pt=-1.1,-2.2 d=5.5}";
                
                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                };
            }
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void SpatialFilterParameterTheory001(Action<ISpatialFilterParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            var parameter = (ISpatialFilterParameter<TestDocument>)new SpatialFilterParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            var actual = string.Join("&", container);

            Assert.Equal(expectd, actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void SpatialFilterParameter003()
        {
            //// Arrange
            //bool actual;
            //string dummy;
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.NotIndexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

            //// Act
            //parameter.Validate(out actual, out dummy);

            //// Assert
            //Assert.False(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void SpatialFilterParameter004()
        {
            //// Arrange
            //bool actual;
            //string dummy;
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.Indexed, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(), 0);

            //// Act
            //parameter.Validate(out actual, out dummy);

            //// Assert
            //Assert.True(actual);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void SpatialFilterParameter005()
        {
            //// Arrange
            //var expressionCache = new ExpressionCache<TestDocument>();
            //var expressionBuilder = (IExpressionBuilder<TestDocument>)new ExpressionBuilder<TestDocument>(expressionCache);
            //var parameter = new SpatialFilterParameter<TestDocument>(expressionBuilder);

            //// Act / Assert
            //Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 10));
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Validate" using field Indexed=true
        /// What    Valid is true
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void SpatialFilterParameter006()
        {
            //// Arrange
            //bool isValid;
            //string errorMessage;
            //var container = new List<string>();
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.Indexed, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 0);

            //// Act
            //parameter.Validate(out isValid, out errorMessage);

            //// Assert
            //Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking the method "Validate" using field Indexed=false
        /// What    Valid is true
        /// </summary>
        [Fact(Skip = "Needs review in validation logic")]
        public void SpatialFilterParameter007()
        {
            //// Arrange
            //bool isValid;
            //string errorMessage;
            //var container = new List<string>();
            //var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            //var expressionBuilder = (IExpressionBuilder<TestDocumentWithAttribute>)new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            //var parameter = new SpatialFilterParameter<TestDocumentWithAttribute>(expressionBuilder);
            //parameter.Configure(q => q.NotIndexed, SolrSpatialFunctionType.Bbox, new GeoCoordinate(), 0);

            //// Act
            //parameter.Validate(out isValid, out errorMessage);

            //// Assert
            //Assert.False(isValid);
            //Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInAQueryException, errorMessage);
        }
    }
}
