using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using SolrExpress.Solr4.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Query.Result
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
                              ""10.0"", 2,
                              ""70.0"", 1,
                              ""90.0"", 1
                            ],
                            ""gap"": 10,
                            ""start"": 10,
                            ""end"": 100,
                            ""before"": 34,
                            ""after"": 9}}}
            }");

            var parameters = new List<IParameter> {
                new FacetRangeParameter<TestDocument>().Configure("facetRange", q=> q.Score, "10", "10", "100")
            };
            var result = new FacetRangeResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            Assert.Equal(1, result.Data.Count);
            Assert.Equal("facetRange", result.Data[0].Name);
            Assert.Equal(5, result.Data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<decimal>), result.Data[0].Data.First().Key);
            Assert.Equal(10, ((FacetRange<decimal>)result.Data[0].Data.First().Key).MaximumValue.Value);
            Assert.Equal(34, result.Data[0].Data.First().Value);
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

            var parameters = new List<IParameter> {
                new FacetRangeParameter<TestDocument>().Configure("facetRange", q=> q.CreatedAt, "10", "10", "100")
            };
            var result = new FacetRangeResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            Assert.Equal(1, result.Data.Count);
            Assert.Equal("facetRange", result.Data[0].Name);
            Assert.Equal(4, result.Data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<DateTime>), result.Data[0].Data.First().Key);
            Assert.Equal(DateTime.Parse("2014-06-22T20:33:00.741Z").Date, ((FacetRange<DateTime>)result.Data[0].Data.First().Key).MaximumValue.Value.Date);
            Assert.Equal(3, result.Data[0].Data.First().Value);
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

            var parameters = new List<IParameter> {
                new FacetRangeParameter<TestDocument>().Configure("facetRange", q=> q.Score, "10", "10", "100")
            };
            var result = new FacetRangeResult<TestDocument>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            Assert.Equal(1, result.Data.Count);
            Assert.Equal("facetRange", result.Data[0].Name);
            Assert.Equal(4, result.Data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<decimal>), result.Data[0].Data.First().Key);
            Assert.Equal(10, ((FacetRange<decimal>)result.Data[0].Data.First().Key).MaximumValue.Value);
            Assert.Equal(3, result.Data[0].Data.First().Value);
        }
    }
}
