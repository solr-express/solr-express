using Moq;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.UnitTests.Search.Result
{
    public class InformationResultTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void InformationResultTheory001(string jsonPlainText)
        {
            // Arrange
            var searchParameters = new List<ISearchParameter>();
            var result = (IInformationResult<TestDocument>)new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => result.Execute(searchParameters, jsonPlainText));
        }

        [Fact]
        public void InformationResultFact001()
        {
            // Arrange
            List<ISearchParameter> searchParameters = null;
            var jsonPlainText = ".";
            var result = (IInformationResult<TestDocument>)new InformationResult<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => result.Execute(searchParameters, jsonPlainText));
        }

        [Fact]
        public void InformationResultFact002()
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

            var searchParameters = new List<ISearchParameter>
            {
                new Mock<IOffsetParameter<TestDocument>>().SetupProperty(q => q.Value, 20).Object,
                new Mock<ILimitParameter<TestDocument>>().SetupProperty(q => q.Value, 10).Object
            };

            var result = (IInformationResult<TestDocument>)new InformationResult<TestDocument>();

            // Act
            result.Execute(searchParameters, jsonPlainText);

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
