using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search.ParameterValue;
using SolrExpress.Core.Utility;
using SolrExpress.Solr5.Search.Parameter;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FilterParameterTests
    {
        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FilterParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""_id_:X"",
                ""_score_:Y""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter1 = new FilterParameter<TestDocument>(expressionBuilder);
            var parameter2 = new FilterParameter<TestDocument>(expressionBuilder);
            parameter1.Configure(new Single<TestDocument>(q => q.Id, "X"));
            parameter2.Configure(new Single<TestDocument>(q => q.Score, "Y"));
            
            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FilterParameter002()
        {
            // Arrange
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FilterParameter<TestDocument>(expressionBuilder);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Invoking the method "Execute" using tag name
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FilterParameter003()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""{!tag=tag1}_id_:X""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new FilterParameter<TestDocument>(expressionBuilder);
            parameter.Configure(new Single<TestDocument>(q => q.Id, "X"), "tag1");
            
            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
