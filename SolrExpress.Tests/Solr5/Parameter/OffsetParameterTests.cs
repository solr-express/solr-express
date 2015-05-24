using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class OffsetParameterTests
    {
        /// <summary>
        /// Where   Using an instance of the class OffsetParameter
        /// When    Invoke the "Execute" method
        /// What    JsonQuery created with offset:10 argument
        /// </summary>
        [TestMethod]
        public void OffsetParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "OffsetParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var paramer = new OffsetParameter(10);

            // Act
            paramer.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }
    }
}
