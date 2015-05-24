using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FieldsParameterTests
    {
        [TestMethod]
        public void WhenExecuteWith2Instances_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""Id"",
                ""Score""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var parameter1 = new FieldsParameter<TestDocument>(q => q.Id);
            var parameter2 = new FieldsParameter<TestDocument>(q => q.Score);

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
