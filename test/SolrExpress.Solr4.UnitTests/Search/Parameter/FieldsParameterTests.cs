﻿using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FieldsParameterTests
    {
        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FieldsParameter001()
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = new FieldsParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Score
            };

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("fl=id,score", container[0]);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeStoredTrueAttribute
        /// </summary>
        [Fact]
        public void FieldsParameter002()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FieldsParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeStoredTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
