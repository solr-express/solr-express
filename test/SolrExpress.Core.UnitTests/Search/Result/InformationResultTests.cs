using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.Result
{
    public class InformationResultTests
    {
        /// <summary>
        /// Where   Using a InformationResult instance
        /// When    Invoking the method "Execute" using a invvalid JSON
        /// What    Throws UnexpectedJsonFormatException
        /// </summary>
        [Fact]
        public void InformationResult001()
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
            var result = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => ((IConvertJsonObject)result).Execute(parameters, jsonObject));
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
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10}
            }";
            var parameters = new List<ISearchParameter>();
            var jsonObject = JObject.Parse(jsonStr);
            var result = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => ((IConvertJsonObject)result).Execute(parameters, jsonObject));
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
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var parameters = new List<ISearchParameter>();
            var jsonObject = JObject.Parse(jsonStr);
            var result = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => ((IConvertJsonObject)result).Execute(parameters, jsonObject));
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
            var parameters = new List<ISearchParameter>();
            var jsonObject = new JObject();
            var result = new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<UnexpectedJsonFormatException>(() => ((IConvertJsonObject)result).Execute(parameters, jsonObject));
        }
    }
}
