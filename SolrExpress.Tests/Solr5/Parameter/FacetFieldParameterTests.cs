using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Solr5.Parameter;
using System;
using System.IO;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetFieldParameterTests
    {
        /// <summary>
        /// Where   Using an instance of the class FacetFieldParameter
        /// When    Invoke the "Execute" method with default arguments
        /// What    JsonQuery created with facet={Id:{type:terms,field:Id}} argument
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetFieldParameter001.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id);

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }

        /// <summary>
        /// Where   Using an instance of the class FacetFieldParameter
        /// When    Invoke the "Execute" method with sort=quantity and order=descendent
        /// What    JsonQuery created with facet={X:{type:terms,field:X,sort:{index:desc}}} argument
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter002()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Solr5", "Parameter", "FacetFieldParameter002.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            string jsonExpression;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.Quantity, false);

            // Act
            parameter.Execute(jObject);
            jsonExpression = jObject.ToString();

            // Assert
            Assert.AreEqual(jsonStr, jsonExpression);
        }
    }
}
