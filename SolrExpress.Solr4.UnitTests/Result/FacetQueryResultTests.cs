﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr4.Builder;

namespace SolrExpress.Solr4.UnitTests.Result
{
    [TestClass]
    public class FacetQueryResultTests
    {
        /// <summary>
        /// Where   Using a FacetQueryResult instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetQueryResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                 ""facet_counts"":{
                    ""facet_queries"":{
                      ""facetQuery"":10}
                }
            }");

            var parameter = new FacetQueryResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.IsTrue(parameter.Data.ContainsKey("facetQuery"));
            Assert.AreEqual(10, parameter.Data["facetQuery"]);
        }
    }
}