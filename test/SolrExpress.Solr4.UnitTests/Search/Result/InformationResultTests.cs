using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.UnitTests;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.Result
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
                new RowsParameter<TestDocument>().Configure(10),
                new StartParameter<TestDocument>().Configure(1)
            };            
            var jsonStr = @"
            {
              ""responseHeader"":{
                ""status"":0,
                ""QTime"":10},
                ""response"":{""numFound"":1000,""start"":0,""maxScore"":1.0}
            }";
            var jsonObject = JObject.Parse(jsonStr);
            var result = new InformationResult<TestDocument>();
            long documentCount;
            TimeSpan timeToExecution;

            // Act
            ((IConvertJsonObject)result).Execute(parameters, jsonObject);
            documentCount = result.Data.DocumentCount;
            timeToExecution = result.Data.ElapsedTime;

            // Assert
            Assert.Equal(1000, documentCount);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 10), timeToExecution);
        }
    }
}
