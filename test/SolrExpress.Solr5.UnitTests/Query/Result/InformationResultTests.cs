using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Solr5.Query.Result;
using System;
using System.Collections.Generic;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Parameter;

namespace SolrExpress.Solr5.UnitTests.Query.Result
{
    public class InformationResultTests
    {
        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void InformationResult001()
        {
            // Arrange
            var parameters = new List<IParameter>
            {
                new LimitParameter().Configure(10),
                new OffsetParameter().Configure(1)
            };
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10},
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new InformationResult<TestDocument>();
            
            long documentCount;
            TimeSpan timeToExecution;

            // Act
            builder.Execute(parameters, jsonObject);
            documentCount = builder.Data.DocumentCount;
            timeToExecution = builder.Data.ElapsedTime;

            // Assert
            Assert.Equal(1000, documentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 10), timeToExecution);
        }

        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [Fact]
        public void InformationResult002()
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
            var builder = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => builder.Execute(null, jsonObject));
        }

        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [Fact]
        public void InformationResult003()
        {
            // Arrange
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => builder.Execute(null, jsonObject));
        }

        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [Fact]
        public void InformationResult004()
        {
            // Arrange
            var jsonStr = @"
            {
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var builder = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => builder.Execute(null, jsonObject));
        }

        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [Fact]
        public void InformationResult005()
        {
            // Arrange
            var jsonObject = new JObject();
            var builder = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => builder.Execute(null, jsonObject));
        }
    }
}
