using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FilterParameterTests
    {
        [TestMethod]
        public void WhenExecuteWith2Instances_CreateJson()
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
            var parameter1 = new FilterParameter<TestDocument>(q => q.Id, "X");
            var parameter2 = new FilterParameter<TestDocument>(q => q.Score, "Y");

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
