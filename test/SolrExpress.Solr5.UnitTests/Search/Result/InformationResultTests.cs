using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Result
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
            var parameters = new List<ISearchParameter>
            {
                new LimitParameter<TestDocumentWithAnyPropertyTypes>().Configure(10),
                new OffsetParameter<TestDocumentWithAnyPropertyTypes>().Configure(1)
            };
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10},
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var result = (IConvertJsonObject)new InformationResult<TestDocumentWithAnyPropertyTypes>();

            long documentCount;
            TimeSpan timeToExecution;

            // Act
            result.Execute(parameters, jsonObject);
            documentCount = ((IInformationResult<TestDocumentWithAnyPropertyTypes>)result).Data.DocumentCount;
            timeToExecution = ((IInformationResult<TestDocumentWithAnyPropertyTypes>)result).Data.ElapsedTime;

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
            var parameters = new List<ISearchParameter>();
            var jsonObject = JObject.Parse(jsonStr);
            var result = (IConvertJsonObject)new InformationResult<TestDocumentWithAnyPropertyTypes>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => result.Execute(parameters, jsonObject));
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
            var parameters = new List<ISearchParameter>();
            var jsonObject = JObject.Parse(jsonStr);
            var result = (IConvertJsonObject)new InformationResult<TestDocumentWithAnyPropertyTypes>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => result.Execute(parameters, jsonObject));
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
            var parameters = new List<ISearchParameter>();
            var jsonObject = JObject.Parse(jsonStr);
            var result = (IConvertJsonObject)new InformationResult<TestDocumentWithAnyPropertyTypes>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => result.Execute(parameters, jsonObject));
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
            var parameters = new List<ISearchParameter>();
            var jsonObject = new JObject();
            var result = (IConvertJsonObject)new InformationResult<TestDocumentWithAnyPropertyTypes>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => result.Execute(parameters, jsonObject));
        }
    }
}
