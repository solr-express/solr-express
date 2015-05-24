using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetQueryParameterTests
    {
        /// <summary>
        /// Where   Using an instance of the class FacetQueryParameter
        /// When    Invoke the "Execute" method with default arguments
        /// What    JsonQuery created with facet={X:{type:terms,field:X}} argument
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetQueryParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetQueryParameter("X", "avg('Y')");

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class FacetQueryParameter
        /// When    Invoke the "Execute" method with sort=quantity and order=descendent
        /// What    JsonQuery created with "facet": {"X": {"query": {"q": "avg('Y')","sort": {"count": "desc"}}}} argument
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter002()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetQueryParameter002.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetQueryParameter("X", "avg('Y')", SolrFacetSortType.Quantity, false);

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

    }
}
