using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Exception;
using SolrExpress.Solr5.Builder;
using SolrExpress.Solr5.Tests;
using System.Collections.Generic;

namespace SolrExpress.Tests.Query
{
    [TestClass]
    public class DocumentBuilderTests
    {
        /// <summary>
        /// Where   Using a DocumentBuilder instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void DocumentBuilder001()
        {
            // Arrange
            var jsonStr = @"
            {
              ""response"":{""numFound"":7722,""start"":0,""maxScore"":1.0,""docs"":[
                  {
                    ""id"":""ITEM01"",
                    ""score"":1.5}]
              }
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new DocumentBuilder<TestDocument>();
            List<TestDocument> lst;

            // Act
            builder.Execute(jsonObject);
            lst = builder.Data;

            // Assert
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual("ITEM01", lst[0].Id);
            Assert.AreEqual(1.5M, lst[0].Score);
        }

        /// <summary>
        /// Where   Using a DocumentBuilder instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void DocumentBuilder002()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseX"":{""numFound"":7722,""start"":0,""maxScore"":1.0,""docs"":[
                  {
                    ""id"":""ITEM01"",
                    ""score"":1.5}]
              }
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new DocumentBuilder<TestDocument>();

            // Act / Assert
            builder.Execute(jsonObject);
        }
    }
}
