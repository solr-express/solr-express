using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetRangeParameterTests
    {
        /// <summary>
        /// Where   Using an instance of the class FacetRangeParameter
        /// When    Invoke the "Execute" method with default arguments
        /// What    JsonQuery created with "facet": {"X": {"range": {"field": "X","start": "10","end": "20","gap": "1","other": ["before","after"]}}} argument
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetRangeParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>(q => q.Id, "X", "1", "10", "20");

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class FacetRangeParameter
        /// When    Invoke the "Execute" method with sort=quantity and order=descendent
        /// What    JsonQuery created with "facet": {"X": {"range": {"field": "X","start": "10","end": "20","gap": "1","other": ["before","after"],"sort": {"count": "desc"}}}} argument
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter002()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetRangeParameter002.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>(q => q.Id, "X", "1", "10", "20", SolrFacetSortType.Quantity, false);

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }
    }
}
