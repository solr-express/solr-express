using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FilterParameterTests
    {
        /// <summary>
        /// Where   Using an instance of the class SolrQueryable
        /// When    Invoke the "Add" method with a instance of FilterParameter
        /// What    JsonQuery created with filter:["Id:X", "Score:Y"] argument
        /// </summary>
        [TestMethod]
        public void FilterParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FilterParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter1 = new FilterParameter<TestDocument>(q => q.Id, "X");
            var parameter2 = new FilterParameter<TestDocument>(q => q.Score, "Y");

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }
    }
}
