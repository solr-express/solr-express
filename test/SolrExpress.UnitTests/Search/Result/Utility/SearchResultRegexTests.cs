using SolrExpress.Search.Result.Utility;
using System;
using Xunit;

namespace SolrExpress.UnitTests.Search.Result.Utility
{
    public class SearchResultRegexTests
    {
        
        private const string InformationResultData1 = @"{""responseHeader"": {""status"": 0,""QTime"": 123456},""response"": {""numFound"": 654321,""start"": 0,""docs"": []}}";
        private const string InformationResultData2 = @"
        {
          ""responseHeader"": {
            ""status"": 0,
            ""QTime"": 123456
          },
          ""response"": {
            ""numFound"": 654321,
            ""start"": 0,
            ""docs"": []
        }
        }";
        private const string InformationResultData3 = @"{""__responseHeader__"": {""status"": 0,""QTime"": 123456},""__response__"": {""numFound"": 654321,""start"": 0,""docs"": []}}";
        private const string InformationResultData4 = @"
        {
          ""__responseHeader__"": {
            ""status"": 0,
            ""QTime"": 123456
          },
          ""__response__"": {
            ""numFound"": 654321,
            ""start"": 0,
            ""docs"": []
        }
        }";

        [Theory]
        [InlineData(SearchResultRegexTests.InformationResultData1, true)]
        [InlineData(SearchResultRegexTests.InformationResultData2, true)]
        [InlineData(SearchResultRegexTests.InformationResultData3, false)]
        [InlineData(SearchResultRegexTests.InformationResultData4, false)]
        public void InformationResultElapsedTimeragmentTheory001(string input, bool expected)
        {
            // Arrange / Act
            var actual = SearchResultRegex.InformationResultElapsedTimeragment.IsMatch(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(SearchResultRegexTests.InformationResultData1, 123456)]
        [InlineData(SearchResultRegexTests.InformationResultData2, 123456)]
        public void InformationResultElapsedTimeragmentTheory002(string input, long expected)
        {
            // Arrange / Act
            var match = SearchResultRegex.InformationResultElapsedTimeragment.Match(input);
            var actual = Convert.ToInt64(match.Groups[3].Value);

            // Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(SearchResultRegexTests.InformationResultData1, true)]
        [InlineData(SearchResultRegexTests.InformationResultData2, true)]
        [InlineData(SearchResultRegexTests.InformationResultData3, false)]
        [InlineData(SearchResultRegexTests.InformationResultData4, false)]
        public void InformationResultDocumentCountFragmentTheory001(string input, bool expected)
        {
            // Arrange / Act
            var actual = SearchResultRegex.InformationResultDocumentCountFragment.IsMatch(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(SearchResultRegexTests.InformationResultData1, 654321)]
        [InlineData(SearchResultRegexTests.InformationResultData2, 654321)]
        public void InformationResultDocumentCountFragmentTheory002(string input, long expected)
        {
            // Arrange / Act
            var match = SearchResultRegex.InformationResultDocumentCountFragment.Match(input);
            var actual = Convert.ToInt64(match.Groups[3].Value);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}