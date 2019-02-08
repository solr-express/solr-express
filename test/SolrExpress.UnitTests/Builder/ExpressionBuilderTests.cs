using Moq;
using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.UnitTests.Builder
{
    public class ExpressionBuilderTests
    {
        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field without IsDynamic settings
        /// What    Returns original field name
        /// </summary>
        [Fact]
        public void ExpressionBuilder001()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": []
                }");
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.NoDynamic);

            // Assert
            Assert.Equal("no_dynamic", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic prefix and suffix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder002()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": []
                }");
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithPrefixAndSufix);

            // Assert
            Assert.Equal("prefix_dynamic_suffix", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic prefix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder003()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": []
                }");
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithPrefix);

            // Assert
            Assert.Equal("prefix_dynamic", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldName with a field with dynamic suffix settings
        /// What    Returns field name with prefix and suffix
        /// </summary>
        [Fact]
        public void ExpressionBuilder004()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": []
                }");
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadDocument();

            // Act
            var fieldName = expressionBuilder.GetFieldName(q => q.DynamicWithSuffix);

            // Assert
            Assert.Equal("dynamic_suffix", fieldName);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldSchema using a static field
        /// What    Get field settings using SOLR field API
        /// </summary>
        [Fact]
        public void ExpressionBuilder005()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": []
                }");

            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadSolrSchemaFields();

            // Act
            var fieldData = expressionBuilder.GetFieldSchema("id");

            // Assert
            Assert.True(fieldData.IsIndexed);
            Assert.True(fieldData.IsStored);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class
        /// When    Invoke GetFieldSchema using a dynamic field
        /// What    Get field settings using SOLR field API
        /// </summary>
        [Fact]
        public void ExpressionBuilder006()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection<TestDocumentDynamic>>();
            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/fields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
                ""fields"": [
                    {
                        ""name"": ""id"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""no_dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    },
                    {
                        ""name"": ""dynamic"",
                        ""indexed"": true,
                        ""stored"": true
                    }]
                }");

            solrConnection
                .Setup(q => q.Get(It.Is<string>(s => s.EndsWith("schema/dynamicfields")), It.IsAny<List<string>>()))
                .Returns(@"
                {
	                ""dynamicFields"": [{

                        ""name"": ""*_x"",
                        ""indexed"": true,
                        ""stored"": true

                    }]
                }");

            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentDynamic>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrDocumentConfiguration, solrConnection.Object);
            expressionBuilder.LoadSolrSchemaFields();

            // Act
            var fieldData = expressionBuilder.GetFieldSchema("id_x");

            // Assert
            Assert.True(fieldData.IsIndexed);
            Assert.True(fieldData.IsStored);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class using a document with SolrFieldAttribute
        /// When    Invoke GetFieldSchema using all fields in Document
        /// What    Get field settings using SOLR field API
        /// </summary>
        [Fact]
        public void ExpressionBuilder007()
        {
            // Arrange
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocumentAttributes>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentAttributes>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentAttributes>(solrOptions, solrDocumentConfiguration, solrConnection);

            expressionBuilder.LoadDocument();

            // Act
            var fielDataCreateAt = expressionBuilder.GetData(q => q.CreatedAt);
            var fielDataDummy = expressionBuilder.GetData(q => q.Dummy);
            var fielDataId = expressionBuilder.GetData(q => q.Id);
            var fielDataScore = expressionBuilder.GetData(q => q.Score);
            var fielDataSpatial = expressionBuilder.GetData(q => q.Spatial);

            // Assert
            Assert.NotNull(fielDataCreateAt);
            Assert.Equal("_created_at_", fielDataCreateAt.FieldSchema.FieldName);
            Assert.Equal(typeof(DateTime).Name, fielDataCreateAt.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataDummy);
            Assert.Equal("_dummy_", fielDataDummy.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataDummy.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataId);
            Assert.Equal("id", fielDataId.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataId.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataScore);
            Assert.Equal("score", fielDataScore.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataScore.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataSpatial);
            Assert.Equal("_spatial_", fielDataSpatial.FieldSchema.FieldName);
            Assert.Equal(typeof(GeoCoordinate).Name, fielDataSpatial.PropertyType.UnderlyingSystemType.Name);
        }

        /// <summary>
        /// Where   Using a ExpressionBuilder class using a document without SolrFieldAttribute
        /// When    Invoke GetFieldSchema using all fields in Document
        /// What    Get field settings using SOLR field API
        /// </summary>
        [Fact]
        public void ExpressionBuilder008()
        {
            // Arrange
            var fieldNames = new List<string>
            {
                "id",
                "_created_at_",
                "_dummy_",
                "_spatial_"
            };

            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeNoAttributeSolrConnection<TestDocumentNoAttributes>(fieldNames);
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocumentNoAttributes>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentNoAttributes>(solrOptions, solrDocumentConfiguration, solrConnection);

            solrDocumentConfiguration.Field(t => t.CreatedAt).HasName("_created_at_");
            solrDocumentConfiguration.Field(t => t.Dummy).HasName("_dummy_");
            solrDocumentConfiguration.Field(t => t.Spatial).HasName("_spatial_");

            expressionBuilder.LoadDocument();

            // Act
            var fielDataCreateAt = expressionBuilder.GetData(q => q.CreatedAt);
            var fielDataDummy = expressionBuilder.GetData(q => q.Dummy);
            var fielDataId = expressionBuilder.GetData(q => q.Id);
            var fielDataScore = expressionBuilder.GetData(q => q.Score);
            var fielDataSpatial = expressionBuilder.GetData(q => q.Spatial);

            // Assert
            Assert.NotNull(fielDataCreateAt);
            Assert.Equal("_created_at_", fielDataCreateAt.FieldSchema.FieldName);
            Assert.Equal(typeof(DateTime).Name, fielDataCreateAt.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataDummy);
            Assert.Equal("_dummy_", fielDataDummy.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataDummy.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataId);
            Assert.Equal("id", fielDataId.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataId.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataScore);
            Assert.Equal("score", fielDataScore.FieldSchema.FieldName);
            Assert.Equal(typeof(string).Name, fielDataScore.PropertyType.UnderlyingSystemType.Name);

            Assert.NotNull(fielDataSpatial);
            Assert.Equal("_spatial_", fielDataSpatial.FieldSchema.FieldName);
            Assert.Equal(typeof(GeoCoordinate).Name, fielDataSpatial.PropertyType.UnderlyingSystemType.Name);
        }
    }
}
