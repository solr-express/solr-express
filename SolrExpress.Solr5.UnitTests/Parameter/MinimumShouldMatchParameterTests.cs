using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Parameter
{
    [TestClass]
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                mm:""75%""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new MinimumShouldMatchParameter("75%");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinimumShouldMatchParameter002()
        {
            // Arrange / Act / Assert
            new MinimumShouldMatchParameter(null);
        }
    }
}
