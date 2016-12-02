using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Result
{
    public class FacetRangeResultTests
    {
        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with integer values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetRangeResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                        ""facetRange"": {
                            ""counts"": [
                              ""10"", 2,
                              ""70"", 1,
                              ""90"", 1
                            ],
                            ""gap"": 10,
                            ""start"": 10,
                            ""end"": 100,
                            ""before"": 34,
                            ""after"": 9}}}
            }");

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropInteger, "10", "10", "100")
            };
            var result = new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            ((IConvertJsonObject)result).Execute(parameters, jObject);

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(5, data[0].Data.Count());
            Assert.IsType(typeof(FacetRange<int>), data[0].Data.First().Key);
            Assert.Equal(10, ((FacetRange<int>)data[0].Data.First().Key).MaximumValue.Value);
            Assert.Equal(34, data[0].Data.First().Quantity);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with date time values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetRangeResult002()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                            ""2014-06-22T20:33:00.741Z"",10,
                            ""2014-07-04T20:33:00.741Z"",10],
                        ""gap"":""+6DAYS"",
                        ""start"":""2014-06-22T20:33:00.741Z"",
                        ""end"":""2015-06-23T20:33:00.741Z"",
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropDateTime, "+10DAYS", "NOW/YEAR-1", "NOW/DAY+1")
            };
            var result = new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            ((IConvertJsonObject)result).Execute(parameters, jObject);

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count());
            Assert.IsType(typeof(FacetRange<DateTime>), data[0].Data.First().Key);
            Assert.Equal(DateTime.Parse("2014-06-22T20:33:00.741Z").Date, ((FacetRange<DateTime>)data[0].Data.First().Key).MaximumValue.Value.Date);
            Assert.Equal(3, data[0].Data.First().Quantity);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with float values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetRangeResult003()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                          ""10.5"",2,
                          ""31.5"",1],
                        ""gap"":10.5,
                        ""start"":10.0,
                        ""end"":100.0,
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropDecimal, "10", "10", "100")
            };
            var result = new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            ((IConvertJsonObject)result).Execute(parameters, jObject);

            // Assert
            var data = result.Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count());
            Assert.IsType(typeof(FacetRange<decimal>), data[0].Data.First().Key);
            Assert.Equal(10, ((FacetRange<decimal>)data[0].Data.First().Key).MaximumValue.Value);
            Assert.Equal(3, data[0].Data.First().Quantity);
        }
    }
}
