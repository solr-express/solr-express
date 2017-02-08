using Moq;
using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SolrExpress.UnitTests.Search.Result
{
    public class InformationResultTests
    {
        [Fact]
        public void InformationResultFact001()
        {
            // Arrange
            var jsonPlainText = @"
            {
              ""responseHeader"": {
                ""status"": 0,
                ""QTime"": 111
              },
              ""response"": {
                ""numFound"": 222,
                ""start"": 0,
                ""docs"": []
            }
            }";

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            var searchParameters = new List<ISearchParameter>
            {
                new Mock<IOffsetParameter<TestDocument>>().SetupProperty(q => q.Value, 20).Object,
                new Mock<ILimitParameter<TestDocument>>().SetupProperty(q => q.Value, 10).Object
            };

            var result = (IInformationResult<TestDocument>)new InformationResult<TestDocument>();

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            Assert.Equal(222, result.Data.DocumentCount);
            Assert.Equal(TimeSpan.FromMilliseconds(111), result.Data.ElapsedTime);
            Assert.Equal(true, result.Data.HasNextPage);
            Assert.Equal(true, result.Data.HasPreviousPage);
            Assert.Equal(false, result.Data.IsFirstPage);
            Assert.Equal(false, result.Data.IsLastPage);
            Assert.Equal(23, result.Data.PageCount);
            Assert.Equal(3, result.Data.PageNumber);
            Assert.Equal(10, result.Data.PageSize);
        }
    }
}
