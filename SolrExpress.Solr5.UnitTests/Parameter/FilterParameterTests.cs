using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr5.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Parameter
{
    [TestClass]
    public class FilterParameterTests
    {
        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FilterParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""Id:X"",
                ""Score:Y""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var parameter1 = new FilterParameter(new Single<TestDocument>(q => q.Id, "X"));
            var parameter2 = new FilterParameter(new Single<TestDocument>(q => q.Score, "Y"));

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FilterParameter002()
        {
            // Arrange / Act / Assert
            new FilterParameter(null);
        }

        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Invoking the method "Execute" using tag name
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FilterParameter003()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""filter"": [
                ""{!tag=tag1}Id:X""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var parameter1 = new FilterParameter(new Single<TestDocument>(q => q.Id, "X"), "tag1");

            // Act
            parameter1.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
