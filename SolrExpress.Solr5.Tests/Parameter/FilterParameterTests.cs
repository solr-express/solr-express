using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;
using SolrExpress.Solr5.ParameterValue;
using SolrExpress.Solr5.Tests;

namespace SolrExpress.Tests.Solr5.Parameter
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
            var parameter1 = new FilterParameter(new SingleValue<TestDocument>(q => q.Id, "X"));
            var parameter2 = new FilterParameter(new SingleValue<TestDocument>(q => q.Score, "Y"));

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
