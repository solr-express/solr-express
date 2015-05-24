using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Exception;
using SolrExpress.QueryBuilder;
using SolrExpress.Solr5.Builder;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolrExpress.Tests.QueryBuilder
{
    /// <summary>
    /// Unit tests to ResultDataBuilder
    /// </summary>
    [TestClass]
    public class ResultDataBuilderTests
    {
        /// <summary>
        /// Where   Using an instance of the class SimpleResultDataBuilder
        /// When    Invoke the "Execute" method with a expected JSON object
        /// What    Do the parse to a list of class than implements IDocument
        /// </summary>
        [TestMethod]
        public void SimpleResultDataBuilder001()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "QueryBuilder", "ResultDataBuilder01.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new ResultDataBuilder<TestDocument>();
            List<TestDocument> lst;

            // Act
            lst = builder.Execute(jsonObject);

            // Assert
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual("ITEM01", lst[0].Id);
            Assert.AreEqual(1.5M, lst[0].Score);
        }

        /// <summary>
        /// Where   Using an instance of the class SimpleResultDataBuilder
        /// When    Invoke the "Execute" method with a expected JSON object
        /// What    Do the parse to a list of class than implements IDocument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void SimpleResultDataBuilder002()
        {
            // Arrange
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "QueryBuilder", "ResultDataBuilder02.txt");
            var jsonStr = File.ReadAllText(jsonFilePath);
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new ResultDataBuilder<TestDocument>();

            // Act / Assert
            builder.Execute(jsonObject);
        }
    }
}
