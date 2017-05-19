using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class SpatialFilterParameterTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                Action<ISpatialFilterParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Geofilt).CenterPoint(new GeoCoordinate(-1.23456789M, -2.234567891M)).Distance(5.5M);
                };
                var expected1 = JObject.Parse(@"
                {
                  params:{
                    fq:""{!geofilt sfield=_spatial_ pt=-1.23456789,-2.234567891 d=5.5}"",
                  }
                }");

                Action<ISpatialFilterParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Bbox).CenterPoint(new GeoCoordinate(-1.23456789M, -2.234567891M)).Distance(5.5M);
                };
                var expected2 = JObject.Parse(@"
                {
                  params:{
                    fq:""{!bbox sfield=_spatial_ pt=-1.23456789,-2.234567891 d=5.5}"",
                  }
                }");

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                };
            }
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void SpatialFilterParameterTheory001(Action<ISpatialFilterParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            expressionBuilder.LoadDocument();
            var parameter = (ISpatialFilterParameter<TestDocument>)new SpatialFilterParameter<TestDocument>(expressionBuilder);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expectd.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a SpatialFilterParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void SpatialFilterParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(SpatialFilterParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
