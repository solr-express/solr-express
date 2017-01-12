using Xunit;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Solr4.Search.Parameter;
using System;
using System.Collections.Generic;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FilterQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter1 = new FilterQueryParameter<TestDocument>(expressionBuilder);
            var parameter2 = new FilterQueryParameter<TestDocument>(expressionBuilder);
            parameter1.Configure(new Single<TestDocument>(q => q.Id, "X"));
            parameter2.Configure(new Single<TestDocument>(q => q.Score, "Y"));

            // Act
            parameter1.Execute(container);
            parameter2.Execute(container);

            // Assert
            Assert.Equal(2, container.Count);
            Assert.Equal("fq=_id_:X", container[0]);
            Assert.Equal("fq=_score_:Y", container[1]);
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FilterQueryParameter002()
        {
            // Arrange
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FilterQueryParameter<TestDocument>(expressionBuilder);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using tag name
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FilterQueryParameter003()
        {
            // Arrange
            var container = new List<string>();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FilterQueryParameter<TestDocument>(expressionBuilder);
            parameter.Configure(new Single<TestDocument>(q => q.Id, "X"), "tag1");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fq={!tag=tag1}_id_:X", container[0]);
        }
    }
}
