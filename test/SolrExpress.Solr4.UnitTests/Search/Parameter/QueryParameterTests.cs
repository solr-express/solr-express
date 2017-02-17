//using Xunit;
//using SolrExpress.Core.Search.ParameterValue;
//using SolrExpress.Solr4.Search.Parameter;
//using System;
//using System.Collections.Generic;
//using SolrExpress.Core.Utility;

//namespace SolrExpress.Solr4.UnitTests.Search.Parameter
//{
//    public class QueryParameterTests
//    {
//        /// <summary>
//        /// Where   Using a QueryParameter instance
//        /// When    Invoking the method "Execute"
//        /// What    Create a valid string
//        /// </summary>
//        [Fact]
//        public void QueryParameter001()
//        {
//            // Arrange
//            var container = new List<string>();
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new QueryParameter<TestDocument>(expressionBuilder);
//            parameter.Configure(new Single<TestDocument>(q => q.Id, "ITEM01"));

//            // Act
//            parameter.Execute(container);

//            // Assert
//            Assert.Equal(1, container.Count);
//            Assert.Equal("q=_id_:ITEM01", container[0]);
//        }

//        /// <summary>
//        /// Where   Using a QueryParameter instance
//        /// When    Create the instance with null
//        /// What    Throws ArgumentNullException
//        /// </summary>
//        [Fact]
//        public void QueryParameter002()
//        {
//            // Arrange
//            var expressionCache = new ExpressionCache<TestDocument>();
//            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
//            var parameter = new QueryParameter<TestDocument>(expressionBuilder);

//            // Act / Assert
//            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
//        }
//    }
//}
