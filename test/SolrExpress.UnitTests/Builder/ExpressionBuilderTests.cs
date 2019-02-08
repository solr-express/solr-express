using Moq;
using SolrExpress.Builder;
using System.Collections.Generic;
using SolrExpress.Options;
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
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
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
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
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
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
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
            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
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

            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
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

            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);
            expressionBuilder.LoadSolrSchemaFields();

            // Act
            var fieldData = expressionBuilder.GetFieldSchema("id_x");

            // Assert
            Assert.True(fieldData.IsIndexed);
            Assert.True(fieldData.IsStored);
        }
    }
}
