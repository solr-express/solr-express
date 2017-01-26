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

        private const string DocumentResultData1 = @"{""responseHeader"": {""status"": 0,""QTime"": 0,""params"": {""q"": ""*:*"",""indent"": ""true"",""fq"": ""manu_id_s:samsung"",""wt"": ""json""}},""response"": {""numFound"": 1,""start"": 0,""docs"": [{""id"": ""SP2514N"",""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",""manu"": ""Samsung Electronics Co. Ltd."",""manu_id_s"": ""samsung"",""cat"": [""electronics"",""hard drive""],""features"": [""7200RPM, 8MB cache, IDE Ultra ATA-133"",""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""],""docs"": [""7200RPM, 8MB cache, IDE Ultra ATA-133"",""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""],""price"": 92.0,""price_c"": ""92.0,USD"",""popularity"": 6,""inStock"": true,""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",""store"": ""35.0752,-97.032"",""_version_"": 1557618225908285440}]}}";
        private const string DocumentResultData2 = @"
        {
          ""responseHeader"": {
            ""status"": 0,
            ""QTime"": 0,
            ""params"": {
              ""q"": ""*:*"",
              ""indent"": ""true"",
              ""fq"": ""manu_id_s:samsung"",
              ""wt"": ""json""
            }
          },
          ""response"": {
            ""numFound"": 1,
            ""start"": 0,
            ""docs"": [
              {
                ""id"": ""SP2514N"",
                ""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",
                ""manu"": ""Samsung Electronics Co. Ltd."",
                ""manu_id_s"": ""samsung"",
                ""cat"": [
                  ""electronics"",
                  ""hard drive""
                ],
                ""features"": [
                  ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                  ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                ],
		          ""docs"": [
                  ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                  ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                ],
                ""price"": 92.0,
                ""price_c"": ""92.0,USD"",
                ""popularity"": 6,
                ""inStock"": true,
                ""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",
                ""store"": ""35.0752,-97.032"",
                ""_version_"": 1557618225908285440
              }
            ]
          }
        }";
        private const string DocumentResultData3 = @"{""responseHeader"": {""status"": 0,""QTime"": 0,""params"": {""q"": ""*:*"",""indent"": ""true"",""fq"": ""manu_id_s:samsung"",""wt"": ""json""}},""_response_"": {""numFound"": 1,""start"": 0,""docs"": [{""id"": ""SP2514N"",""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",""manu"": ""Samsung Electronics Co. Ltd."",""manu_id_s"": ""samsung"",""cat"": [""electronics"",""hard drive""],""features"": [""7200RPM, 8MB cache, IDE Ultra ATA-133"",""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""],""docs"": [""7200RPM, 8MB cache, IDE Ultra ATA-133"",""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""],""price"": 92.0,""price_c"": ""92.0,USD"",""popularity"": 6,""inStock"": true,""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",""store"": ""35.0752,-97.032"",""_version_"": 1557618225908285440}]}}";
        private const string DocumentResultData4 = @"
        {
          ""responseHeader"": {
            ""status"": 0,
            ""QTime"": 0,
            ""params"": {
              ""q"": ""*:*"",
              ""indent"": ""true"",
              ""fq"": ""manu_id_s:samsung"",
              ""wt"": ""json""
            }
          },
          ""_response_"": {
            ""numFound"": 1,
            ""start"": 0,
            ""docs"": [
              {
                ""id"": ""SP2514N"",
                ""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",
                ""manu"": ""Samsung Electronics Co. Ltd."",
                ""manu_id_s"": ""samsung"",
                ""cat"": [
                  ""electronics"",
                  ""hard drive""
                ],
                ""features"": [
                  ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                  ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                ],
		          ""docs"": [
                  ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                  ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                ],
                ""price"": 92.0,
                ""price_c"": ""92.0,USD"",
                ""popularity"": 6,
                ""inStock"": true,
                ""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",
                ""store"": ""35.0752,-97.032"",
                ""_version_"": 1557618225908285440
              }
            ]
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

        [Theory]
        [InlineData(SearchResultRegexTests.DocumentResultData1, true)]
        [InlineData(SearchResultRegexTests.DocumentResultData2, true)]
        [InlineData(SearchResultRegexTests.DocumentResultData3, false)]
        [InlineData(SearchResultRegexTests.DocumentResultData4, false)]
        public void DocumentResultFragmentTheory001(string input, bool expected)
        {
            // Arrange / Act
            var actual = SearchResultRegex.DocumentResultFragment.IsMatch(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(SearchResultRegexTests.DocumentResultData1)]
        [InlineData(SearchResultRegexTests.DocumentResultData2)]
        public void DocumentResultFragmentTheory002(string input)
        {
            // Arrange
            var expectedFragment = "SP2514N";

            // Act
            var match = SearchResultRegex.DocumentResultFragment.Match(input);
            var actual = match.Groups[3].Value;

            // Assert
            Assert.True(actual.Contains(expectedFragment));
        }
    }
}