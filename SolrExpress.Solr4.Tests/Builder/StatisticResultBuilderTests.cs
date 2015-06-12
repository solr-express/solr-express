using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Solr4.Builder;

namespace SolrExpress.Solr4.Tests.Builder
{
    [TestClass]
    public class StatisticResultBuilderTests
    {
        /// <summary>
        /// Where   Using a StatisticResultBuilder instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void StatisticResultBuilder001()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10},
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new StatisticResultBuilder();
            bool isEmpty;
            long documentCount;
            TimeSpan timeToExecution;

            // Act
            builder.Execute(jsonObject);
            isEmpty = builder.IsEmpty;
            documentCount = builder.DocumentCount;
            timeToExecution = builder.TimeToExecution;

            // Assert
            Assert.AreEqual(false, isEmpty);
            Assert.AreEqual(1000, documentCount);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 10), timeToExecution);
        }

        /// <summary>
        /// Where   Using a StatisticResultBuilder instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void StatisticResultBuilder002()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseHeaderX"":{
                ""status"":0,
                ""QTime"":10},
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new StatisticResultBuilder();

            // Act / Assert
            builder.Execute(jsonObject);
        }

        /// <summary>
        /// Where   Using a StatisticResultBuilder instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void StatisticResultBuilder003()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new StatisticResultBuilder();

            // Act / Assert
            builder.Execute(jsonObject);
        }

        /// <summary>
        /// Where   Using a StatisticResultBuilder instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void StatisticResultBuilder004()
        {
            // Arrange
            var jsonStr = @"
            {
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new StatisticResultBuilder();

            // Act / Assert
            builder.Execute(jsonObject);
        }

        /// <summary>
        /// Where   Using a StatisticResultBuilder instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnexpectedJsonFormatException))]
        public void StatisticResultBuilder005()
        {
            // Arrange
            var jsonObject = new JObject();
            var builder = new StatisticResultBuilder();

            // Act / Assert
            builder.Execute(jsonObject);
        }
    }
}
