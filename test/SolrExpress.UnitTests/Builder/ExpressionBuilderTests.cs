using Moq;
using SolrExpress.Builder;
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
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection
                .Setup(q => q.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(@"
                {
                    ""responseHeader"": {
                    ""status"": 0,
                    ""QTime"": 118
                    },
                    ""field"": {
                    ""name"": ""no_dynamic"",
                    ""type"": ""string"",
                    ""multiValued"": false,
                    ""indexed"": true,
                    ""required"": true,
                    ""stored"": true
                    }
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
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection
                .Setup(q => q.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(@"
                {
                    ""responseHeader"": {
                    ""status"": 0,
                    ""QTime"": 118
                    },
                    ""field"": {
                    ""name"": ""dynamic"",
                    ""type"": ""string"",
                    ""multiValued"": false,
                    ""indexed"": true,
                    ""required"": true,
                    ""stored"": true
                    }
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
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection
                .Setup(q => q.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(@"
                {
                    ""responseHeader"": {
                    ""status"": 0,
                    ""QTime"": 118
                    },
                    ""field"": {
                    ""name"": ""dynamic"",
                    ""type"": ""string"",
                    ""multiValued"": false,
                    ""indexed"": true,
                    ""required"": true,
                    ""stored"": true
                    }
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
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection
                .Setup(q => q.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(@"
                {
                    ""responseHeader"": {
                    ""status"": 0,
                    ""QTime"": 118
                    },
                    ""field"": {
                    ""name"": ""dynamic"",
                    ""type"": ""string"",
                    ""multiValued"": false,
                    ""indexed"": true,
                    ""required"": true,
                    ""stored"": true
                    }
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
        /// When    Invoke CheckSolrField
        /// What    Set field settings using SOLR field API
        /// </summary>
        [Fact]
        public void ExpressionBuilder005()
        {
            // Arrange
            var solrExpressOptions = new SolrExpressOptions();
            var solrConnection = new Mock<ISolrConnection>();
            solrConnection
                .Setup(q => q.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns(@"
                {
                    ""field"": {
                        ""indexed"": true,
                        ""stored"": true
                    }
                }");

            var fieldData = new FieldData
            {
                AliasName = "id",
                FieldName = "id",
                IsIndexed = false,
                IsStored = false
            };

            var expressionBuilder = new ExpressionBuilder<TestDocumentDynamic>(solrExpressOptions, solrConnection.Object);

            // Act
            expressionBuilder.CheckSolrField(ref fieldData);

            // Assert
            Assert.True(fieldData.IsIndexed);
            Assert.True(fieldData.IsStored);
        }
    }
}
