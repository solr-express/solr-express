using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using SolrExpress.Options;
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
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Geofilt).CenterPoint(new GeoCoordinate(-1.23456789M, -2.234567891M)).Distance(5.5M);
                };
                var expected1 = "fq={!geofilt sfield=_spatial_ pt=-1.23456789,-2.234567891 d=5.5}";

                Action<ISpatialFilterParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Spatial).FunctionType(SpatialFunctionType.Bbox).CenterPoint(new GeoCoordinate(-1.23456789M, -2.234567891M)).Distance(5.5M);
                };
                var expected2 = "fq={!bbox sfield=_spatial_ pt=-1.23456789,-2.234567891 d=5.5}";
                
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
        public void SpatialFilterParameterTheory001(Action<ISpatialFilterParameter<TestDocument>> config, string expectd)
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
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
