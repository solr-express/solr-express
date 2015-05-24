using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FieldsParameterTests
    {
        /// <summary>
        /// Where   Using instances of the class FieldsParameter
        /// When    Invoke the "Execute" method with 2 instances of FieldsParameter
        /// What    JsonQuery created with fields:["Id","Score"] argument
        /// </summary>
        [TestMethod]
        public void FieldsParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FieldsParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter1 = new FieldsParameter<TestDocument>(q => q.Id);
            var parameter2 = new FieldsParameter<TestDocument>(q => q.Score);

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }
    }
}
